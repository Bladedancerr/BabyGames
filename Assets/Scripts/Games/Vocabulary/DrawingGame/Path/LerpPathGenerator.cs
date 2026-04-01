using System.Collections.Generic;
using UnityEngine;

public class LerpPathGenerator : IPathGenerator<Vector2>
{
    private float _maxDistanceBetweenPoints;

    public LerpPathGenerator(float maxDistanceBetweenPoints)
    {
        _maxDistanceBetweenPoints = maxDistanceBetweenPoints;
    }

    public List<Vector2> GeneratePath(List<Vector2> points)
    {
        var smoothedPoints = new List<Vector2>();

        for (int i = 0; i < points.Count - 1; i++)
        {
            var start = points[i];
            var end = points[i + 1];

            var distance = Vector2.Distance(start, end);

            int addedPointsAmount = Mathf.CeilToInt(distance / _maxDistanceBetweenPoints);

            for (int j = 0; j < addedPointsAmount; j++)
            {
                float t = (float)j / addedPointsAmount;
                Vector2 newPoint = Vector2.Lerp(start, end, t);
                smoothedPoints.Add(newPoint);
            }
        }

        smoothedPoints.Add(points[points.Count - 1]);
        return smoothedPoints;
    }
}
