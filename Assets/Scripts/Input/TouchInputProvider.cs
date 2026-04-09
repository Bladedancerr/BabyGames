using System;
using UnityEngine;

public class TouchInputProvider : IPointerInputProvider
{
    public event Action<Vector2> OnPointerDown;
    public event Action<Vector2> OnPointerMove;
    public event Action<Vector2> OnPointerUp;

    private Camera _mainCamera;
    private bool _wasActive;
    private Vector2 _lastTouchPos;

    public TouchInputProvider(Camera mainCamera)
    {
        _mainCamera = mainCamera;
    }

    public void Tick()
    {
        bool isActive = Input.touchCount > 0;

        if (isActive)
        {
            Vector2 touchPos = _mainCamera.ScreenToWorldPoint(Input.GetTouch(0).position);

            if (!_wasActive)
            {
                OnPointerDown?.Invoke(touchPos);
            }
            else
            {
                OnPointerMove?.Invoke(touchPos);
            }

            _lastTouchPos = touchPos;
        }
        else if (_wasActive)
        {
            OnPointerUp?.Invoke(_lastTouchPos);
        }

        _wasActive = isActive;
    }
}
