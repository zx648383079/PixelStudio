using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.Xaml.Interactivity;
using System.Collections.Generic;
using System.Windows.Input;

namespace ZoDream.PixelStudio.Behaviors
{
    public class SelectionChangedBehavior: Behavior<Control>
    {
        public ICommand Command {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Command.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand), typeof(SelectionChangedBehavior), new PropertyMetadata(null));

        protected override void OnAttached()
        {
            base.OnAttached();
            if (AssociatedObject is TreeView tree)
            {
                tree.SelectionChanged += TreeView_SelectionChanged;
            } else if (AssociatedObject is Selector s)
            {
                s.SelectionChanged += Selector_SelectionChanged; ;
            }
        }

        private void Selector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ListViewBase v)
            {
                Command?.Execute(v.SelectedItems.AsReadOnly());
                return;
            }
            if (sender is Selector o)
            {
                Command?.Execute(new object[] { o.SelectedItem });
            }
        }

        private void TreeView_SelectionChanged(TreeView sender, TreeViewSelectionChangedEventArgs args)
        {
            Command?.Execute(args.AddedItems.AsReadOnly());
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            if (AssociatedObject is TreeView tree)
            {
                tree.SelectionChanged -= TreeView_SelectionChanged;
            }
            else if (AssociatedObject is Selector s)
            {
                s.SelectionChanged -= Selector_SelectionChanged; ;
            }
        }
    }
}
