using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace VirtualWorkspace_Mirzaie_Kim.WPF.Controls.Subcontrols
{
    public class WorkspaceListRow : Control
    {
        public string WorkspaceName
        {
            get { return (string)GetValue(WorkspaceNameProperty); }
            set { SetValue(WorkspaceNameProperty, value); }
        }

        public static readonly DependencyProperty WorkspaceNameProperty =
            DependencyProperty.Register("WorkspaceName", typeof(string), typeof(WorkspaceListRow), new PropertyMetadata(null));

        public int WorkspaceId
        {
            get { return (int)GetValue(WorkspaceIdProperty); }
            set { SetValue(WorkspaceIdProperty, value); }
        }

        public static readonly DependencyProperty WorkspaceIdProperty =
            DependencyProperty.Register("WorkspaceId", typeof(int), typeof(WorkspaceListRow), new PropertyMetadata(null));

        public ICommand RemoveWorkspaceCommand
        {
            get { return (ICommand)GetValue(RemoveWorkspaceCommandProperty); }
            set { SetValue(RemoveWorkspaceCommandProperty, value); }
        }

        public static readonly DependencyProperty RemoveWorkspaceCommandProperty =
            DependencyProperty.Register("RemoveWorkspaceCommand", typeof(ICommand), typeof(WorkspaceListRow), new PropertyMetadata(null));

        public ICommand SetWorkspaceCommand
        {
            get { return (ICommand)GetValue(SetWorkspaceCommandProperty); }
            set { SetValue(SetWorkspaceCommandProperty, value); }
        }

        public static readonly DependencyProperty SetWorkspaceCommandProperty =
            DependencyProperty.Register("SetWorkspaceCommand", typeof(ICommand), typeof(WorkspaceListRow), new PropertyMetadata(null));

        public SolidColorBrush RowForeground
        {
            get { return (SolidColorBrush)GetValue(RowForegroundProperty); }
            set { SetValue(RowForegroundProperty, value); }
        }

        public static readonly DependencyProperty RowForegroundProperty =
            DependencyProperty.Register("RowForeground", typeof(SolidColorBrush), typeof(WorkspaceListRow), new PropertyMetadata(new SolidColorBrush(Colors.WhiteSmoke)));

        static WorkspaceListRow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(WorkspaceListRow), new FrameworkPropertyMetadata(typeof(WorkspaceListRow)));
        }
    }
}
