using System.Collections.Generic;
using UnityEngine;

public class CatmullRomPathGenerator : IPathGenerator<Vector2>
{
    private float _maxDistanceBetweenPoints;

    public CatmullRomPathGenerator(float maxDistanceBetweenPoints)
    {
        _maxDistanceBetweenPoints = maxDistanceBetweenPoints;
    }

    public List<Vector2> GeneratePath(List<Vector2> points)
    {
        if (points.Count < 2) return points;
        var smoothedPoints = new List<Vector2>();

        for (int i = 0; i < points.Count - 1; i++)
        {
            Vector2 p0 = (i == 0) ? points[i] : points[i - 1];
            Vector2 p1 = points[i];
            Vector2 p2 = points[i + 1];
            Vector2 p3 = (i + 2 >= points.Count) ? points[i + 1] : points[i + 2];

            var distance = Vector2.Distance(p1, p2);
            var resolution = Mathf.CeilToInt(distance / _maxDistanceBetweenPoints);

            for (int j = 0; j < resolution; j++)
            {
                float t = (float)j / resolution;
                smoothedPoints.Add(GetCatmullRomPosition(t, p0, p1, p2, p3));
            }
        }

        smoothedPoints.Add(points[points.Count - 1]);
        return smoothedPoints;
    }

    public Vector2 GetCatmullRomPosition(float t, Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3)
    {
        return 0.5f * (
            (2 * p1) +
            (-p0 + p2) * t +
            (2 * p0 - 5 * p1 + 4 * p2 - p3) * t * t +
            (-p0 + 3 * p1 - 3 * p2 + p3) * t * t * t
        );
    }
}
