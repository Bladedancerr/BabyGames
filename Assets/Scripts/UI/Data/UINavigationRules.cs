using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UINavigationRules", menuName = "UI/NavigationRules")]
public class UINavigationRules : ScriptableObject
{
    [Header("navigation rules")]
    public UINavigationRule[] Rules;

    public bool TryGetNavigationRule(string contextID, out UINavigationRule foundRule)
    {
        foreach (var rule in Rules)
        {
            if (rule.ContextID == contextID)
            {
                foundRule = rule;
                return true;
            }
        }

        foundRule = default;
        return false;
    }
}

[System.Serializable]
public struct UINavigationRule
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
