using UnityEngine;

[CreateAssetMenu(fileName = "UINavigationRules", menuName = "UI/NavigationRules")]
public class UINavigationRules : ScriptableObject
{
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

    [Header("Stack Management")]
    [Tooltip("Should the screen we are coming FROM be removed from the history?")]
    public bool PrunePreviousFromStack;

    [Tooltip("Should the screen we are coming FROM be visually disabled?")]
    public bool HidePrevious;
}
