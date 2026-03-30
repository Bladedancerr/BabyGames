using UnityEngine;

public class ParentPortalExercise : MonoBehaviour, IExercise
{
    [SerializeField]
    private ParentPortalExerciseContainer _container;

    public ExerciseData Get()
    {
        return _container.Excercises[Random.Range(0, _container.Excercises.Length)];
    }
}