using System.Windows;
using System.Windows.Media;

namespace diagramlib
{
    public class IndexedColorSelector : DependencyObject, IColorSelector
    {
        public static readonly DependencyProperty BrushesProperty = DependencyProperty.Register(
            "Brushes", typeof(Brush[]), typeof(IndexedColorSelector), new UIPropertyMetadata(null));

        public Brush[] Brushes
        {
            get { return (Brush[]) GetValue(BrushesProperty); }
            set { SetValue(BrushesProperty, value); }
        }

        public Brush SelectedBrush(object item, int index)
        {
            if (Brushes == null || Brushes.Length == 0)
                return System.Windows.Media.Brushes.Black;
            return Brushes[index % Brushes.Length];
        }
    }
}