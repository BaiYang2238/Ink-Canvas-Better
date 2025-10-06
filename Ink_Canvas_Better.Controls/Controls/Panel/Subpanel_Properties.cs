using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace Ink_Canvas_Better.Controls.Controls.Panel
{
    public partial class Subpanel
    {
        #region Title

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(Subpanel), new PropertyMetadata("Title"));

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        #endregion

        #region IsShowHeader

        public static readonly DependencyProperty IsShowHeaderProperty =
            DependencyProperty.Register("ShowHeader", typeof(bool), typeof(Subpanel), new PropertyMetadata(true));

        public bool IsShowHeader
        {
            get { return (bool)GetValue(IsShowHeaderProperty); }
            set { SetValue(IsShowHeaderProperty, value); }
        }

        #endregion

        private Binding titleBinding;
        private Binding isShowHeaderBinding;

        private Binding childBinding;
        private Binding marginBinding;
        private Binding staysOpenBinding;
    }
}
