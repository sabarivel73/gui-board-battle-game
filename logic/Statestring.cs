using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace logic
{
    public class Statestring
    {
        private readonly StringBuilder sb = new StringBuilder();
        public Statestring(Player currentplayer,Board board)
        {
            addpieceplace(board);
            sb.Append(' ');
            addcurrentplayer(currentplayer);
            sb.Append(' ');
            addcastlingrights(board);
            sb.Append(' ');
            addenpassant(board, currentplayer);
        }
        public override string ToString()
        {
            return sb.ToString();
        }
        private static char piecechar(Piece piece)
        {
            char c = piece.type switch
            {
                Piecetype.pawn => 'p',
                Piecetype.knight => 'n',
                Piecetype.rook => 'r',
                Piecetype.bishop => 'b',
                Piecetype.queen => 'q',
                Piecetype.king => 'k',
                _ => ' '
            };
            if (piece.color == Player.white)
            {
                return char.ToUpper(c);
            }
            return c;
        }
        private void addrowdata(Board board,int row)
        {
            int e = 0;
            for(int i=0;i<8;i++)
            {
                if (board[row,i]==null)
                {
                    e++;
                    continue;
                }
                if(e>0)
                {
                    sb.Append(e);
                    e = 0;
                }
                sb.Append(piecechar(board[row, i]));
            }
            if(e>0)
            {
                sb.Append(e);
            }
        }
        private void addpieceplace(Board board)
        {
            for(int i=0;i<8;i++)
            {
                if(i!=0)
                {
                    sb.Append('/');
                }
                addrowdata(board, i);
            }
        }
        private void addcurrentplayer(Player currentplayer)
        {
            if(currentplayer == Player.white)
            {
                sb.Append('w');
            }
            else
            {
                sb.Append('b');
            }
        }
        private void addcastlingrights(Board board)
        {
            bool castlewks = board.castlerightks(Player.white);
            bool castlewqs = board.castlerightqs(Player.white);
            bool castlebks = board.castlerightks(Player.black);
            bool castlebqs = board.castlerightqs(Player.black);
            if(!(castlewks || castlewqs || castlebks || castlebqs))
            {
                sb.Append('-');
                return;
            }
            if(castlewks)
            {
                sb.Append('K');
            }
            if(castlewqs)
            {
                sb.Append('Q');
            }
            if(castlebks)
            {
                sb.Append('k');
            }
            if(castlebqs)
            {
                sb.Append('q');
            }
        }
        private void addenpassant(Board board,Player currentplayer)
        {
            if(!board.cancaptureenpassant(currentplayer))
            {
                sb.Append('-');
                return;
            }
            Position pos = board.getpawnsp(currentplayer.opp());
            char f1 = (char)('a' + pos.c);
            int r1 = 8 - pos.r;
            sb.Append(f1);
            sb.Append(r1);
        }
    }
}
