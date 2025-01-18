using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace logic
{
    public abstract class Piece
    {
        public abstract Piecetype type { get; }
        public abstract Player color { get; }
        public bool hashmove { get; set; } = false;
        public abstract Piece copy();
        public abstract IEnumerable<Move> GetMoves(Position from, Board board);
        protected IEnumerable<Position> MovePositionInDir1(Position from,Board board,Direction dir)
        {
            for(Position pos = from+dir; Board.isinside(pos);pos += dir)
            {
                if(board.isempty(pos))
                {
                    yield return pos;
                    continue;
                }
                Piece piece = board[pos];
                if(piece.color != color)
                {
                    yield return pos;
                }
                yield break;
            }
        }
        protected IEnumerable<Position> MovePositionInDir2(Position from,Board board, Direction[] dirs)
        {
            return dirs.SelectMany(dir => MovePositionInDir1(from, board, dir));
        }
        public virtual bool cancaptureok(Position from,Board board)
        {
            return GetMoves(from, board).Any(move =>
            {
                Piece piece = board[move.topos];
                return piece != null && piece.type == Piecetype.king;
            }
            );
        }
    }
}
