using System;
using UnityEngine;

public abstract class BaseScreen : MonoBehaviour
{
    public ScreenType ScreenType;

    public void Open()
    {
        Debug.Log("base screen open called");
        gameObject.SetActive(true);
    }

    public void Close()
    {
        Debug.Log("base screen close called");
        gameObject.SetActive(false);
    }
}
