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
    /// Interaction logic for GOM.xaml
    /// </summary>
    public partial class GOM : UserControl
    {
        public event Action<Option> optionselected;
        public GOM(Gamestate gamestate)
        {
            InitializeComponent();
            Result result = gamestate.result;
            wt.Text = getwt(result.winner);
            rt.Text = getreasontext(result.reason, gamestate.cp);
        }
        private static string getwt(Player winner)
        {
            return winner switch
            {
                Player.white => "White Player wins",
                Player.black => "Black Player wins",
                _ => "Game was draw"
            };
        }
        private static string playerstring(Player player)
        {
            return player switch
            {
                Player.white => "White Player",
                Player.black => "Black Player",
                _=>""
            };
        }
        private static string getreasontext(Endreason reason,Player currentplayer)
        {
            return reason switch
            {
                Endreason.stalemate => $"Stalemate - {playerstring(currentplayer)} can't move",
                Endreason.checkmate => $"Checkmate - {playerstring(currentplayer)} can't move",
                Endreason.fiftymoverule => "Fifty move rule",
                Endreason.insufficientmaterial => "Insufficient material",
                Endreason.threefoldrepetition => "Three fold repetition",
                _=>""
            };
        }
        private void restart_click(object sender, RoutedEventArgs e)
        {
            optionselected?.Invoke(Option.Restart);
        }

        private void exit_click(object sender, RoutedEventArgs e)
        {
            optionselected?.Invoke(Option.Exit);
        }
    }
}
