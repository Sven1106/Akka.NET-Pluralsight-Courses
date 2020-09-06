
namespace GameConsole.Events
{
    class PlayerCreated
    {
        public string PlayerName { get; private set; }
        public PlayerCreated(string playerName)
        {
            PlayerName = playerName;
        }
    }
}
