using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
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
using logic.moves;
namespace ui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Image[,] pieceimages = new Image[8, 8];
        private readonly Rectangle[,] hl = new Rectangle[8, 8];
        private readonly Dictionary<Position, Move> movecache = new Dictionary<Position, Move>();
        private Gamestate gamestate;
        private Position selectpos = null;
        public MainWindow()
        {
            InitializeComponent();
            initialboard();
            gamestate = new Gamestate(Player.white, Board.initial());
            drawboard(gamestate.board);
            setcursor(gamestate.cp);
        }
        private void initialboard()
        {
            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    Image image = new Image();
                    pieceimages[r, c] = image;
                    piecegrid.Children.Add(image);
                    Rectangle highlight = new Rectangle();
                    hl[r, c] = highlight;
                    highlights.Children.Add(highlight);
                }
            }
        }
        private void drawboard(Board board)
        {
            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    Piece piece = board[r, c];
                    pieceimages[r, c].Source = Images.GetImage(piece);
                }
            }
        }

        private void boardgrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(ismenuonscreen())
            {
                return;
            }
            Point point = e.GetPosition(boardgrid);
            Position pos = tosp(point);
            if(selectpos == null)
            {
                onfrompositionselected(pos);
            }
            else
            {
                ontopositionselected(pos);
            }
        }
        private Position tosp(Point point)
        {
            double sqs = boardgrid.ActualWidth / 8;
            int r = (int)(point.Y / sqs);
            int c = (int)(point.X / sqs);
            return new Position(r, c);
        }
        private void onfrompositionselected(Position pos)
        {
            IEnumerable<Move> moves = gamestate.LMP(pos);
            if(moves.Any())
            {
                selectpos = pos;
                cachemove(moves);
                showhl();
            }
        }
        private void ontopositionselected(Position pos)
        {
            selectpos = null;
            hidehl();
            if(movecache.TryGetValue(pos,out Move move))
            {
                if(move.type == Movetype.pawnpromotion)
                {
                    handlepromotion(move.frompos, move.topos);
                }
                else
                {
                    handlemove(move);
                }
            }
        }
        private void handlepromotion(Position from,Position to)
        {
            pieceimages[to.r, to.c].Source = Images.GetImage(gamestate.cp, Piecetype.pawn);
            pieceimages[from.r, from.c].Source = null;
            Promotionmenu pm = new Promotionmenu(gamestate.cp);
            menucontainer.Content = pm;
            pm.pieceselected += type =>
            {
                menucontainer.Content = null;
                Move pm = new Pawnpromotion(from, to, type);
                handlemove(pm);
            };
        }
        private void handlemove(Move move)
        {
            gamestate.Makemove(move);
            drawboard(gamestate.board);
            setcursor(gamestate.cp);
            if(gamestate.isgameover())
            {
                showgameover();
            }
        }
        private void cachemove(IEnumerable<Move> moves)
        {
            movecache.Clear();
            foreach (Move move in moves)
            {
                movecache[move.topos] = move;
            }
        }
        private void showhl()
        {
            Color color1 = Color.FromArgb(150,238,130,238);
            foreach (Position to in movecache.Keys)
            {
                hl[to.r, to.c].Fill = new SolidColorBrush(color1);
            }
        }
        private void hidehl()
        {
            foreach(Position to in movecache.Keys)
            {
                hl[to.r, to.c].Fill = Brushes.Transparent;
            }
        }
        private void setcursor(Player player)
        {
            if(player == Player.white)
            {
                Cursor = Chesscursor.whitcursor;
            }
            else
            {
                Cursor = Chesscursor.blackcursor;
            }
        }
        private bool ismenuonscreen()
        {
            return menucontainer.Content != null;
        }
        private void showgameover()
        {
            GOM gOM = new GOM(gamestate);
            menucontainer.Content = gOM;
            gOM.optionselected += option =>
            {
                if(option == Option.Restart)
                {
                    menucontainer.Content = null;
                    restartgame();
                }
                else
                {
                    Application.Current.Shutdown();
                }
            };
        }
        private void restartgame()
        {
            selectpos = null;
            hidehl();
            movecache.Clear();
            gamestate = new Gamestate(Player.white, Board.initial());
            drawboard(gamestate.board);
            setcursor(gamestate.cp);
        }

        private void window_kd(object sender, KeyEventArgs e)
        {
            if(!ismenuonscreen() && e.Key == Key.Escape)
            {
                showpausemenu();
            }
        }
        private void showpausemenu()
        {
            Pausemenu pausemenu = new Pausemenu();
            menucontainer.Content = pausemenu;
            pausemenu.optionselected += option =>
            {
                menucontainer.Content = null;
                if (option == Option.Restart)
                {
                    restartgame();
                }
            };
        }
    }
}