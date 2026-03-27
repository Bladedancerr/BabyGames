using UnityEngine;

[CreateAssetMenu(fileName = "ConditionsContainer", menuName = "Conditions/Container")]
public class ConditionsContainer : ScriptableObject
{
    public ConditionDefinition[] Conditions;
}
