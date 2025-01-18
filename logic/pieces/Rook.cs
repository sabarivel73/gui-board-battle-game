using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace logic
{
    public class Rook : Piece
    {
        public override Piecetype type => Piecetype.rook;
        public override Player color { get; }
        private static readonly Direction[] dirs = new Direction[]
        {
            Direction.n, Direction.s, Direction.e, Direction.w
        };
        public Rook(Player c1)
        {
            color = c1;
        }
        public override Piece copy()
        {
            Rook copy1 = new Rook(color);
            copy1.hashmove = hashmove;
            return copy1;
        }
        public override IEnumerable<Move> GetMoves(Position from, Board board)
        {
            return MovePositionInDir2(from, board, dirs).Select(to => new Normalmove(from, to));
        }
    }
}
