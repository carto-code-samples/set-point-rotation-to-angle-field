# set-point-rotation-to-angle-field
Add-in for ArcGIS Pro that will update a point layer's symbology settings so that the point symbols are rotated based on the value of the "angle" field. 

### Requirements
- ArcGIS Pro version 2.8, 2.9, 3.0

### To use:
- In ArcGIS Pro, select a point layer in the Table of Contents of a map by clicking it. The point layer must contain a numerical field named "angle".
- With that layer highlighted, go to the Add-Ins tab and click the Set Point Rotation To Angle Field Button. 
- Every symbol layer in the point symbols will now have the rotation property mapped to the "angle" attribute. 

### To install:
Clone this repository and double-click the .esriAddinX file located here to install the add-in:

- For ArcGIS Pro 2.8, 2.9:
`..\set-point-rotation-to-angle-field\v28\SetPointRotationToAngleField\bin\Debug\SetPointRotationToAngleField.esriAddinX`
- For ArcGIS Pro 3.0:
`..\set-point-rotation-to-angle-field\v30\SetPointRotationToAngleField\bin\Debug\net6.0-windows\SetPointRotationToAngleField.esriAddinX`

To install without cloning this repository, download that individual file from this repo and run it:

- For ArcGIS Pro 2.8, 2.9:
https://github.com/carto-code-samples/set-point-rotation-to-angle-field/blob/main/v28/SetPointRotationToAngleField/bin/Debug/SetPointRotationToAngleField.esriAddinX
- For ArcGIS Pro 3.0:
https://github.com/carto-code-samples/set-point-rotation-to-angle-field/blob/main/v30/SetPointRotationToAngleField/bin/Debug/net6.0-windows/SetPointRotationToAngleField.esriAddinX

### To modify:
Install Visual Studio 2022 and the ArcGIS Pro SDK for developers following the instructions in this repo:

https://github.com/Esri/arcgis-pro-sdk/wiki/ProGuide-Installation-and-Upgrade

Clone this repository and open the solution file inside the main folder named SetPointRotationToAngleField.sln
