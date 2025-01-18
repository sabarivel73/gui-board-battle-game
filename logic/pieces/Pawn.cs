using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using logic.moves;

namespace logic
{
    public class Pawn : Piece
    {
        public override Piecetype type => Piecetype.pawn;
        public override Player color { get; }
        private readonly Direction forward;
        public Pawn(Player c1)
        {
            color = c1;
            if(c1 == Player.white)
            {
                forward = Direction.n;
            }
            else if(c1 == Player.black)
            {
                forward = Direction.s;
            }
        }
        public override Piece copy()
        {
            Pawn copy1 = new Pawn(color);
            copy1.hashmove = hashmove;
            return copy1;
        }
        private static bool canmoveto(Position pos,Board board)
        {
            return Board.isinside(pos) && board.isempty(pos);
        }
        private bool cancaptureat(Position pos,Board board)
        {
            if(!Board.isinside(pos) || board.isempty(pos))
            {
                return false;
            }
            return board[pos].color != color;
        }
        private static IEnumerable<Move> promotionmoves(Position from,Position to)
        {
            yield return new Pawnpromotion(from, to, Piecetype.knight);
            yield return new Pawnpromotion(from, to, Piecetype.bishop);
            yield return new Pawnpromotion(from, to, Piecetype.rook);
            yield return new Pawnpromotion(from, to, Piecetype.queen);
        }
        private IEnumerable<Move> forwardmoves(Position from,Board board)
        {
            Position onemovepos = from + forward;
            if(canmoveto(onemovepos,board))
            {
                if(onemovepos.r == 0 || onemovepos.r == 7)
                {
                    foreach(Move prm in promotionmoves(from,onemovepos))
                    {
                        yield return prm;
                    }
                }
                else
                {
                    yield return new Normalmove(from, onemovepos);
                }
                Position twomovespos = onemovepos + forward;
                if(!hashmove && canmoveto(twomovespos,board))
                {
                    yield return new Doublepawn(from, twomovespos);
                }
            }
        }
        private IEnumerable<Move> diagonalmoves(Position from,Board board)
        {
            foreach(Direction dir in new Direction[] {Direction.w,Direction.e})
            {
                Position to = from + forward + dir;
                if(to == board.getpawnsp(color.opp()))
                {
                    yield return new Enpassant(from, to);
                }
                else if(cancaptureat(to,board))
                {
                    if (to.r == 0 || to.r == 7)
                    {
                        foreach (Move prm in promotionmoves(from, to))
                        {
                            yield return prm;
                        }
                    }
                    else
                    {
                        yield return new Normalmove(from, to);
                    }
                }
            }
        }
        public override IEnumerable<Move> GetMoves(Position from,Board board)
        {
            return forwardmoves(from, board).Concat(diagonalmoves(from, board));
        }
        public override bool cancaptureok(Position from, Board board)
        {
            return diagonalmoves(from, board).Any(move =>
            {
                Piece piece = board[move.topos];
                return piece != null && piece.type == Piecetype.king;
            }
            );
        }
    }
}
