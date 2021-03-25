using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace VirtualWorkspace_Mirzaie_Kim.WPF.Controls.Subcontrols
{
    public class InteractableItem : Control
    {
        public string ItemName
        {
            get { return (string)GetValue(ItemNameProperty); }
            set { SetValue(ItemNameProperty, value); }
        }

        public static readonly DependencyProperty ItemNameProperty =
            DependencyProperty.Register("ItemName", typeof(string), typeof(InteractableItem), new PropertyMetadata(null));

        public int ItemId
        {
            get { return (int)GetValue(ItemIdProperty); }
            set { SetValue(ItemIdProperty, value); }
        }

        public static readonly DependencyProperty ItemIdProperty =
            DependencyProperty.Register("ItemId", typeof(int), typeof(InteractableItem), new PropertyMetadata(null));

        public string Path
        {
            get { return (string)GetValue(PathProperty); }
            set { SetValue(PathProperty, value); }
        }

        public static readonly DependencyProperty PathProperty =
            DependencyProperty.Register("Path", typeof(string), typeof(InteractableItem), new PropertyMetadata(null));

        static InteractableItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(InteractableItem), new FrameworkPropertyMetadata(typeof(InteractableItem)));
        }

        public string IconSource
        {
            get { return (string)GetValue(IconSourceProperty); }
            set { SetValue(IconSourceProperty, value); }
        }

        public static readonly DependencyProperty IconSourceProperty =
            DependencyProperty.Register("IconSource", typeof(string), typeof(InteractableItem), new PropertyMetadata(null));

        public int IconWidth
        {
            get { return (int)GetValue(IconWidthProperty); }
            set { SetValue(IconWidthProperty, value); }
        }

        public static readonly DependencyProperty IconWidthProperty =
            DependencyProperty.Register("IconWidth", typeof(int), typeof(InteractableItem), new PropertyMetadata(25));

        /// <summary>
        /// Every interactable will start the process on double-click.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDoubleClick(MouseButtonEventArgs e)
        {
            base.OnMouseDoubleClick(e);

            if (Path != null)
            {
                new Process
                {
                    StartInfo = new ProcessStartInfo(Path)
                    {
                        UseShellExecute = true
                    }
                }.Start();
            }
        }
    }
}
