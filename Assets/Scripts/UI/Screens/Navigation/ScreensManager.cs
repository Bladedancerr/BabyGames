using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreensManager : MonoBehaviour, IUINavigator
{
    public static ScreensManager Instance { get; private set; }

    [SerializeField]
    private ScreenDataContainer _screenDataContainer;

    [SerializeField]
    private Transform _screensParent;

    [SerializeField]
    private Transform _popupsParent;

    [SerializeField]
    private bool _preCache;

    private Stack<IScreen> _screenStack;
    private IScreenProvider _screenProvider;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        Init();
    }

    private void Init()
    {
        if (_screenDataContainer == null)
        {
            Debug.LogError($"screendatacontainer isn't assigned to {this.gameObject.name}");
            return;
        }

        if (_screensParent == null || _popupsParent == null)
        {
            Debug.LogError($"screensparent or popupsparent isn't assigned to {this.gameObject.name}");
            return;
        }

        _screenStack = new Stack<IScreen>();
        _screenProvider = new SimpleScreenProvider();

        if (_preCache)
        {
            if (_screenProvider is IPreCacheableScreenProvider cacheable)
            {
                Precache(cacheable);
            }
        }
    }

    private void Precache(IPreCacheableScreenProvider provider)
    {
        Transform parent;

        foreach (var data in _screenDataContainer.Screens)
        {
            parent = GetScreenParent(data.UILayer);
            if (data.PrecacheOnStart)
            {
                provider.Precache(data, parent, this);
            }
        }
    }


    public void RequestScreenOpen(ScreenType screenType)
    {
        Debug.Log($"screensmanager screen open requested {screenType}");

        if (!_screenDataContainer.TryGetScreenData(screenType, out ScreenData nextData))
        {
            Debug.LogWarning($"couldn't find screen data for screen {screenType}");
            return;
        }

        if (nextData.UILayer == UILayer.Screen)
        {
            while (_screenStack.Count > 0)
            {
                var top = _screenStack.Peek();
                if (!_screenDataContainer.TryGetScreenData(top.ScreenType, out ScreenData topData))
                {
                    break;
                }

                if (topData.CloseOnNextScreen)
                {
                    _screenStack.Pop();
                    top.Close();
                    _screenProvider.ReleaseScreen(top);
                }
                else
                {
                    top.Close();
                    break;
                }
            }
        }

        Transform parent = GetScreenParent(nextData.UILayer);
        IScreen spawnedScreen = _screenProvider.GetScreen(nextData, parent, this);

        spawnedScreen.Transform.SetAsLastSibling();
        spawnedScreen.Open();
        _screenStack.Push(spawnedScreen);

        PrintStackForDebug();
    }

    public void RequestBack()
    {
        Debug.Log("screensmanager back requested");

        if (_screenStack.Count <= 1)
        {
            return;
        }

        var current = _screenStack.Pop();
        current.Close();

        _screenProvider.ReleaseScreen(current);

        if (_screenStack.TryPeek(out IScreen previous))
        {
            previous.Open();
        }

        PrintStackForDebug();
    }

    private Transform GetScreenParent(UILayer uILayer)
    {
        return uILayer == UILayer.Screen ? _screensParent : _popupsParent;
    }

    private void PrintStackForDebug()
    {
        var stack = "stack: ";
        foreach (var screen in _screenStack)
        {
            stack += screen.ScreenType + " -> ";
        }

        Debug.Log(stack);
    }
}
