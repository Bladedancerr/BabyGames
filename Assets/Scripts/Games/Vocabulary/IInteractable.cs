using UnityEngine;

public interface IPressable
{
    public void Press();
}

public interface IHoldable : IPressable
{
    public void Release();
}

public interface IDraggable : IHoldable
{
    public void Move(Vector3 pos);
}