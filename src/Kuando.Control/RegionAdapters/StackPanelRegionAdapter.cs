using Prism.Regions;
using System.Collections.Specialized;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Controls;

namespace Kuando.Control.RegionAdapters
{
    [Export]
    public class StackPanelRegionAdapter : RegionAdapterBase<StackPanel>
    {
        #region Constructors

        [ImportingConstructor]
        public StackPanelRegionAdapter(IRegionBehaviorFactory regionBehaviorFactory) : base(regionBehaviorFactory)
        {
        }

        #endregion

        #region Methods

        protected override void Adapt(IRegion region, StackPanel regionTarget)
        {
            region.Views.CollectionChanged += (s, e) =>
            {
                switch (e.Action)
                {
                    case NotifyCollectionChangedAction.Add:
                        foreach (FrameworkElement element in e.NewItems)
                        {
                            regionTarget.Children.Add(element);
                        }
                        break;
                    case NotifyCollectionChangedAction.Remove:
                        foreach (FrameworkElement element in e.OldItems)
                        {
                            if (regionTarget.Children.Contains(element))
                            {
                                regionTarget.Children.Remove(element);
                            }
                        }
                        break;
                }
            };
        }

        protected override IRegion CreateRegion()
        {
            return new AllActiveRegion();
        }

        #endregion
    }
}
