using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace logic
{
    public class Enpassant : Move
    {
        public override Movetype type => Movetype.enpassant;
        public override Position frompos { get; }
        public override Position topos { get; }
        private readonly Position capturepos;
        public Enpassant(Position from, Position to)
        {
            frompos = from;
            topos = to;
            capturepos = new Position(from.r, to.c);
        }
        public override bool execute(Board board)
        {
            new Normalmove(frompos, topos).execute(board);
            board[capturepos] = null;
            return true;
        }
    }
}
