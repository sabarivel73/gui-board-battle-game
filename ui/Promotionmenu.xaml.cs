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
using logic;

namespace ui
{
    /// <summary>
    /// Interaction logic for Promotionmenu.xaml
    /// </summary>
    public partial class Promotionmenu : UserControl
    {
        public event Action<Piecetype> pieceselected;
        public Promotionmenu(Player player)
        {
            InitializeComponent();
            queenimage.Source = Images.GetImage(player, Piecetype.queen);
            bishopimage.Source = Images.GetImage(player, Piecetype.bishop);
            knightimage.Source = Images.GetImage(player, Piecetype.knight);
            rookimage.Source = Images.GetImage(player, Piecetype.rook);
        }

        private void qi_md(object sender, MouseButtonEventArgs e)
        {
            pieceselected?.Invoke(Piecetype.queen);
        }

        private void bi_md(object sender, MouseButtonEventArgs e)
        {
            pieceselected?.Invoke(Piecetype.bishop);
        }

        private void kn_md(object sender, MouseButtonEventArgs e)
        {
            pieceselected?.Invoke(Piecetype.knight);
        }

        private void ri_md(object sender, MouseButtonEventArgs e)
        {
            pieceselected?.Invoke(Piecetype.rook);
        }
    }
}
