using System.Windows.Media;

namespace ScheduX.App_Logic
{
    public static class ColorPalette
    {
        public static SolidColorBrush GetPredefinedColor(string color)
        {
            return new BrushConverter().ConvertFrom(color) as SolidColorBrush;
        }
    }
}
