namespace logic
{
    public enum Player
    {
        none,
        white,
        black
    }
    public static class fun1
    {
        public static Player opp(this Player p1)
        {
            return p1 switch
            {
                Player.white => Player.black,
                Player.black => Player.white,
                _ => Player.none,
            };
        }
    }
}
