using System;
using UnityEngine;

public interface IPointerInputProvider
{
    event Action<Vector2> OnPointerDown;
    event Action<Vector2> OnPointerMove;
    event Action<Vector2> OnPointerUp;

    void Tick();
}
