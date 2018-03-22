[![Build Status](http://brightwavepartners.com:8080/buildStatus/icon?job=brightwavepartners/Kuando.Control/master)](http://brightwavepartners.com:8080/job/brightwavepartners/job/Kuando.Control/job/master/)

### What is Kuando.Control?

Kuando.Control is a Windows tray application that is built as a modular control application for the [Kuando BusyLight](http://www.plenom.com/products/kuando-busylight-uc-for-skype4b-lync-cisco-jabber-more/).

The modularity aspect of this application is meant to provide the ability to "plug-in" additional modules to the control application in order to support new and additional functionality without having
to write a new application from scratch for each control scenario.

Modularity is built on top of the [Microsoft Prism Library](https://msdn.microsoft.com/en-us/library/gg406140.aspx) and the [Managed Extensibility Framework](https://docs.microsoft.com/en-us/dotnet/framework/mef/).

The tray application also provides a quick context menu to change the light to red, yellow, or green.

<img src="https://brightwavepartners.blob.core.windows.net/kuando-control/contextmenu.png" alt="context menu">

### How do I run the application?

At present, there are no pre-compiled binaries. Download the source and build to produce the required artifacts to run the program. Once compiled, you can run the binaries from the build output folder,
but it would probably be more appropriate to copy the build output to a more permanent folder and run from there.

The easiest method to have the application start with Windows is add it to the Startup folder. On your keyboard, press the Windows key and the R key together to bring up the Run dialog. In the Run dialog
type **shell:startup** and click the **OK** button. Your Startup folder for Windows should open in Window File Explorer. Right-click in the folder and select **New** -> **Shortcut**. Either type in the
path to program files from above, or use the **Browse** button to find the path. You will want to run the program **Kuando.Control.exe**. Click on the **Next** button. Give your new shortcut a name
(e.g. Kuando Control) and click the **Finish** button. The application will now start everytime Windows starts.

### How do I get started adding additional modules?

If you are unfamiliar with Prism or MEF, it is highly recommended that you visit the links above and become familiar with the general concepts. It will help to understand if, or more likely when, things
don't operate as expected how to fix them. Due to the dynamic nature of Prism and MEF, it can be difficult to track down unexpected behavior unless you understand how they operate and the conventions used.

Refer to the Google Hangouts project that is already a part of the solution for guidance as you go through the following steps.

There are two sections required for adding a new module. The navigation section and the settings section. The navigation section is the left hand navigation tree that provides a button to navigate to your module's settings. The settings section is where you will actually allow the user to configure settings (if necessary) for your module.

## Navigation
1. Add a new **Class Library (.NET Framework)** project to **src** folder and name it following the pattern Kuando.Control.Modules.[yourmodulename].
2. Add NuGet packages to your new project for Prism.Wpf and Prism.Mef
3. Add a reference to the .Net assembly System.ComponentModel.Composition
4. As a convention, the name of the main class of a new Prism module is post-fixed with the word **module**. If you kept the Class1.cs file from when the project was created, rename it to [yourmodulename] Module. If you did not keep the Class1.cs file, add a new class file and give it the name indicated previously. Make sure the name of the class in the code is also renamed to match the filename.
5. Add using statements for Prism.Mef.Modularity and Prism.Modularity to the new class.
4. Add the module export attribute to the module class of your new module and have it implement Prism's **IModule** interface.

```
[ModuleExport(typeof(GoogleHangoutsModule))]
public class GoogleHangoutsModule : IModule
{
   public void Initialize()
   {
   }
}
```

5. Add a constructor to the module and decorate it with the [ImportingConstructor] attribute.
6. Add IRegionManager to the constructor parameters so that Prism's region manager will get injected. Prism's region manager is used to display items in regions on the UI.

```
[ImportingConstructor]
public GoogleHangoutsModule(IRegionManager regionManager)
{
}
```

5. Add a views folder
6. Add a new **User Control (WPF)** to the views folder to be used as the navigation view. Do not post-fix this file with the word **View**. The automatic ViewModel wiring between the View and the ViewModel is using a convention approach where the ViewModel will use whatever the name is for the View and try to locate a file with the same name post-fixed with the word **ViewModel** in the same folder as the view. The recommended name for the user control is [yourmodulename]Navigation, but you can technically name this any way you like as long as the ViewModel follows the convention indicated in the next step. The navigation view is simply used to show a navigation button on the left side of the application's dialog window to switch to your view when the button is clicked.

<img src="https://brightwavepartners.blob.core.windows.net/kuando-control/navigation.png" alt="navigation">

7. Add **ViewModelLocator.AutoWireViewModel="True"** to the .xaml of the new navigation view.

```
<UserControl x:Class="Kuando.Control.Modules.GoogleHangouts.Views.GoogleHangoutsNavigation"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006" 
             xmlns:d="http://schemas.microsoft.com/expression/blend/2008" 
             xmlns:mvvm="http://prismlibrary.com/"
             mc:Ignorable="d" 
             mvvm:ViewModelLocator.AutoWireViewModel="True">
```

8. Add XAML to the view to present a button for your navigation. Use the GoogleHangoutsNavigation.xaml as an example if you want to keep the buttons looking the same across modules.
9. Add a new **Class** to the views folder for the ViewModel and name it following the pattern [yourviewname]ViewModel.cs.
10. Add the export attribute to the ViewModel Class and have it implement Prism's **BindableBase** base class.
```
[Export]
public class GoogleHangoutsNavigationViewModel : BindableBase
{
}
```
11. Now that you have a navigation view class defined, go back to the constructor in your module class and tell the region manager to load your navigation view into the navigation region so that your navigation button will show on the UI.

```
[ImportingConstructor]
public GoogleHangoutsModule(IRegionManager regionManager)
{
    this._regionManager = regionManager;
    this._regionManager.RegisterViewWithRegion(Constants.NavigationRegion, () => ServiceLocator.Current.GetInstance<Views.GoogleHangoutsNavigation>());
```

12. Because we are using MEF to dynamically load modules, there is no hard reference from the main application (e.g. **Kuando.Control**) to the module you created. Because of this, Visual Studio does not know about your module so will not copy the necessary .dll files for your module to the main application's output directory to be loaded by MEF. To solve this, you need to add a post-build event to your module's project properties to copy the output files from your module's output directory to the main application's output directory, navigate to the **Build Events** tab of the project properties pages and add the line **xcopy $(TargetFileName) "../../../Kuando.Control/$(OutDir)" /y**. This will force the output files from your module to be copied to the output directory of the main application each time the solution is built thereby allowing MEF to discover your module and load it.

## Settings Section

The process for adding the items for your module's settings is exactly the same as that for the navigation section with the exception of recommended naming of each item. Refer to the Google Hangouts project for guidance on naming conventions.
