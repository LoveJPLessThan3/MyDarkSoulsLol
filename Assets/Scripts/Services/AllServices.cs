using System;

public class AllServices
{
    //singlton
    //service locator

    private static AllServices _instance;

    //ленивая инициализация..  перем??=присвоен -- если переменная пустая, то присвоется, а если не пустая, то ничего не произойдет
    // ?? Присваиваетя, если в первом случае будет null
    //Instance
    public static AllServices Container => _instance ?? (_instance = new AllServices());

    public void RegisterSingle<TService>(TService implementation) where TService : IService
    {
        Implementation<TService>.ServiceInstance = implementation;
    }
    //к примеру GameFactory зависит от AssetProvider и чтобы зарегистрировать его, нужно получить интерфейс IAssetProvider. Для этого тут это
    public TService Single<TService>() where TService : IService
    {
        return Implementation<TService>.ServiceInstance;
    }

    //выполнение, реализация
    private static class Implementation<TService> where TService : IService
    {
        public static TService ServiceInstance;
    }
}
