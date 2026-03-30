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
            Debug.LogError($"screensmanager screendatacontainer isn't assigned to {this.gameObject.name}");
            return;
        }

        if (_screensParent == null || _popupsParent == null)
        {
            Debug.LogError($"screensmanager screensparent or popupsparent isn't assigned to {this.gameObject.name}");
            return;
        }

        _screenStack = new Stack<IScreen>();
        _screenProvider = new PooledScreenProvider();

        if (_preCache)
        {
            if (_screenProvider is IPreCacheableScreenProvider cacheable)
            {
                StartCoroutine(Precache(cacheable));
            }
        }
    }

    private IEnumerator Precache(IPreCacheableScreenProvider provider)
    {
        yield return null;
        Transform parent;

        foreach (var data in _screenDataContainer.Screens)
        {
            if (data == null)
            {
                continue;
            }

            yield return null;
            parent = GetScreenParent(data.UILayer);
            if (data.PrecacheOnStart)
            {
                provider.Precache(data, parent, this);
            }
        }
    }


    public void RequestScreenOpen(ScreenType target, int depth, ScreenType until, bool clear)
    {
        Debug.Log($"screensmanager screen open requested {target}");

        if (clear)
        {
            Debug.Log("screensmanager clear requested");
            ClearStack();
        }

        if (!_screenDataContainer.TryGetScreenData(target, out ScreenData nextData))
        {
            Debug.LogWarning($"screensmanager couldn't find screen data for screen {target}");
            return;
        }
        else if (until != ScreenType.NONE)
        {
            Debug.Log("screensmanager until is set");
            while (_screenStack.Count > 0 && _screenStack.Peek().ScreenType != until)
            {
                var top = _screenStack.Pop();
                top.Close();
                _screenProvider.ReleaseScreen(top);
                PrintStackForDebug();
            }
        }
        else
        {
            Debug.Log("screensmanager until is not set using depth");
            for (int i = 0; i < depth; i++)
            {
                if (_screenStack.Count == 0)
                {
                    break;
                }

                var top = _screenStack.Pop();
                top.Close();
                _screenProvider.ReleaseScreen(top);
                PrintStackForDebug();
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

    private void ClearStack()
    {
        while (_screenStack.Count > 0)
        {
            _screenProvider.ReleaseScreen(_screenStack.Pop());
            PrintStackForDebug();
        }
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
