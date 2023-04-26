
//из состояния можно войти и выйти
public interface IState : IExitableState
{
    void Enter();
}
public interface IPayLoadedState<TPayLoaded> : IExitableState
{
    void Enter(TPayLoaded payLoaded);
}
public interface IExitableState
{
    void Exit();
}