using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace logic
{
    public class Board
    {
        private readonly Piece[,] pieces = new Piece[8, 8];
        private readonly Dictionary<Player, Position> pawnskipposition = new Dictionary<Player, Position>
        {
            {Player.white,null },
            {Player.black,null }
        };
        public Piece this[int r,int c]
        {
            get { return pieces[r, c]; }
            set { pieces[r, c] = value; }
        }
        public Piece this[Position pos]
        {
            get { return this[pos.r, pos.c]; }
            set { this[pos.r, pos.c] = value; }
        }
        public Position getpawnsp(Player player)
        {
            return pawnskipposition[player];
        }
        public void setpawnsp(Player player,Position pos)
        {
            pawnskipposition[player] = pos;
        }
        public static Board initial()
        {
            Board board = new Board();
            board.asp();
            return board;
        }
        private void asp()
        {
            this[0, 0] = new Rook(Player.black);
            this[0, 1] = new Knight(Player.black);
            this[0, 2] = new Bishop(Player.black);
            this[0, 3] = new Queen(Player.black);
            this[0, 4] = new King(Player.black);
            this[0, 5] = new Bishop(Player.black);
            this[0, 6] = new Knight(Player.black);
            this[0, 7] = new Rook(Player.black);
            this[7, 0] = new Rook(Player.white);
            this[7, 1] = new Knight(Player.white);
            this[7, 2] = new Bishop(Player.white);
            this[7, 3] = new Queen(Player.white);
            this[7, 4] = new King(Player.white);
            this[7, 5] = new Bishop(Player.white);
            this[7, 6] = new Knight(Player.white);
            this[7, 7] = new Rook(Player.white);
            for(int i=0;i<8;i++)
            {
                this[1, i] = new Pawn(Player.black);
                this[6, i] = new Pawn(Player.white);
            }
        }
        public static bool isinside(Position pos)
        {
            return pos.r >= 0 && pos.r < 8 && pos.c >= 0 && pos.c < 8;
        }
        public bool isempty(Position pos)
        {
            return this[pos] == null;
        }
        public IEnumerable<Position> pieceposition()
        {
            for(int r=0;r<8;r++)
            {
                for(int c=0;c<8;c++)
                {
                    Position pos = new Position(r, c);
                    if(!isempty(pos))
                    {
                        yield return pos;
                    }
                }
            }
        }
        public IEnumerable<Position> piecepositionfor(Player player)
        {
            return pieceposition().Where(pos => this[pos].color == player);
        }
        public bool isincheck(Player player)
        {
            return piecepositionfor(player.opp()).Any(pos =>
            {
                Piece piece = this[pos];
                return piece.cancaptureok(pos, this);
            });
        }
        public Board copy()
        {
            Board copy = new Board();
            foreach (Position pos in pieceposition())
            {
                copy[pos] = this[pos].copy();
            }
            return copy;
        }
        public Counting countpieces()
        {
            Counting counting = new Counting();
            foreach(Position pos in pieceposition())
            {
                Piece piece = this[pos];
                counting.increment(piece.color, piece.type);
            }
            return counting;
        }
        public bool insufficientmaterial()
        {
            Counting counting = countpieces();
            return iskingvsking(counting) || iskingbishopvsking(counting) || iskingknightvsking(counting) || iskingbishopvskingbishop(counting);
        }
        private static bool iskingvsking(Counting counting)
        {
            return counting.totalcount == 2;
        }
        private static bool iskingbishopvsking(Counting counting)
        {
            return counting.totalcount == 3 && (counting.white(Piecetype.bishop) == 1 || counting.black(Piecetype.bishop) == 1);
        }
        private static bool iskingknightvsking(Counting counting)
        {
            return counting.totalcount == 3 && (counting.white(Piecetype.knight) == 1 || counting.black(Piecetype.knight) == 1);
        }
        private bool iskingbishopvskingbishop(Counting counting)
        {
            if(counting.totalcount != 4)
            {
                return false;
            }
            if(counting.white(Piecetype.bishop) != 1 || counting.black(Piecetype.bishop) != 1)
            {
                return false;
            }
            Position wbishoppos = findpiece(Player.white, Piecetype.bishop);
            Position bbishoppos = findpiece(Player.black, Piecetype.bishop);
            return wbishoppos.squarecolor() == bbishoppos.squarecolor();
        }
        private Position findpiece(Player color,Piecetype t1)
        {
            return piecepositionfor(color).First(pos => this[pos].type == t1);
        }
        private bool isunmovekingandrook(Position kingpos,Position rookpos)
        {
            if(isempty(kingpos) || isempty(rookpos))
            {
                return false;
            }
            Piece king = this[kingpos];
            Piece rook = this[rookpos];
            return king.type == Piecetype.king && rook.type == Piecetype.rook && !king.hashmove && !rook.hashmove;
        }
        public bool castlerightks(Player player)
        {
            return player switch
            {
                Player.white => isunmovekingandrook(new Position(7, 4), new Position(7, 7)),
                Player.black => isunmovekingandrook(new Position(0, 4), new Position(0, 7)),
                _ => false
            };
        }
        public bool castlerightqs(Player player)
        {
            return player switch
            {
                Player.white => isunmovekingandrook(new Position(7, 4), new Position(7, 0)),
                Player.black => isunmovekingandrook(new Position(0, 4), new Position(0, 0)),
                _ => false
            };
        }
        private bool haspawninposition(Player player, Position[] pawnposition,Position skippos)
        {
            foreach(Position pos in pawnposition.Where(isinside))
            {
                Piece piece = this[pos];
                if(piece == null || piece.color != player || piece.type != Piecetype.pawn)
                {
                    continue;
                }
                Enpassant move = new Enpassant(pos, skippos);
                if(move.islegal(this))
                {
                    return true;
                }
            }
            return false;
        }
        public bool cancaptureenpassant(Player player)
        {
            Position skippos = getpawnsp(player.opp());
            if(skippos == null)
            {
                return false;
            }
            Position[] pawnposition = player switch
            {
                Player.white => new Position[] {skippos + Direction.sw,skippos+Direction.se},
                Player.black => new Position[] {skippos + Direction.nw,skippos+Direction.ne},
                _=>Array.Empty<Position>()
            };
            return haspawninposition(player, pawnposition, skippos);
        }
    }
}
