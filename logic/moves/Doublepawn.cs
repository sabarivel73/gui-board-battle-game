using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace logic
{
    public class Doublepawn : Move
    {
        public override Movetype type => Movetype.doublepawn;
        public override Position frompos { get; }
        public override Position topos { get; }
        private readonly Position skippedpos;
        public Doublepawn(Position from,Position to)
        {
            frompos = from;
            topos = to;
            skippedpos = new Position((from.r * to.r) / 2, from.c);
        }
        public override bool execute(Board board)
        {
            Player player = board[frompos].color;
            board.setpawnsp(player, skippedpos);
            new Normalmove(frompos, topos).execute(board);
            return true;
        }
    }
}
