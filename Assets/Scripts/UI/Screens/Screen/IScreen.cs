using UnityEngine;

public interface IScreen
{
    Transform Transform { get; }
    ScreenType ScreenType { get; }

    public void Initialize(IUINavigator uiNavigator);
    public void Open();
    public void Close();
}
