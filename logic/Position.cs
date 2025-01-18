namespace logic
{
    public class Position
    {
        public int r { get; }
        public int c { get; }
        public Position(int r1,int c1)
        {
            r = r1; c = c1;
        }
        public Player squarecolor()
        {
            if((r+c)%2==0)
            {
                return Player.white;
            }
            return Player.black;
        }

        public override bool Equals(object obj)
        {
            return obj is Position position &&
                   r == position.r &&
                   c == position.c;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(r, c);
        }

        public static bool operator ==(Position left, Position right)
        {
            return EqualityComparer<Position>.Default.Equals(left, right);
        }

        public static bool operator !=(Position left, Position right)
        {
            return !(left == right);
        }
        public static Position operator +(Position p,Direction d)
        {
            return new Position(p.r + d.rd, p.c + d.cd);
        }
    }
}
