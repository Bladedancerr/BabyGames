using UnityEngine;
using UnityEngine.UI;

public class BackButton : MonoBehaviour
{
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
        Debug.Log("back button clicked");
        _screensManager.RequestBack();
    }
}
