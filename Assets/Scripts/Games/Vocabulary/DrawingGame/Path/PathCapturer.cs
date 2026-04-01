using UnityEngine;

public class PathCapturer : MonoBehaviour
{
    [SerializeField]
    private PathData _pathData;

    [SerializeField]
    private Transform _target;


    [ContextMenu("Capture From Scene")]
    public void Capture()
    {
        _pathData.Capture(_target);
    }
}
