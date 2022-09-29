using Zenject;

public class UnitsDetectorInstaller : MonoInstaller
{
    public UnitsDetector Instance;
    
    public override void InstallBindings() => Bind();

    private void Bind()
    {
        Container.BindInstance(Instance)
            .AsSingle()
            .NonLazy();
    }
}