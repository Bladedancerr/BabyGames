using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScreenData", menuName = "UI/Screens/ScreenData")]
public class ScreenData : ScriptableObject
{
    [Header("screen identity")]
    public ScreenType ScreenType;
    public GameObject ScreenPrefab;
    public UILayer UILayer;
    public bool Cacheable = true;
    public bool PrecacheOnStart = true;
    public bool CloseOnNextScreen = false;

    [Header("which screens can it open")]
    [SerializeField]
    private List<ScreenType> _allowedScreens;

    public bool CanNavigateTo(ScreenType screenType)
    {
        // for now, needs change
        return _allowedScreens.Count == 0 || _allowedScreens.Contains(screenType);
    }
}
