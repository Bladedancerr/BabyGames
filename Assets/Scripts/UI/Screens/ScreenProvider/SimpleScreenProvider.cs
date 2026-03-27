using UnityEngine;

public class SimpleScreenProvider : IScreenProvider
{
    public IScreen GetScreen(ScreenData data, Transform parent, IUINavigator navigator)
    {
        var go = Object.Instantiate(data.ScreenPrefab, parent);

        var screen = go.GetComponent<IScreen>();
        screen.Initialize(navigator);

        screen.Close();

        return screen;
    }

    public void ReleaseScreen(IScreen screen)
    {
        Object.Destroy(((MonoBehaviour)screen).gameObject);
    }
}