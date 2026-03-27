using UnityEngine;

public interface IUICoordinator
{
    public void RequestGoTo(string contextID);
    public void RequestBack();
}
