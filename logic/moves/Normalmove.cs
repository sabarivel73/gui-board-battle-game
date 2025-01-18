using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace logic
{
    public class Normalmove : Move
    {
        public override Movetype type => Movetype.normal;
        public override Position frompos { get; }
        public override Position topos { get; }
        public Normalmove(Position from,Position to)
        {
            frompos = from;
            topos = to;
        }
        public override bool execute(Board board)
        {
            Piece piece = board[frompos];
            bool capture = !board.isempty(topos);
            board[topos] = piece;
            board[frompos] = null;
            piece.hashmove = true;
            return capture || piece.type == Piecetype.pawn;
        }
    }
}
