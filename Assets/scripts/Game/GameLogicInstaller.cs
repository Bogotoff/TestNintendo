using Zenject;

public class GameLogicInstaller : MonoInstaller<GameLogicInstaller>
{
    public GameLogic m_gameLogic;
    
    public override void InstallBindings()
    {
        Container.Bind<GameLogic>().FromInstance(m_gameLogic).AsSingle();
    }
}
