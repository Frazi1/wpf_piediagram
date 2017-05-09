using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
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
        /// <summary>
        /// Размер дырки внутри пирога относительно размера всего пирога (значение от 0 до 1)
        /// </summary>
        public static readonly DependencyProperty HoleSizeProperty = DependencyProperty.Register(
            "HoleSize", typeof(double), typeof(Plotter), new PropertyMetadata(default(double)));

        /// <summary>
        /// Размер дырки внутри пирога относительно размера всего пирога (значение от 0 до 1)
        /// </summary>
        public double HoleSize
        {
            get { return (double) GetValue(HoleSizeProperty); }
            set
            {
                double correctValue = value < 0 ? 0 : value > 1 ? 1 : value;
                SetValue(HoleSizeProperty, correctValue);
            }
        }

        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register(
            "ItemsSource", typeof(IEnumerable), typeof(Plotter), new PropertyMetadata(OnItemsSourcePropertyChanged));

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable) GetValue(ItemsSourceProperty); }
            set
            {
                SetValue(ItemsSourceProperty, value);
            }
        }

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

        public static readonly DependencyProperty ColorSelectorProperty = DependencyProperty.Register(
            "ColorSelector", typeof(IColorSelector), typeof(Plotter), new PropertyMetadata(default(IColorSelector)));


        public IColorSelector ColorSelector
        {
            get { return (IColorSelector) GetValue(ColorSelectorProperty); }
            set { SetValue(ColorSelectorProperty, value); }
        }

        public Plotter()
        {
            InitializeComponent();
            //DependencyPropertyDescriptor dpd =
            //    DependencyPropertyDescriptor.FromProperty(DependentValueNameProperty, typeof(Plotter));
            //dpd.AddValueChanged(this, DependentValueChanged);
            this.DataContextChanged += Plotter_DataContextChanged;
        }

        private void ConstructPiePieces()
        {
            CollectionView collectionView = (CollectionView) CollectionViewSource.GetDefaultView(this.DataContext);

            double halfWidth = this.Width / 2;
            double innerRadius = halfWidth;
            double holeSize = halfWidth * HoleSize;
            double total = 0;
            foreach (var item in (IEnumerable) this.DataContext)
            {
                total += (double) PieChartLayout.GetPropertyValue(item, DependentValueName);
            }

            Canvas1.Children.Clear();
            double accumulativeAngle = 0;

            int index = 0;
            foreach (object item in (IEnumerable) this.DataContext)
            {
                double value = (double) PieChartLayout.GetPropertyValue(item, DependentValueName);
                string name = (string) PieChartLayout.GetPropertyValue(item, IndependentValueName);

                double percentage = value / total * 100;
                double wedgeAngle = value * 360 / total;
                bool pushOut = collectionView.CurrentItem == item;
                PiePiece piece = new PiePiece()
                {
                    Name = name,
                    Radius = halfWidth,
                    InnerRadius = holeSize,
                    CenterX = halfWidth,
                    CenterY = halfWidth,
                    WedgeAngle = wedgeAngle,
                    RotationAngle = accumulativeAngle,
                    Percentage = percentage,
                    Fill = ColorSelector != null ? ColorSelector.SelectedBrush(item, index) : Brushes.Green,
                    PushOut = pushOut ? 10.0 : 0.0,
                    Stroke = Brushes.Black,
                    Tag = index
                };
                Canvas1.Children.Insert(0, piece);
                accumulativeAngle += wedgeAngle;

                piece.MouseUp += Piece_MouseUp;
                piece.ToolTipOpening += Piece_ToolTipOpening;

                index++;
            }
        }

        private void Piece_ToolTipOpening(object sender, ToolTipEventArgs e)
        {
            PiePiece piece = (PiePiece) sender;
            ToolTip tip = (ToolTip) piece.ToolTip;
            tip.DataContext = piece;
        }
        private void Piece_MouseUp(object sender, MouseButtonEventArgs e)
        {
            CollectionView collectionView = (CollectionView) CollectionViewSource.GetDefaultView(this.DataContext);
            PiePiece piePiece = (PiePiece) sender;
            int index = (int) piePiece.Tag;
            if (collectionView.CurrentPosition == index)
                collectionView.MoveCurrentTo(-1);
            else
                collectionView.MoveCurrentToPosition(index);
            ConstructPiePieces();
        }
        private void DependentValueChanged(object sender, EventArgs e)
        {
            ConstructPiePieces();
        }
        private void Plotter_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ConstructPiePieces();
        }
        private static void OnItemsSourcePropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var control = sender as Plotter;
            if (control != null)
                control.OnItemsSourceChanged((IEnumerable) e.OldValue, (IEnumerable) e.NewValue);
        }
        private void OnItemsSourceChanged(IEnumerable oldValue, IEnumerable newValue)
        {
            DataContext = ItemsSource;
        }
    }
}
