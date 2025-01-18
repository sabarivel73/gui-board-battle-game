using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace logic
{
    public class Castle : Move
    {
        public override Movetype type { get; }
        public override Position frompos { get; }
        public override Position topos { get; }
        private readonly Direction kingmovedir;
        private readonly Position rookfrompos;
        private readonly Position rooktopos;
        public Castle(Movetype type1,Position kingpos)
        {
            type = type1;
            frompos = kingpos;
            if(type == Movetype.castleks)
            {
                kingmovedir = Direction.e;
                topos = new Position(kingpos.r, 6);
                rookfrompos = new Position(kingpos.r, 7);
                rooktopos = new Position(kingpos.r, 5);
            }
            else if(type == Movetype.castleqs)
            {
                kingmovedir = Direction.w;
                topos = new Position(kingpos.r, 2);
                rookfrompos = new Position(kingpos.r, 0);
                rooktopos = new Position(kingpos.r, 3);
            }
        }
        public override bool execute(Board board)
        {
            new Normalmove(frompos, topos).execute(board);
            new Normalmove(rookfrompos, rooktopos).execute(board);
            return false;
        }
        public override bool islegal(Board board)
        {
            Player player = board[frompos].color;
            if(board.isincheck(player))
            {
                return false;
            }
            Board copy = board.copy();
            Position kingposincopy = frompos;
            for(int i=0;i<2;i++)
            {
                new Normalmove(kingposincopy, kingposincopy + kingmovedir).execute(copy);
                kingposincopy += kingmovedir;
                if(copy.isincheck(player))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
