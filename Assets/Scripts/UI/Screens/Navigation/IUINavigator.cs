using UnityEngine;

public interface IUINavigator
{
    void RequestScreenOpen(ScreenType target, int depth, ScreenType until, bool clear);
    void RequestBack();
}
