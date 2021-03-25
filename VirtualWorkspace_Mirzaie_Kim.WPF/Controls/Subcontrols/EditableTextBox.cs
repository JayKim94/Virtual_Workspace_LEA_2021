using System;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace VirtualWorkspace_Mirzaie_Kim.WPF.Controls.Subcontrols
{
    [TemplatePart(Name = "PART_HintText", Type = typeof(Label))]
    [TemplatePart(Name = "PART_IconActionButton", Type = typeof(IconActionButton))]
    [TemplatePart(Name = "PART_CurrentValue", Type = typeof(TextBox))]
    public class EditableTextBox : Control
    {
        private Label part_HintText;
        private IconActionButton part_IconActionButton;
        private TextBox part_CurrentValue;

        public string CurrentValue
        {
            get { return (string)GetValue(CurrentValueProperty); }
            set { SetValue(CurrentValueProperty, value); }
        }

        public static readonly DependencyProperty CurrentValueProperty =
            DependencyProperty.Register("CurrentValue", typeof(string), typeof(EditableTextBox), new PropertyMetadata(null));

        public ICommand SubmitCommand
        {
            get { return (ICommand)GetValue(SubmitCommandProperty); }
            set { SetValue(SubmitCommandProperty, value); }
        }

        public static readonly DependencyProperty SubmitCommandProperty =
            DependencyProperty.Register("SubmitCommand", typeof(ICommand), typeof(EditableTextBox), new PropertyMetadata(null));
        
        static EditableTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(EditableTextBox), new FrameworkPropertyMetadata(typeof(EditableTextBox)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            part_HintText = GetTemplateChild("PART_HintText") as Label;
            part_IconActionButton = GetTemplateChild("PART_IconActionButton") as IconActionButton;
            part_CurrentValue = GetTemplateChild("PART_CurrentValue") as TextBox;

            part_CurrentValue.GotFocus += TextBoxFocused;
            part_CurrentValue.LostFocus += TextBoxLostFocus;
        }

        private void TextBoxFocused(object sender, RoutedEventArgs e)
        {
            part_HintText.Visibility = Visibility.Visible;
            part_IconActionButton.Visibility = Visibility.Collapsed;
        }

        private void TextBoxLostFocus(object sender, RoutedEventArgs e)
        {
            if (CurrentValue == null) return;

            if (CurrentValue != string.Empty && CurrentValue.Length < 25)
                SubmitCommand.Execute(CurrentValue);

            part_HintText.Visibility = Visibility.Collapsed;
            part_IconActionButton.Visibility = Visibility.Visible;
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            
            if (e.Key != Key.Enter) return;
            
            FocusManager.SetFocusedElement(FocusManager.GetFocusScope(part_CurrentValue), null);
            Keyboard.ClearFocus();
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);

            part_HintText.Visibility = Visibility.Visible;
            part_IconActionButton.Visibility = Visibility.Collapsed;
            part_CurrentValue.Focus();
        }
    }
}
