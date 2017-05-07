using System.Windows.Media;

namespace diagramlib
{
    public interface IColorSelector
    {
        Brush SelectedBrush(object item, int index);
    }
}