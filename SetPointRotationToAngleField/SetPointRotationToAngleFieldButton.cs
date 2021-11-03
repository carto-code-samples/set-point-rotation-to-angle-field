using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArcGIS.Core.CIM;
using ArcGIS.Core.Data;
using ArcGIS.Core.Geometry;
using ArcGIS.Desktop.Catalog;
using ArcGIS.Desktop.Core;
using ArcGIS.Desktop.Editing;
using ArcGIS.Desktop.Extensions;
using ArcGIS.Desktop.Framework;
using ArcGIS.Desktop.Framework.Contracts;
using ArcGIS.Desktop.Framework.Dialogs;
using ArcGIS.Desktop.Framework.Threading.Tasks;
using ArcGIS.Desktop.Layouts;
using ArcGIS.Desktop.Mapping;

namespace SetPointRotationToAngleField
{
  internal class SetPointRotationToAngleFieldButton : Button
  {
    internal static bool IsNumericFieldType(FieldType type)
    {
      switch (type)
      {
        case FieldType.Double:
        case FieldType.Integer:
        case FieldType.Single:
        case FieldType.SmallInteger:
          return true;
        default:
          return false;
      }
    }

    protected bool hasNumericAngleField(List<FieldDescription> fieldDesc)
    {
      foreach (var f in fieldDesc)
      {
        if ((f.Name == "angle") && IsNumericFieldType(f.Type))
        {
          return true;
        }
      }
      return false;
    }

    protected override async void OnClick()
    {
      // Check for an active 2D mapview, if not, then prompt and exit.
      if (MapView.Active == null || (MapView.Active.ViewingMode != ArcGIS.Core.CIM.MapViewingMode.Map))
      {
        ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show("An active 2D MapView is required to use this tool. Exiting...", "Info");
        return;
      }
      // Get the layer(s) selected in the Contents pane, if there is not just one, then prompt then exit.
      if (MapView.Active.GetSelectedLayers().Count != 1)
      {
        ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show("One feature layer must be selected in the Contents pane. Exiting...", "Info");
        return;
      }
      var featLayer = MapView.Active.GetSelectedLayers().First() as FeatureLayer;
      if (featLayer == null)
      {
        ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show("A feature layer must be selected in the Contents pane. Exiting...", "Info");
        return;
      }
      // Check if the feature layer shape type is point, if not, then prompt and exit.
      if (featLayer.ShapeType != esriGeometryType.esriGeometryPoint)
      {
        ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show("Selected feature layer must be shape type POINT. Exiting...", "Info");
        return;
      }
      // Check if the feature layer has a numeric field named "angle", if not, then prompt and exit
      var fieldDescriptions = await QueuedTask.Run(() =>
      {
        return featLayer.GetFieldDescriptions();
      });
      if (!hasNumericAngleField(fieldDescriptions))
      {
        ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show("Selected feature layer must contain a numeric field named 'angle'. Exiting...", "Info");
        return;
      }


      try
      {
        await QueuedTask.Run(() =>
        {
          var def = featLayer.GetDefinition() as CIMFeatureLayer;
          var renderer = def.Renderer as CIMSimpleRenderer;
          var symbol = renderer.Symbol as CIMSymbolReference;
          var ptSymbol = symbol.Symbol as CIMPointSymbol;
          var symbolLayers = ptSymbol.SymbolLayers;

          var primitiveOverrides = new List<CIMPrimitiveOverride>();

          foreach (var symbolLayer in symbolLayers)
          {
            if (symbolLayer.PrimitiveName == null)
            {
              //symbolLayer.PrimitiveName = Guid.NewGuid().ToString();
              symbolLayer.PrimitiveName = "SetPointRotationToAngleField";
            }

            primitiveOverrides.Add(new CIMPrimitiveOverride()
            {
              PrimitiveName = symbolLayer.PrimitiveName,
              PropertyName = "Rotation",
              Expression = "$feature.angle"
            });
          };
          symbol.PrimitiveOverrides = primitiveOverrides.ToArray();
          featLayer.SetDefinition(def);
        });
      }
      catch (Exception exc)
      {
        // Catch any exception found and display in a message box
        ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show("Exception caught: " + exc.Message);
        return;
      }

    }
  }
}
