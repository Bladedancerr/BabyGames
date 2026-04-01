using System;
using UnityEngine;

[Serializable]
public class DrawingGameTouchInputProvider : IDrawingGameInputProvider
{
    public bool IsActive { get; private set; }

    private Camera _mainCamera;
    private Vector2 _touchPos;

    public DrawingGameTouchInputProvider(Camera camera)
    {
        IsActive = false;
        _mainCamera = camera;
    }

    public void Tick()
    {
        IsActive = Input.touchCount > 0;

        if (IsActive)
        {
            _touchPos = _mainCamera.ScreenToWorldPoint(Input.GetTouch(0).position);
        }
    }

    public Vector2 GetPos()
    {
        return _touchPos;
    }
}
