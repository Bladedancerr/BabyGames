using UnityEngine;

[CreateAssetMenu(fileName = "ScreenData", menuName = "UI/Screens/ScreenData")]
public class ScreenData : ScriptableObject
{
    [Header("screen identity")]
    public ScreenType ScreenType;
    public BaseScreen ScreenPrefab;

    [Header("which screens can it open")]
    [SerializeField]
    private ScreenType[] _allowedScreens;

    public bool CanNavigateTo(ScreenType screenType)
    {
        foreach (ScreenType s in _allowedScreens)
        {
            if (s == screenType)
            {
                return true;
            }
        }

        return false;
    }
}
