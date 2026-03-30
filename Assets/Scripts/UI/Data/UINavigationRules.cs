using UnityEngine;

[CreateAssetMenu(fileName = "NavRules", menuName = "UI/Navigation/Rules")]
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
