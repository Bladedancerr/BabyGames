using UnityEngine;

public interface IUINavigator
{
    void RequestScreenOpen(ScreenType screenType);
    void RequestBack();
}
