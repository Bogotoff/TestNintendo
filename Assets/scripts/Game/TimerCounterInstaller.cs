using Zenject;

public class TimerCounterInstaller : MonoInstaller<TimerCounterInstaller>
{
    public float m_initialTime = 60;
    
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<TimerCounter>().AsSingle().WithArguments(m_initialTime);
    }
}
