using System.Collections.Generic;
using UnityEngine;

public class PooledScreenProvider : IPreCacheableScreenProvider
{
    private Dictionary<ScreenType, IScreen> _screenCache = new Dictionary<ScreenType, IScreen>();

    public void Precache(ScreenData data, Transform parent, IUINavigator navigator)
    {
        if (data.Cacheable)
        {
            GetScreen(data, parent, navigator);
        }
    }

    public IScreen GetScreen(ScreenData data, Transform parent, IUINavigator navigator)
    {
        if (_screenCache.TryGetValue(data.ScreenType, out IScreen cached))
        {
            return cached;
        }

        var go = Object.Instantiate(data.ScreenPrefab, parent);
        var screen = go.GetComponent<IScreen>();
        screen.Initialize(navigator);
        _screenCache.Add(data.ScreenType, screen);

        screen.Close();

        return screen;
    }

    public void ReleaseScreen(IScreen screen)
    {
        ((MonoBehaviour)screen).gameObject.SetActive(false);
    }
}
