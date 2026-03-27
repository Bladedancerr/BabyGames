using UnityEngine;

[CreateAssetMenu(fileName = "ScreenDataContainer", menuName = "UI/Screens/ScreenDataContainer")]

public class ScreenDataContainer : ScriptableObject
{
    public ScreenData[] Screens;

    public bool TryGetScreenData(ScreenType screenType, out ScreenData screenData)
    {
        screenData = null;

        foreach (ScreenData s in Screens)
        {
            if (s.ScreenType == screenType)
            {
                screenData = s;
                return true;
            }
        }

        return false;
    }
}
