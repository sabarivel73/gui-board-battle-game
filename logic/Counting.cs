using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace logic
{
    public class Counting
    {
        private readonly Dictionary<Piecetype, int> whitec = new();
        private readonly Dictionary<Piecetype, int> blackc = new();
        public int totalcount { get; private set; }
        public Counting()
        {
            foreach(Piecetype type in Enum.GetValues(typeof(Piecetype)))
            {
                whitec[type] = 0;
                blackc[type] = 0;
            }
        }
        public void increment(Player color,Piecetype type)
        {
            if(color == Player.white)
            {
                whitec[type]++;
            }
            else if(color == Player.black)
            {
                blackc[type]++;
            }
            totalcount++;
        }
        public int white(Piecetype type)
        {
            return whitec[type];
        }
        public int black(Piecetype type)
        {
            return blackc[type];
        }
    }
}
