using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace logic
{
    public abstract class Move
    {
        public abstract Movetype type { get; }
        public abstract Position frompos { get; }
        public abstract Position topos { get; }
        public abstract bool execute(Board board);
        public virtual bool islegal(Board board)
        {
            Player player = board[frompos].color;
            Board boardcopy = board.copy();
            execute(boardcopy);
            return !boardcopy.isincheck(player);
        }
    }
}
