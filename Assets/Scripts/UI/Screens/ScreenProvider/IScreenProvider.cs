using UnityEngine;

public interface IScreenProvider
{
    // always return closed screen, manager should open
    public IScreen GetScreen(ScreenData data, Transform parent, IUINavigator navigator);
    public void ReleaseScreen(IScreen screen);
}

public interface IPreCacheableScreenProvider : IScreenProvider
{
    void Precache(ScreenData data, Transform parent, IUINavigator navigator);
}
