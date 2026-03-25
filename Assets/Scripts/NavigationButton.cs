using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class NavigationButton : MonoBehaviour
{
    [SerializeField]
    private ScreenType _targetScreen;

    private Button _button;
    private ScreensManager _screensManager;

    private void Awake()
    {
        if (!TryGetComponent<Button>(out _button))
        {
            Debug.LogError($"button isn't assigned to navigation button {this.gameObject.name}");
            return;
        }

        _button.onClick.AddListener(OnClick);
    }

    private void Start()
    {
        _screensManager = ScreensManager.Instance;
    }

    private void OnClick()
    {
        Debug.Log($"navigation button clicked tried to open {_targetScreen}");
        _screensManager.RequestScreenOpen(_targetScreen);
    }
}
