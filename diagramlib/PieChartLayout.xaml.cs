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

namespace diagramlib
{
    /// <summary>
    /// Логика взаимодействия для PieChartLayout.xaml
    /// </summary>
    public partial class PieChartLayout : UserControl
    {

        public static readonly DependencyProperty IndependentValueNameProperty = DependencyProperty.Register(
            "IndependentValueName", typeof(string), typeof(PieChartLayout),
            new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.Inherits));

        public string IndependentValueName
        {
            get { return GetIndependentValueProperty(this); }
            set { SetIndependentValueProperty(this, value); }
        }

        public static readonly DependencyProperty DependentValueNameProperty = DependencyProperty.Register(
            "DependentValueName", typeof(string), typeof(PieChartLayout),
            new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.Inherits));

        public string DependentValueName
        {
            get { return GetDependentValueProperty(this); }
            set { SetDependentValueProperty(this, value); }
        }

        public static readonly DependencyProperty ColorSelectorProperty = DependencyProperty.Register(
            "ColorSelector", typeof(IColorSelector), typeof(PieChartLayout),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));

        public IColorSelector ColorSelector
        {
            get { return GetColorSelector(this); }
            set { SetColorSelector(this, value); }
        }

        public static readonly DependencyProperty ItemsProperty = DependencyProperty.Register(
            "Items", typeof(IEnumerable), typeof(PieChartLayout),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));

        public IEnumerable Items
        {
            get { return GetItemsProperty(this); }
            set { SetItemsProperty(this,value); }
        }

        internal static void SetDependentValueProperty(UIElement uiElement, string value)
        {
            uiElement.SetValue(DependentValueNameProperty, value);
        }
        internal static string GetDependentValueProperty(UIElement uiElement)
        {
            return (string) uiElement.GetValue(DependentValueNameProperty);
        }
        internal static void SetIndependentValueProperty(UIElement uiElement, string value)
        {
            uiElement.SetValue(IndependentValueNameProperty, value);
        }
        internal static string GetIndependentValueProperty(UIElement uiElement)
        {
            return (string) uiElement.GetValue(IndependentValueNameProperty);
        }
        internal static void SetColorSelector(UIElement uiElement, IColorSelector value)
        {
            uiElement.SetValue(ColorSelectorProperty, value);
        }
        internal static IColorSelector GetColorSelector(UIElement uiElement)
        {
            return (IColorSelector) uiElement.GetValue(ColorSelectorProperty);
        }
        internal static IEnumerable GetItemsProperty(UIElement uiElement)
        {
            return (IEnumerable) uiElement.GetValue(ItemsProperty);
        }
        internal static void SetItemsProperty(UIElement uiElement, IEnumerable value)
        {
            uiElement.SetValue(ItemsProperty, value);
        }

        public PieChartLayout()
        {
            InitializeComponent();
        }

        public static object GetPropertyValue(object item, string dependentValueName)
        {
            PropertyDescriptorCollection filterPropDesc = TypeDescriptor.GetProperties(item);
            object itemValue = filterPropDesc[dependentValueName].GetValue(item);
            return itemValue;
        }
    }
}
