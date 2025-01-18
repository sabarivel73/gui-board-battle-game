namespace logic
{
    public class Direction
    {
        public readonly static Direction s = new Direction(1, 0);
        public readonly static Direction n = new Direction(-1, 0);
        public readonly static Direction w = new Direction(0,-1);
        public readonly static Direction e = new Direction(0,1);
        public readonly static Direction sw = s + w;
        public readonly static Direction se = s + e;
        public readonly static Direction nw = n + w;
        public readonly static Direction ne = n + e;
        public int rd { get; }
        public int cd { get; }
        public Direction(int rd1,int cd1)
        {
            rd = rd1; cd = cd1;
        }
        public static Direction operator +(Direction d1,Direction d2)
        {
            return new Direction(d1.rd + d2.rd, d1.cd + d2.cd);
        }
        public static Direction operator *(int s,Direction d)
        {
            return new Direction(s * d.rd,s*d.cd);
        }
    }
}
