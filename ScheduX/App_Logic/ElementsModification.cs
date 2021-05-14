using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace ScheduX.App_Logic
{
    public static class ElementsModification
    {
        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }
        public static void ResetControlText<T>(Window currentWindow) where T : DependencyObject
        {
            currentWindow.Visibility = Visibility.Hidden;
            foreach (T item in FindVisualChildren<T>(currentWindow))
            {
                //item.ClearValue(TextBlock.TextProperty);
                LocalValueEnumerator locallySetProperties = item.GetLocalValueEnumerator();
                while (locallySetProperties.MoveNext())
                {
                    DependencyProperty propertyToClear = locallySetProperties.Current.Property;
                    if (propertyToClear.Name == "Text")
                    {
                        item.ClearValue(propertyToClear);
                    }
                }
            }
        }
        public static void HideChildWindow(Window currentWindow)
        {
            for (int i = 0; i < currentWindow.OwnedWindows.Count; i++)
            {
                currentWindow.OwnedWindows[i].Visibility = Visibility.Hidden;
            }
            currentWindow.Visibility = Visibility.Hidden;
        }
    }
}
