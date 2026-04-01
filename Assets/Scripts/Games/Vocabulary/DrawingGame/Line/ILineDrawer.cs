using System;
using UnityEngine;

public interface ILineDrawer
{
    public void AddPointToLine(Vector3 position);
    public void ResetLine();
}
