using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace logic
{
    public class Knight : Piece
    {
        public override Piecetype type => Piecetype.knight;
        public override Player color { get; }
        public Knight(Player c1)
        {
            color = c1;
        }
        public override Piece copy()
        {
            Knight copy1 = new Knight(color);
            copy1.hashmove = hashmove;
            return copy1;
        }
        private static IEnumerable<Position> ptop(Position from)
        {
            foreach (Direction vdir in new Direction[] {Direction.n,Direction.s})
            {
                foreach(Direction hdir in new Direction[] {Direction.w,Direction.e})
                {
                    yield return from + 2 * vdir + hdir;
                    yield return from + 2 * hdir + vdir;
                }
            }
        }
        private IEnumerable<Position> moveposition(Position from,Board board)
        {
            return ptop(from).Where(pos => Board.isinside(pos) && (board.isempty(pos) || board[pos].color != color));
        }
        public override IEnumerable<Move> GetMoves(Position from, Board board)
        {
            return moveposition(from, board).Select(to => new Normalmove(from, to));
        }
    }
}
