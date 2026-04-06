using System;
using UnityEngine;

public class MouseInputProvider : IPointerInputProvider
{
    public event Action<Vector2> OnPointerDown;
    public event Action<Vector2> OnPointerMove;
    public event Action<Vector2> OnPointerUp;

    private Camera _mainCamera;
    private bool _wasActive;
    private Vector2 _lastMousePos;

    public MouseInputProvider(Camera mainCamera)
    {
        _mainCamera = mainCamera;
    }

    public void Tick()
    {
        bool isActive = Input.GetMouseButton(0);
        Vector2 currentMousePos = _mainCamera.ScreenToWorldPoint(Input.mousePosition);

        if (isActive)
        {
            if (!_wasActive)
            {
                OnPointerDown?.Invoke(currentMousePos);
            }
            else
            {
                OnPointerMove?.Invoke(currentMousePos);
            }

            _lastMousePos = currentMousePos;
        }
        else if (_wasActive)
        {
            OnPointerUp?.Invoke(_lastMousePos);
        }

        _wasActive = isActive;
    }
}
