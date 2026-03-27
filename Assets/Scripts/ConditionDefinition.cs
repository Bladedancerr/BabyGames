using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ConditionDefinition", menuName = "Conditions/Definition")]
public class ConditionDefinition : ScriptableObject
{
    [ReadOnly]
    public string UniqueID = Guid.NewGuid().ToString();

    public bool ResetOnChange = false;
}
