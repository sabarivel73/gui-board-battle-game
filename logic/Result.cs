using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace logic
{
    public class Result
    {
        public Player winner { get; }
        public Endreason reason { get; }
        public Result(Player w1, Endreason r1)
        {
            winner = w1;
            reason = r1;
        }
        public static Result win(Player w1)
        {
            return new Result(w1, Endreason.checkmate);
        }
        public static Result draw(Endreason reason)
        {
            return new Result(Player.none, reason);
        }
    }
}
