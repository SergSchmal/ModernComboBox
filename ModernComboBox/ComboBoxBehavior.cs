using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Media;

namespace ModernComboBox
{
    public class ComboBoxBehavior : Behavior<ComboBox>
    {
        protected override void OnAttached()
        {
            AssociatedObject.DropDownClosed += AssociatedObjectOnDropDownClosed;
            AssociatedObject.SelectionChanged += AssociatedObject_SelectionChanged;
        }

        private void AssociatedObject_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var combo = sender as ComboBox;

            if (combo != null)
            {
                combo.IsDropDownOpen = true;
            }
        }

        private void AssociatedObjectOnDropDownClosed(object sender, EventArgs eventArgs)
        {
            var parent = VisualTreeHelper.GetParent((DependencyObject) sender);
            Point m = Mouse.GetPosition((IInputElement) parent);
            VisualTreeHelper.HitTest((Visual) parent, FilterCallback, ResultCallback, new PointHitTestParameters(m));
        }

        private HitTestFilterBehavior FilterCallback(DependencyObject o)
        {
            var c = o as Control;
            if (o is MainWindow || c == null || !c.Focusable) return HitTestFilterBehavior.Continue;
            if (c is ComboBox box && !box.IsDropDownOpen)
            {
                // This go exception
              //box.IsDropDownOpen = true;
            }
            else
            {
                var mouseDevice = Mouse.PrimaryDevice;
                var mouseButtonEventArgs = new MouseButtonEventArgs(mouseDevice, 0, MouseButton.Left)
                {
                    RoutedEvent = Mouse.MouseDownEvent,
                    Source = c
                };
                c.RaiseEvent(mouseButtonEventArgs);
            }

            return HitTestFilterBehavior.Stop;
        }

        private HitTestResultBehavior ResultCallback(HitTestResult r)
        {
            return HitTestResultBehavior.Continue;
        }
    }
}