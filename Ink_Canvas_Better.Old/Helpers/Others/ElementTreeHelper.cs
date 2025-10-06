using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Ink_Canvas_Better.Helpers.Others
{
    public static class ElementTreeHelper
    {
        public static bool IsFirstVisualTreeParent(this DependencyObject child, params Type[] types)
        {
            return child.GetFirstVisualTreeParent(types) != null;
        }

        public static DependencyObject GetFirstVisualTreeParent(this DependencyObject child, params Type[] types)
        {
            DependencyObject parent = VisualTreeHelper.GetParent(child);
            while (parent != null)
            {
                foreach (var item in types)
                {
                    if (parent.GetType() == item)
                    {
                        return parent;
                    }
                }
                parent = parent.GetFirstVisualTreeParent();
            }
            return null;
        }

        public static bool IsFirstLogicalTreeParent(this DependencyObject child, params Type[] types)
        {
            return child.GetFirstLogicalTreeParent(types) != null;
        }

        public static DependencyObject GetFirstLogicalTreeParent(this DependencyObject child, params Type[] types)
        {
            DependencyObject parent = LogicalTreeHelper.GetParent(child);
            while (parent != null)
            {
                foreach (var item in types)
                {
                    if (parent.GetType() == item)
                    {
                        return parent;
                    }
                }
                parent = parent.GetFirstLogicalTreeParent();
            }
            return null;
        }
    }
}
