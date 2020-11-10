using Zenject;

public class GameInfoInstaller : MonoInstaller<GameInfoInstaller>
{
    public override void InstallBindings()
    {
        GameInfo gameInfo = new GameInfo();
        gameInfo.AddPlayers(FindObjectsOfType<Player>());
        gameInfo.AddPenguins(FindObjectsOfType<Penguin>());
        
        Container.Bind<GameInfo>().FromInstance(gameInfo).AsSingle();
    }
}
