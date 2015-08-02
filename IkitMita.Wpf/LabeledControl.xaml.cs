using System.Windows;
using System.Windows.Markup;
using JetBrains.Annotations;

namespace IkitMita.Wpf
{
    /// <summary>
    /// Interaction logic for LabeledControl.xaml
    /// </summary>
    [ContentProperty("Control")]
    public partial class LabeledControl 
    {
        public LabeledControl()
        {
            InitializeComponent();
            SetGroupIdByParent();
        }

        public static readonly DependencyProperty GroupIdProperty = DependencyProperty<LabeledControl>.Register(c => c.GroupId);
        public static readonly DependencyProperty LabelProperty = DependencyProperty<LabeledControl>.Register(c => c.Label);
        public static readonly DependencyProperty ControlProperty = DependencyProperty<LabeledControl>.Register(c => c.Control);

        public string GroupId
        {
            get { return (string)GetValue(GroupIdProperty); }
            set { SetValue(GroupIdProperty, value); }
        }

        public string Label
        {
            get { return (string)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }

        public object Control
        {
            get { return GetValue(ControlProperty); }
            set { SetValue(ControlProperty, value); }
        }

        protected override void OnVisualParentChanged(DependencyObject oldParent)
        {
            base.OnVisualParentChanged(oldParent);
            SetGroupIdByParent();
        }

        private void SetGroupIdByParent(DependencyObject oldParent = null)
        {
            if (Parent == null)
            {
                return;
            }

            if (oldParent != null && GroupId != null && GroupId == GetGroupId(oldParent))
            {
                return;
            }

            GroupId = GetGroupId(Parent);
        }

        [NotNull]
        private static string GetGroupId([NotNull]DependencyObject obj)
        {
            return "_gr_" + obj.GetHashCode();
        }
    }
}
