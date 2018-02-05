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
