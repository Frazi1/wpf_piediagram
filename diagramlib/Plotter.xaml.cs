using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using diagramlib.Shapes;

namespace diagramlib
{
    /// <summary>
    /// Логика взаимодействия для Plotter.xaml
    /// </summary>
    public partial class Plotter : UserControl
    {
        public static readonly DependencyProperty IndependentValueNameProperty = DependencyProperty.Register(
            "IndependentValueName", typeof(string), typeof(Plotter), new PropertyMetadata(default(string)));

        public string IndependentValueName
        {
            get { return (string) GetValue(IndependentValueNameProperty); }
            set { SetValue(IndependentValueNameProperty, value); }
        }

        public static readonly DependencyProperty DependentValueNameProperty = DependencyProperty.Register(
            "DependentValueName", typeof(string), typeof(Plotter), new PropertyMetadata(default(string)));

        public string DependentValueName
        {
            get { return (string) GetValue(DependentValueNameProperty); }
            set { SetValue(DependentValueNameProperty, value); }
        }

        public static readonly DependencyProperty ItemsProperty = DependencyProperty.Register(
            "Items", typeof(IEnumerable), typeof(Plotter), new PropertyMetadata(default(IEnumerable)));

        public IEnumerable Items
        {
            get { return (IEnumerable) GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        public static readonly DependencyProperty ColorSelectorProperty = DependencyProperty.Register(
            "ColorSelector", typeof(IColorSelector), typeof(Plotter), new PropertyMetadata(default(IColorSelector)));

        public IColorSelector ColorSelector
        {
            get { return (IColorSelector) GetValue(ColorSelectorProperty); }
            set { SetValue(ColorSelectorProperty, value); }
        }

        public void ConstructPiePieces()
        {
            double halfWidth = this.Width / 2;
            double innerRadius = halfWidth;

            double total = 0;
            foreach (var item in Items)
            {
                total += PieChartLayout.GetPlottedPropertyValue(item, DependentValueName);
            }
            Canvas1.Children.Clear();
            double accumulativeAngle = 0;

            int index = 0;
            foreach (object item in Items)
            {
                double wedgeAngle = PieChartLayout.GetPlottedPropertyValue(item, DependentValueName) * 360 / total;

                PiePiece piece = new PiePiece()
                {
                    Radius = halfWidth,
                    InnerRadius = innerRadius / 2,
                    CenterX = halfWidth,
                    CenterY = halfWidth,
                    WedgeAngle = wedgeAngle,
                    RotationAngle = accumulativeAngle,
                    Fill = ColorSelector != null ? ColorSelector.SelectedBrush(item, index) : Brushes.Green,
                    Stroke = Brushes.Black
                };
                Canvas1.Children.Insert(0, piece);
                accumulativeAngle += wedgeAngle;
                index++;
            }
        }

        public Plotter()
        {
            InitializeComponent();
        }
    }
}
