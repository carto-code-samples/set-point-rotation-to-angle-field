# set-point-rotation-to-angle-field
Add-in for ArcGIS Pro that will update a point layer's symbology settings so that the point symbols are rotated based on the value of the "angle" field. 

### Requirements
- ArcGIS Pro version 2.8 or 2.9

### To use:
- In ArcGIS Pro, select a point layer in the Table of Contents of a map by clicking it. The point layer must contain a numerical field named "angle".
- With that layer highlighted, go to the Add-Ins tab and click the Set Point Rotation To Angle Field Button. 
- Every symbol layer in the point symbols will now have the rotation property mapped to the "angle" attribute. 

### To install:
Clone this repository and double-click the .esriAddinX file located here to install the add-in:

`..\set-point-rotation-to-angle-field\SetPointRotationToAngleField\bin\Debug\SetPointRotationToAngleField.esriAddinX`

To install without cloning this repository, download that individual file from this repo and run it:

https://github.com/carto-code-samples/set-point-rotation-to-angle-field/blob/main/SetPointRotationToAngleField/bin/Debug/SetPointRotationToAngleField.esriAddinX

### To modify:
Install Visual Studio 2019 and the ArcGIS Pro SDK for developers following the instructions in this repo:

https://github.com/Esri/arcgis-pro-sdk/wiki/ProGuide-Installation-and-Upgrade

Clone this repository and open the solution file inside the main folder named SetPointRotationToAngleField.sln
