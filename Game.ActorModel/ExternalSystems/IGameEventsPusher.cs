namespace Game.ActorModel.ExternalSystems
{
    public interface IGameEventsPusher
    {
        void PlayerJoined(string playerName, int playerHealth); // The signalRBridgeActor is going to call this to allow SPA client to update list of players when new players join.
        void UpdatePlayerHealth(string playerName, int playerHealth);
    }
}
