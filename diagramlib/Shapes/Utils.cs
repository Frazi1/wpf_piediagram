using System;
using System.Windows;

namespace diagramlib
{
    internal static class Utils
    {
        public static Point CartesianCoordinate(double angle, double radius)
        {
            double angleRad = ToRadians(angle);
            double x = radius * Math.Cos(angleRad);
            double y = radius * Math.Sin(angleRad);
            return new Point(x,y);
        }

        private static double ToRadians(double rotationAngleDegrees)
        {
            return rotationAngleDegrees * Math.PI / 180;
        }
    }
}