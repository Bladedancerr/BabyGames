using System;
using UnityEngine;

public interface IDrawingGameInputProvider
{
    bool IsActive { get; }
    public void Tick();
    public Vector2 GetPos();
}
