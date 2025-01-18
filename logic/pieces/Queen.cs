using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace logic
{
    public class Queen : Piece
    {
        public override Piecetype type => Piecetype.queen;
        public override Player color { get; }
        private static readonly Direction[] dirs = new Direction[]
        {
            Direction.n, Direction.s, Direction.e, Direction.w, Direction.ne, Direction.nw, Direction.se, Direction.sw
        };
        public Queen(Player c1)
        {
            color = c1;
        }
        public override Piece copy()
        {
            Queen copy1 = new Queen(color);
            copy1.hashmove = hashmove;
            return copy1;
        }
        public override IEnumerable<Move> GetMoves(Position from, Board board)
        {
            return MovePositionInDir2(from, board, dirs).Select(to => new Normalmove(from, to));
        }
    }
}
