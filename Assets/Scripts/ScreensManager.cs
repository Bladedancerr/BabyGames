using System.Collections.Generic;
using UnityEngine;

public class ScreensManager : MonoBehaviour
{
    [SerializeField]
    private ScreenDataContainer _screenDataContainer;

    [SerializeField]
    private Transform _screensParent;

    public static ScreensManager Instance { get; private set; }

    private Stack<BaseScreen> _screenStack;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        if (_screenDataContainer == null)
        {
            Debug.LogError($"screendatacontainer isn't assigned to {this.gameObject.name}");
            return;
        }

        _screenStack = new Stack<BaseScreen>();

        RequestScreenOpen(ScreenType.SCREEN1);
    }


    public void RequestScreenOpen(ScreenType screenType)
    {
        Debug.Log($"screensmanager screen open requested {screenType}");
        if (CanNavigateTo(screenType))
        {
            if (_screenDataContainer.TryGetScreenData(screenType, out ScreenData newScreenData))
            {
                if (_screenStack.TryPeek(out BaseScreen currentScreen))
                {
                    currentScreen.Close();
                }
                var spawned = Instantiate(newScreenData.ScreenPrefab, _screensParent);
                _screenStack.Push(spawned);
                spawned.Open();
            }
        }
    }

    public void RequestBack()
    {
        Debug.Log("screensmanager back requested");
        if (_screenStack.Count > 1)
        {
            _screenStack.Pop().Close();
            _screenStack.Peek().Open();
        }
    }

    private bool CanNavigateTo(ScreenType screenType)
    {
        if (_screenStack.Count == 0)
        {
            return true;
        }

        if (_screenStack.TryPeek(out BaseScreen currentScreen))
        {
            if (_screenDataContainer.TryGetScreenData(currentScreen.ScreenType, out ScreenData currentScreenData))
            {
                return currentScreenData.CanNavigateTo(screenType);
            }
        }

        return false;
    }
}
