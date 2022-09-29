using Zenject;

public class UnitsSpawnerInstaller : MonoInstaller
{
    public UnitSpawner Instance;
    
    public override void InstallBindings() => Bind();

    private void Bind()
    {
        Container
            .BindInstance(Instance)
            .AsSingle()
            .NonLazy();
    }
}