using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace logic
{
    public class Gamestate
    {
        public Board board { get; }
        public Player cp { get; private set; }
        public Result result { get; private set; } = null;
        private int nocaptureorpawnmoves = 0;
        private string ss;
        private readonly Dictionary<string, int> sh = new Dictionary<string, int>();
        public Gamestate(Player p, Board b)
        {
            cp = p;
            board = b;
            ss = new Statestring(cp, board).ToString();
            sh[ss] = 1;
        }
        public IEnumerable<Move> LMP(Position pos)
        {
            if(board.isempty(pos) || board[pos].color != cp)
            {
                return Enumerable.Empty<Move>();
            }
            Piece piece = board[pos];
            IEnumerable<Move> movecandidate = piece.GetMoves(pos, board);
            return movecandidate.Where(move => move.islegal(board));
        }
        public void Makemove(Move move)
        {
            board.setpawnsp(cp, null);
            bool captureorpawn = move.execute(board);
            if(captureorpawn)
            {
                nocaptureorpawnmoves = 0;
                sh.Clear();
            }
            else
            {
                nocaptureorpawnmoves++;
            }
            cp = cp.opp();
            updatess();
            checkforgameover();
        }
        public IEnumerable<Move> alllegalmove(Player player)
        {
            IEnumerable<Move> movecandidates = board.piecepositionfor(player).SelectMany(pos =>
            {
                Piece piece = board[pos];
                return piece.GetMoves(pos, board);
            });
            return movecandidates.Where(move => move.islegal(board));
        }
        private void checkforgameover()
        {
            if(!alllegalmove(cp).Any())
            {
                if(board.isincheck(cp))
                {
                    result = Result.win(cp.opp());
                }
                else
                {
                    result = Result.draw(Endreason.stalemate);
                }
            }
            else if(board.insufficientmaterial())
            {
                result = Result.draw(Endreason.insufficientmaterial);
            }
            else if(fiftymoverule())
            {
                result = Result.draw(Endreason.fiftymoverule);
            }
            else if(threefr())
            {
                result = Result.draw(Endreason.threefoldrepetition);
            }
        }
        public bool isgameover()
        {
            return result != null;
        }
        private bool fiftymoverule()
        {
            int fullmoves = nocaptureorpawnmoves / 2;
            return fullmoves == 50;
        }
        private void updatess()
        {
            ss = new Statestring(cp, board).ToString();
            if(!sh.ContainsKey(ss))
            {
                sh[ss] = 1;
            }
            else
            {
                sh[ss]++;
            }
        }
        private bool threefr()
        {
            return sh[ss] == 3;
        }
    }
}
