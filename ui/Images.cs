using System.Windows.Media;
using System.Windows.Media.Imaging;
using logic;

namespace ui
{
    public static class Images
    {
        private static readonly Dictionary<Piecetype, ImageSource> whitesources = new()
        {
            {Piecetype.pawn,LoadImage("need/PawnW.png") },
            {Piecetype.bishop,LoadImage("need/BishopW.png") },
            {Piecetype.knight,LoadImage("need/KnightW.png") },
            {Piecetype.rook,LoadImage("need/RookW.png") },
            {Piecetype.queen,LoadImage("need/QueenW.png") },
            {Piecetype.king,LoadImage("need/KingW.png") }
        };
        private static readonly Dictionary<Piecetype, ImageSource> blacksources = new()
        {
            {Piecetype.pawn,LoadImage("need/PawnB.png") },
            {Piecetype.bishop,LoadImage("need/BishopB.png") },
            {Piecetype.knight,LoadImage("need/KnightB.png") },
            {Piecetype.rook,LoadImage("need/RookB.png") },
            {Piecetype.queen,LoadImage("need/QueenB.png") },
            {Piecetype.king,LoadImage("need/KingB.png") }
        };
        private static ImageSource LoadImage(string filepath)
        {
            return new BitmapImage(new Uri(filepath, UriKind.Relative));
        }
        public static ImageSource GetImage(Player color,Piecetype type)
        {
            return color switch
            {
                Player.white => whitesources[type],
                Player.black => blacksources[type],
                _=>null
            };
        }
        public static ImageSource GetImage(Piece piece)
        {
            if(piece == null)
            {
                return null;
            }
            return GetImage(piece.color, piece.type);
        }
    }
}
