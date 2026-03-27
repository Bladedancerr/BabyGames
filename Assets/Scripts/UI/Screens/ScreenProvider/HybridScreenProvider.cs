using System.Collections.Generic;
using UnityEngine;

public class HybridScreenProvider : IPreCacheableScreenProvider
{
    private Dictionary<ScreenType, IScreen> _screenCache = new Dictionary<ScreenType, IScreen>();
    private ScreenDataContainer _screenDataContainer;

    public HybridScreenProvider(ScreenDataContainer screenDataContainer)
    {
        _screenDataContainer = screenDataContainer;
    }

    public void Precache(ScreenData data, Transform parent, IUINavigator navigator)
    {
        if (data.Cacheable)
        {
            GetScreen(data, parent, navigator);
        }
    }

    public IScreen GetScreen(ScreenData data, Transform parent, IUINavigator navigator)
    {
        if (data.Cacheable && _screenCache.TryGetValue(data.ScreenType, out IScreen cached))
        {
            return cached;
        }

        var go = Object.Instantiate(data.ScreenPrefab, parent);
        var screen = go.GetComponent<IScreen>();

        screen.Initialize(navigator);

        if (data.Cacheable)
        {
            _screenCache.Add(data.ScreenType, screen);
        }

        screen.Close();

        return screen;
    }

    public void ReleaseScreen(IScreen screen)
    {
        if (_screenDataContainer.TryGetScreenData(screen.ScreenType, out ScreenData data))
        {
            screen.Close();

            if (!data.Cacheable)
            {
                Object.Destroy(((MonoBehaviour)screen).gameObject);
            }
        }
    }
}
