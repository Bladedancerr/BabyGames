using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class NavigationButton : MonoBehaviour
{
    [SerializeField, ContextID]
    private string _contextID;

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
        Debug.Log($"navigation button clicked tried to open {_contextID}");
        _uiCoordinator.RequestGoTo(_contextID);
    }
}
