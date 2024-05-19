using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Rights.Helpers
{
    /// <summary>
    /// Визуальный помощник
    /// </summary>
    public static class VisualHelper
    {
        /// <summary>
        /// Ищет все элементы управления с типом T в переданном элементе управления
        /// </summary>
        /// <typeparam name="T">Тип искомого элемента управления</typeparam>
        /// <param name="depObj">Элемент управления</param>
        /// <returns>Перечисление найденных элементов управления</returns>
        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj == null) yield return (T)Enumerable.Empty<T>();
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                DependencyObject ithChild = VisualTreeHelper.GetChild(depObj, i);
                if (ithChild == null) continue;
                if (ithChild is T t) yield return t;
                foreach (T childOfChild in FindVisualChildren<T>(ithChild)) yield return childOfChild;
            }
        }

        /// <summary>
        /// Делает элементы управления доступными только для чтения в окне или элементе управления
        /// </summary>
        /// <param name="depObj">Элемент управления в котором следует отключить другие элементы управления</param>
        public static void MakeOnlyReadableControls(DependencyObject depObj)
        {
            foreach (TextBox tb in FindVisualChildren<TextBox>(depObj))
            {
                // Убираем обводку
                tb.BorderThickness = new Thickness(0);
                // Делаем только для чтения
                tb.IsReadOnly = true;
            }

            foreach (ComboBox tb in FindVisualChildren<ComboBox>(depObj))
            {
                // Убираем обводку
                tb.BorderThickness = new Thickness(0);
                // Отключаем возможность нажать
                tb.IsHitTestVisible = false;
                // Отключаем возможность сфокусироваться
                tb.Focusable = false;
            }

            foreach (DatePicker tb in FindVisualChildren<DatePicker>(depObj))
            {
                // Убираем обводку
                tb.BorderThickness = new Thickness(0);
                // Отключаем возможность сфокусироваться
                tb.Focusable = false;
                // Отключаем возможность нажать
                tb.IsHitTestVisible = false;
            }
        }
    }
}
