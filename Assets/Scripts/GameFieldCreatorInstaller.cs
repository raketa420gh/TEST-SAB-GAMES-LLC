using Zenject;

public class GameFieldCreatorInstaller : MonoInstaller
{
    public GameFieldCreator Instance;
    
    public override void InstallBindings() => Bind();

    private void Bind()
    {
        Container
            .BindInstance(Instance)
            .AsSingle()
            .NonLazy();
    }
}