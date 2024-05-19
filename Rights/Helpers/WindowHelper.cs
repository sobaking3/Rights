using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Effects;

namespace Rights.Helpers
{
    public static class WindowHelper
    {
        /// <summary>
        /// Передвинуть окно
        /// </summary>
        /// <param name="window">Окно</param>
        /// <param name="e">Аргументы события мыши</param>
        public static void DragMove(Window window, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                window.DragMove();
            }
        }

        /// <summary>
        /// Открыть диалоговое окно
        /// </summary>
        /// <param name="controlToBlur">Элемент который следует размыть до открытия окна</param>
        /// <param name="windowToShow">Открываемое окно</param>
        /// <param name="strength">Сила размытия</param>
        public static void ShowDialogWithBlur(FrameworkElement controlToBlur, Window windowToShow, double strength = 12)
        {
            controlToBlur.Effect = new BlurEffect { Radius = strength };
            windowToShow.ShowDialog();
            controlToBlur.Effect = null;
        }
    }
}
