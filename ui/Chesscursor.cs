using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ui
{
    public static class Chesscursor
    {
        public static readonly Cursor whitcursor = loadcursor("need/CursorW.cur");
        public static readonly Cursor blackcursor = loadcursor("need/CursorB.cur");
        private static Cursor loadcursor(string filepath)
        {
            Stream stream = Application.GetResourceStream(new Uri(filepath, UriKind.Relative)).Stream;
            return new Cursor(stream,true);
        }
    }
}
