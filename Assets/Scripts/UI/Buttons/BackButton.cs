using UnityEngine;
using UnityEngine.UI;

public class BackButton : MonoBehaviour
{
    private Button _button;
    private IUICoordinator _uiCoordinator;

    private void Awake()
    {
        if (!TryGetComponent<Button>(out _button))
        {
            Debug.LogError($"button isn't assigned to navigation button {this.gameObject.name}");
            return;
        }

    }

    private void Start()
    {
        _uiCoordinator = UICoordinator.Instance;

        _button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        Debug.Log("back button clicked");
        _uiCoordinator.RequestBack();
    }
}
