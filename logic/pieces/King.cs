using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace logic
{
    public class King : Piece
    {
        public override Piecetype type => Piecetype.king;
        public override Player color { get; }
        private static readonly Direction[] dirs = new Direction[]
        {
            Direction.n,Direction.s,Direction.e,Direction.w,Direction.ne,Direction.nw,Direction.se,Direction.sw
        };
        public King(Player c1)
        {
            color = c1;
        }
        private static bool isunmoverook(Position pos,Board board)
        {
            if(board.isempty(pos))
            {
                return false;
            }
            Piece piece = board[pos];
            return piece.type == Piecetype.rook && !piece.hashmove;
        }
        private static bool allempty(IEnumerable<Position>positions,Board board)
        {
            return positions.All(pos => board.isempty(pos));
        }
        private bool cancastleks(Position from,Board board)
        {
            if(hashmove)
            {
                return false;
            }
            Position rookpos = new Position(from.r, 7);
            Position[] betweenposition = new Position[] { new(from.r,5),new(from.r,6)};
            return isunmoverook(rookpos, board) && allempty(betweenposition, board);
        }
        private bool cancastleqs(Position from, Board board)
        {
            if (hashmove)
            {
                return false;
            }
            Position rookpos = new Position(from.r, 0);
            Position[] betweenposition = new Position[] { new(from.r, 1), new(from.r, 2),new(from.r,3) };
            return isunmoverook(rookpos, board) && allempty(betweenposition, board);
        }
        public override Piece copy()
        {
            King copy1 = new King(color);
            copy1.hashmove = hashmove;
            return copy1;
        }
        private IEnumerable<Position> Moveposition(Position from, Board board)
        {
            foreach (Direction dir in dirs)
            {
                Position to = from + dir;
                if(!Board.isinside(to))
                {
                    continue;
                }
                if(board.isempty(to) || board[to].color != color)
                {
                    yield return to;
                }
            }
        }
        public override IEnumerable<Move> GetMoves(Position from, Board board)
        {
            foreach(Position to in Moveposition(from,board))
            {
                yield return new Normalmove(from, to);
            }
            if(cancastleks(from,board))
            {
                yield return new Castle(Movetype.castleks, from);
            }
            if(cancastleqs(from,board))
            {
                yield return new Castle(Movetype.castleqs, from);
            }
        }
        public override bool cancaptureok(Position from, Board board)
        {
            return Moveposition(from, board).Any(to =>
            {
                Piece piece = board[to];
                return piece != null && piece.type == Piecetype.king;
            }
            );
        }
    }
}
