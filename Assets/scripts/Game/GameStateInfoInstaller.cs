using Zenject;

public class GameStateInfoInstaller : MonoInstaller<GameStateInfoInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind<GameStateInfo>().ToSelf().AsSingle();
    }
}
