using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace logic.moves
{
    public class Pawnpromotion : Move
    {
        public override Movetype type => Movetype.pawnpromotion;
        public override Position frompos { get; }
        public override Position topos { get; }
        private readonly Piecetype newtype;
        public Pawnpromotion(Position from,Position to,Piecetype newtype)
        {
            frompos = from;
            topos = to;
            this.newtype = newtype;
        }
        private Piece createpp(Player color)
        {
            return newtype switch
            {
                Piecetype.knight => new Knight(color),
                Piecetype.bishop => new Bishop(color),
                Piecetype.rook => new Rook(color),
                _=>new Queen(color)
            };
        }
        public override bool execute(Board board)
        {
            Piece pawn = board[frompos];
            board[frompos] = null;
            Piece pp = createpp(pawn.color);
            pp.hashmove = true;
            board[topos] = pp;
            return true;
        }
    }
}
