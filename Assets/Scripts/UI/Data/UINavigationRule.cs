using UnityEngine;

[CreateAssetMenu(fileName = "NavRule", menuName = "UI/Navigation/Rule")]
public class UINavigationRule : ScriptableObject
{
    public string ContextID;
    public ScreenType Target;
    public ConditionDefinition[] Conditions;

    [Header("pruning logic")]
    [Tooltip("keep popping screens until this type is at the top. Leave 'None' to use PruneDepth instead.")]
    public ScreenType PruneUntil;

    [Tooltip("how deep want to prune tabs")]
    public int PruneDepth;

    [Tooltip("clear whole screens stack")]
    public bool ClearStack;
}
