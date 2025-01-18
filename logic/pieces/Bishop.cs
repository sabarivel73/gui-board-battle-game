using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace logic
{
    public class Bishop : Piece
    {
        public override Piecetype type => Piecetype.bishop;
        public override Player color { get; }
        private static readonly Direction[] dirs = new Direction[]
        {
            Direction.nw, Direction.ne, Direction.se, Direction.sw
        };
        public Bishop(Player c1)
        {
            color = c1;
        }
        public override Piece copy()
        {
            Bishop copy1 = new Bishop(color);
            copy1.hashmove = hashmove;
            return copy1;
        }
        public override IEnumerable<Move> GetMoves(Position from, Board board)
        {
            return MovePositionInDir2(from, board, dirs).Select(to => new Normalmove(from, to));
        }
    }
}
