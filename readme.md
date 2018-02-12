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

1. Add a new project to **src** folder and name it following the pattern Kuando.Control.Modules._yourmodulename_.
2. Add NuGet packages to your new project for  Prism.Wpf and Prism.Mef
3. Add a reference to the .Net assembly System.ComponentModel.Composition
4. As a convention, the name of the main class of a new Prism module is post-fixed with the word **module**. Add the module export attribute to the module class of your new module and have it implement Prism's **IModule** interface.

```
   [ModuleExport(typeof(GoogleHangoutsModule))]
   public class GoogleHangoutsModule : IModule
   {
       public void Initialize()
       {
       }
   }
```
5. Add a views folder
6. Add a new WPF user control to the views folder to be used as the navigation view. Do not post-fix this file with the word **View**. The automatic ViewModel wiring between the View and the ViewModel is using a convention approach where the ViewModel will use whatever the name is for the View and try to locate a file with the same name post-fixed with the word **ViewModel** in the same folder as the view. The navigation view is simply used to show a navigation button on the left side of the application's dialog window to switch to your view when the button is clicked.
7. Add a new class to the views folder for the ViewModel and name it following the pattern _yourviewname_ ViewModel.cs
8. 


This project is still under development.....

Adding a new module:
1. Add a new project following the nameing pattern Kuando.Control.Modules.<modulename>
2. Add NuGet packages Prism.Wpf and Prism.Mef
3. Add a reference to the .Net assembly System.ComponentModel.Composition
4. Add module export attribute to the new project's main class (e.g. [ModuleExport(typeof(Class1))])
5. Descend the new project's main class from IModule and implement interface
6. Add Views folder
7. Add a new WPF user control to the views folder to be used as the navigation view
8. Add a new view model class to the views folder and name it following the pattern <viewname>ViewModel.cs
9. Descend the view model from BindableBase
10. Add [Export] attribute to view model class
11. Add a constructor to the module class, add [ImportingConstructor] attribute, and add IRegionManager to the constructors parameter list
12. In the module's constructor, add your new navigation view to the NavigationRegion
13. Add mvvm:ViewModelLocator.AutoWireViewModel="True" to view's xaml
14. Add post-build event
15. Add a new WPF user control to the views folder to be used as the settings view
16. Repeat steps 8-13
