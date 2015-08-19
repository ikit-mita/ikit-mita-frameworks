using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Shell;
using IkitMita.Mvvm.ViewModels;

namespace IkitMita.Mvvm.Views
{
    /// <summary>
    /// Interaction logic for ChildViewPresenter.xaml
    /// </summary>
    public partial class ChildViewPresenter
    {
        public ChildViewPresenter()
        {
            InitializeComponent();

            var isBusyBinding = new Binding("IsBusy")
            {
                FallbackValue = false
            };

            SetBinding(IsBusyProperty, isBusyBinding);
        }

        #region ViewResizeMode

        public static readonly DependencyProperty ViewResizeModeProperty = DependencyProperty.RegisterAttached(
            "ViewResizeMode", typeof(ResizeMode), typeof(ChildViewPresenter), new PropertyMetadata(ResizeMode.CanResize));

        public static ResizeMode GetViewResizeMode(DependencyObject element)
        {
            return (ResizeMode)element.GetValue(ViewResizeModeProperty);
        }

        public static void SetViewResizeMode(DependencyObject element, ResizeMode value)
        {
            element.SetValue(ViewResizeModeProperty, value);
        }

        #endregion

        #region ViewMinWidth

        public static readonly DependencyProperty ViewMinWidthProperty = DependencyProperty.RegisterAttached(
            "ViewMinWidth", typeof(double), typeof(ChildViewPresenter), new PropertyMetadata(0d));

        public static void SetViewMinWidth(DependencyObject element, double value)
        {
            element.SetValue(ViewMinWidthProperty, value);
        }

        public static double GetViewMinWidth(DependencyObject element)
        {
            return (double)element.GetValue(ViewMinWidthProperty);
        }

        #endregion

        #region ViewMinHeight

        public static readonly DependencyProperty ViewMinHeightProperty = DependencyProperty.RegisterAttached(
            "ViewMinHeight", typeof(double), typeof(ChildViewPresenter), new PropertyMetadata(0d));

        public static void SetViewMinHeight(DependencyObject element, double value)
        {
            element.SetValue(ViewMinHeightProperty, value);
        }

        public static double GetViewMinHeight(DependencyObject element)
        {
            return (double)element.GetValue(ViewMinHeightProperty);
        }

        #endregion

        #region ViewSizeToContent

        public static readonly DependencyProperty ViewSizeToContentProperty = DependencyProperty.RegisterAttached(
            "ViewSizeToContent", typeof(SizeToContent), typeof(ChildViewPresenter), new PropertyMetadata(SizeToContent.WidthAndHeight));

        public static void SetViewSizeToContent(DependencyObject element, SizeToContent value)
        {
            element.SetValue(ViewSizeToContentProperty, value);
        }

        public static SizeToContent GetViewSizeToContent(DependencyObject element)
        {
            return (SizeToContent)element.GetValue(ViewSizeToContentProperty);
        }

        #endregion ViewSizeToContent

        #region ViewStyle

        public static readonly DependencyProperty ViewStyleProperty = DependencyProperty.RegisterAttached(
            "ViewStyle", typeof(WindowStyle), typeof(ChildViewPresenter), new PropertyMetadata(WindowStyle.SingleBorderWindow));

        public static void SetViewStyle(DependencyObject element, WindowStyle value)
        {
            element.SetValue(ViewStyleProperty, value);
        }

        public static WindowStyle GetViewStyle(DependencyObject element)
        {
            return (WindowStyle)element.GetValue(ViewStyleProperty);
        }

        #endregion ViewStyle
        
        #region IsBusy

        public static readonly DependencyProperty IsBusyProperty = DependencyProperty.Register(
            "IsBusy", typeof(bool), typeof(ChildViewPresenter), new PropertyMetadata(false, OnBusyPropertyChanged));

        private static void OnBusyPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var presenter = dependencyObject as ChildViewPresenter;

            if (presenter != null)
            {
                presenter.TaskbarItemInfo.ProgressState = (bool)dependencyPropertyChangedEventArgs.NewValue
                    ? TaskbarItemProgressState.Indeterminate
                    : TaskbarItemProgressState.None;
            }
        }

        public bool IsBusy
        {
            get { return (bool)GetValue(IsBusyProperty); }
            set { SetValue(IsBusyProperty, value); }
        }

        #endregion

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            if (!((IChildViewModel)DataContext).IsClosed)
            {
                e.Cancel = true;
                Dispatcher.BeginInvoke(new Action(() => ((IChildViewModel)DataContext).Close()));
            }
        }

        public IView View
        {
            get { return Content as IView; }
            set
            {
                SizeToContent = SizeToContent.WidthAndHeight;
                Content = value;
                OnViewChanged();
            }
        }

        protected void OnViewChanged()
        {
            // ReSharper disable once SuspiciousTypeConversion.Global
            var dependencyObject = View as DependencyObject;

            if (dependencyObject != null)
            {
                ResizeMode = GetViewResizeMode(dependencyObject);
                MinWidth = GetViewMinWidth(dependencyObject);
                MinHeight = GetViewMinHeight(dependencyObject);
                SizeToContent = GetViewSizeToContent(dependencyObject);
                WindowStyle = GetViewStyle(dependencyObject);
            }
            else
            {
                ResizeMode = ResizeMode.CanResize;
                MinWidth = 0;
                MinHeight = 0;
                SizeToContent = SizeToContent.Manual;
                WindowStyle = WindowStyle.SingleBorderWindow;
            }
        }
    }
}
