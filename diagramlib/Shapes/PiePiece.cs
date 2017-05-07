using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace diagramlib.Shapes
{
    public class PiePiece : Shape
    {
        #region DependecyPropertries

        public static readonly DependencyProperty RadiusProperty = DependencyProperty.Register(
            "Radius", typeof(double), typeof(PiePiece),
            new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));

        public double Radius
        {
            get { return (double) GetValue(RadiusProperty); }
            set { SetValue(RadiusProperty, value); }
        }

        public static readonly DependencyProperty InnerRadiusProperty = DependencyProperty.Register(
            "InnerRadius", typeof(double), typeof(PiePiece),
            new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));

        public double InnerRadius
        {
            get { return (double) GetValue(InnerRadiusProperty); }
            set { SetValue(InnerRadiusProperty, value); }
        }

        public static readonly DependencyProperty WedgeAngleProperty = DependencyProperty.Register(
            "WedgeAngle", typeof(double), typeof(PiePiece),
            new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));

        public double WedgeAngle
        {
            get { return (double) GetValue(WedgeAngleProperty); }
            set
            {
                double correctValue = value % 360;
                SetValue(WedgeAngleProperty, correctValue);
                Percentage = correctValue / 360.0;
            }
        }

        public static readonly DependencyProperty PercentageProperty = DependencyProperty.Register(
            "Percentage", typeof(double), typeof(PiePiece),
            new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));

        public double Percentage
        {
            get { return (double) GetValue(PercentageProperty); }
            set { SetValue(PercentageProperty, value); }
        }

        public static readonly DependencyProperty RotationAngleProperty = DependencyProperty.Register(
            "RotationAngle", typeof(double), typeof(PiePiece),
            new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));

        public double RotationAngle
        {
            get { return (double) GetValue(RotationAngleProperty); }
            set { SetValue(RotationAngleProperty, value); }
        }

        public static readonly DependencyProperty CenterXProperty = DependencyProperty.Register(
            "CenterX", typeof(double), typeof(PiePiece),
            new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));

        public double CenterX
        {
            get { return (double) GetValue(CenterXProperty); }
            set { SetValue(CenterXProperty, value); }
        }

        public static readonly DependencyProperty CenterYProperty = DependencyProperty.Register(
            "CenterY", typeof(double), typeof(PiePiece),
            new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));

        public double CenterY
        {
            get { return (double) GetValue(CenterYProperty); }
            set { SetValue(CenterYProperty, value); }
        }

        public static readonly DependencyProperty PieceValueProperty = DependencyProperty.Register(
            "PieceValue", typeof(double), typeof(PiePiece),
            new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));

        public double PieceValue
        {
            get { return (double) GetValue(PieceValueProperty); }
            set { SetValue(PieceValueProperty, value); }
        }

        protected override Geometry DefiningGeometry
        {
            get
            {
                StreamGeometry geometry = new StreamGeometry();
                geometry.FillRule = FillRule.EvenOdd;
                using (StreamGeometryContext context = geometry.Open())
                {
                    DrawGeometry(context);
                    //DrawTest(context);
                }
                geometry.Freeze();
                return geometry;
            }
        }

        private void DrawGeometry(StreamGeometryContext context)
        {
            Point startPoint = new Point(CenterX, CenterY);

            Point innerArcStartPoint =
                Utils.CartesianCoordinate(RotationAngle, InnerRadius);
            innerArcStartPoint.Offset(CenterX, CenterY);

            Point innerArcEndPoint =
                Utils.CartesianCoordinate(RotationAngle + WedgeAngle, InnerRadius);
            innerArcEndPoint.Offset(CenterX, CenterY);

            Point outerArcStartPoint =
                Utils.CartesianCoordinate(RotationAngle, Radius);
            outerArcStartPoint.Offset(CenterX, CenterY);

            Point outerArcEndPoint =
                Utils.CartesianCoordinate(RotationAngle + WedgeAngle, Radius);
            outerArcEndPoint.Offset(CenterX, CenterY);

            bool largeArc = WedgeAngle > 180.0;

            Size outerArcSize = new Size(Radius, Radius);
            Size innerArcSize = new Size(InnerRadius, InnerRadius);

            context.BeginFigure(outerArcStartPoint, true, true);
            context.ArcTo(outerArcEndPoint, outerArcSize, 0, largeArc, SweepDirection.Clockwise, true, true);
            context.LineTo(innerArcEndPoint, true, true);
            context.ArcTo(innerArcStartPoint, innerArcSize, 0, largeArc, SweepDirection.Counterclockwise, true, true);
            context.Close();
        }

        private void DrawTest(StreamGeometryContext context)
        {
            Point startPoint = new Point(CenterX, CenterY);

            Point outerArcStartPoint =
                Utils.CartesianCoordinate(RotationAngle, Radius);
            outerArcStartPoint.Offset(CenterX, CenterY);

            Point outerArcEndPoint =
                Utils.CartesianCoordinate(RotationAngle + WedgeAngle, Radius);
            outerArcEndPoint.Offset(CenterX, CenterY);

            bool largeArc = WedgeAngle > 180.0;

            Size outerArcSize = new Size(Radius, Radius);
            Size innerArcSize = new Size(InnerRadius, InnerRadius);

            context.BeginFigure(startPoint, true, true);
            context.LineTo(outerArcStartPoint, true, true);
            context.ArcTo(outerArcEndPoint, outerArcSize, 0, largeArc, SweepDirection.Clockwise, true, true);
            context.LineTo(startPoint, true, true);
            context.Close();
        }

        #endregion


    }
}