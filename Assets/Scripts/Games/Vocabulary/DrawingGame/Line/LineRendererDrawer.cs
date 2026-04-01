using System;
using UnityEngine;

public class LineRendererDrawer : ILineDrawer
{
    private LineRenderer _lineRenderer;

    public LineRendererDrawer(LineRenderer lineRenderer)
    {
        _lineRenderer = lineRenderer;
        _lineRenderer.positionCount = 0;
    }

    public void AddPointToLine(Vector3 position)
    {
        _lineRenderer.positionCount++;
        _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, position);
    }

    public void ResetLine()
    {
        _lineRenderer.positionCount = 0;
    }
}
