1. QUICK START

To demo the property grid, open and run the scene 'PropertyGrid.unity'. You'll see the property grid control in the center and a cube on the left and right. Click on the cubes to select them in the property grid and modify their properties.

To add a property grid to your own scenes, go to the hierarchy view and right click on a UI canvas (or a child of a UI canvas) and navigate to UI->PropertyGrid.

Once you've added a property grid to the scene, you can select it and view its corresponding script in the inspector. The property grid script exposes a property called "Target Object". Set this property in the editor (or via script at runtime) to determine which object the property grid has selected.


2. MANUAL MODE

You can also use the property grid without a target object by adding the properties manually. To do this, call the method PropertyGrid.AppendProperty<T>, specifiying a type derived from the PropertyGridItem class. Then use the property's ValueChanged event to detect edits.

For example:

var property = AppendProperty<PropertyGridBool>("Boolean Property", false);
property.ValueChanged += (o, e) =>
{
    Debug.Log("Property changed! Value=" + property.Value.ToString());
};


3. THANK YOU FROM NAMUDEV

Thank you very much for trying this asset. This is our company's first Unity project and we hope that it will serve you well. If you have any questions, bug reports, suggestions or criticisms, we'd love to hear from you! Connect with us on Facebook (facebook.com/namudevco) or Twitter (@namudevco), or e-mail us at namudevco@gmail.com. (And if you would be interested in guiding the course of future namudev projects, ask us about our codeveloper program!)
