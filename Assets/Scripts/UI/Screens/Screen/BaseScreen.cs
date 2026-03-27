using UnityEngine;

public abstract class Screen : MonoBehaviour, IScreen
{
    [SerializeField]
    protected ScreenType _screenType;

    protected IUINavigator _uiNavigator;

    public ScreenType ScreenType { get { return _screenType; } }
    public Transform Transform { get { return transform; } }


    public virtual void Initialize(IUINavigator uiNavigator)
    {
        _uiNavigator = uiNavigator;
    }

    public virtual void Open()
    {
        gameObject.SetActive(true);
    }

    public virtual void Close()
    {
        gameObject.SetActive(false);
    }
}
