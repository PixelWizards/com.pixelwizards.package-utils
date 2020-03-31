# Package Utility

Simple editor window designed to help you package up content from a Unity project into a Package Manager friendly structure and generate the necessary package.json manifest.

# Setup
Add to your project (see the readme) via git or OpenUPM, then navigate to Window->General->Package Utility

## Basic workflow

The package utility window provides an interface to edit your custom package, as shown below:

![Package Utility Window](images/PackageUtility.png)

Simply fill out the form, select a root folder for the package source (should be within your Unity project), specify a destination folder (should be outside of your Unity project) and hit 'Export Package'

The utility will make a copy of the source folder in the new location and also write a package.json manifest that you can use to load the package into Unity.
