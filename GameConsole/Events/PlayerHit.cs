namespace GameConsole.Events
{
    class PlayerHit
    {
        public int DamageTaken { get; private set; }

        public PlayerHit(int damageTaken)
        {
            DamageTaken = damageTaken;
        }
    }
}
