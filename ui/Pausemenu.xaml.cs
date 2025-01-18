using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ui
{
    /// <summary>
    /// Interaction logic for Pausemenu.xaml
    /// </summary>
    public partial class Pausemenu : UserControl
    {
        public event Action<Option> optionselected;
        public Pausemenu()
        {
            InitializeComponent();
        }

        private void continue_click(object sender, RoutedEventArgs e)
        {
            optionselected?.Invoke(Option.Continue);
        }

        private void restart_click(object sender, RoutedEventArgs e)
        {
            optionselected?.Invoke(Option.Restart);
        }
    }
}
