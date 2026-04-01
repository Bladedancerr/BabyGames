using System.Collections.Generic;
using UnityEngine;

public interface IPathGenerator<T>
{
    public List<T> GeneratePath(List<T> points);
}
