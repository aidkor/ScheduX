using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;

namespace ScheduX.Resourses.UILogic
{
    public static class UITools
    {
        [DllImport("user32.dll")]
        static extern int GetWindowLong(IntPtr hwnd, int index);

        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hwnd, int index, int newStyle);

        [DllImport("user32.dll")]
        static extern bool SetWindowPos(IntPtr hwnd, IntPtr hwndInsertAfter,
                   int x, int y, int width, int height, uint flags);

        [DllImport("user32.dll")]
        static extern IntPtr SendMessage(IntPtr hwnd, uint msg,
                   IntPtr wParam, IntPtr lParam);

        const int GWL_EXSTYLE = -20;
        const int WS_EX_DLGMODALFRAME = 0x0001;
        const int SWP_NOSIZE = 0x0001;
        const int SWP_NOMOVE = 0x0002;
        const int SWP_NOZORDER = 0x0004;
        const int SWP_FRAMECHANGED = 0x0020;
        const uint WM_SETICON = 0x0080;

        public static void RemoveIcon(Window window)
        {
            // Get this window's handle
            IntPtr hwnd = new WindowInteropHelper(window).Handle;

            // Change the extended window style to not show a window icon
            int extendedStyle = GetWindowLong(hwnd, GWL_EXSTYLE);
            SetWindowLong(hwnd, GWL_EXSTYLE, extendedStyle | WS_EX_DLGMODALFRAME);

            // Update the window's non-client area to reflect the changes
            SetWindowPos(hwnd, IntPtr.Zero, 0, 0, 0, 0, SWP_NOMOVE |
                  SWP_NOSIZE | SWP_NOZORDER | SWP_FRAMECHANGED);
        }
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
        public static SolidColorBrush GetGreenColor()
        {
            return new BrushConverter().ConvertFrom("#A8D66D") as SolidColorBrush;
        }
        public static SolidColorBrush GetGreyColor()
        {
            return new BrushConverter().ConvertFrom("#B5C1D3") as SolidColorBrush;
        }
        public static SolidColorBrush GetDarkGreyColor()
        {
            return new BrushConverter().ConvertFrom("#595959") as SolidColorBrush;
        }
        public static SolidColorBrush GetRedColor()
        {
            return new BrushConverter().ConvertFrom("#FFC82929") as SolidColorBrush;
        }
      
    }
}
