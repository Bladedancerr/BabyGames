using UnityEngine;

[CreateAssetMenu(fileName = "ParentPortalExercise", menuName = "UI/Exercise/ParentPortalExercise")]
public class ParentPortalExerciseContainer : ScriptableObject
{
    public ParentPortalExerciseData[] Excercises;
}

[System.Serializable]
public class ParentPortalExerciseData : ExerciseData
{
    public string Excercise;
    public string Result;

    public override string GetData()
    {
        return Excercise;
    }

    public override string GetResult()
    {
        return Result;
    }
}
