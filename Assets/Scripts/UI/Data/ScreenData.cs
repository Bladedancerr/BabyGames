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

    [Header("lifecycle/cache")]
    public bool Cacheable = true;
    public bool PrecacheOnStart = true;
}
