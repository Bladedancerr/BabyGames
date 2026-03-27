using System.Linq;
using UnityEngine;

public class UICoordinator : MonoBehaviour, IUICoordinator
{
    [SerializeField]
    private UINavigationRules _navigationRules;

    [SerializeField]
    private UINavigationRule _defaultNavigationRule;

    private IUINavigator _uiNavigator;
    private IConditionService _conditionService;

    public static UICoordinator Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        _uiNavigator = ScreensManager.Instance;
        _conditionService = ConditionManager.Instance;

        if (_uiNavigator == null || _conditionService == null)
        {
            Debug.LogError($"uinavigator or conditionservice isn't assigned to {this.gameObject.name}");
            return;
        }

        _uiNavigator.RequestScreenOpen(_defaultNavigationRule.Target, _defaultNavigationRule.PruneDepth, _defaultNavigationRule.PruneUntil, _defaultNavigationRule.ClearStack);
    }

    public void RequestGoTo(string contextID)
    {
        if (!_navigationRules.TryGetNavigationRule(contextID, out UINavigationRule rule))
        {
            return;
        }

        if (CanGoTo(rule))
        {
            _uiNavigator.RequestScreenOpen(rule.Target, rule.PruneDepth, rule.PruneUntil, rule.ClearStack);

            foreach (var cond in rule.Conditions)
            {
                if (cond.ResetOnChange)
                {
                    _conditionService.Set(cond.UniqueID, false);
                }
            }
        }
    }

    public void RequestBack()
    {
        _uiNavigator.RequestBack();
    }

    private bool CanGoTo(UINavigationRule rule)
    {
        if (rule.Conditions == null || rule.Conditions.Length == 0)
        {
            return true;
        }

        return _conditionService.CheckAll(rule.Conditions.Select(c => c.UniqueID).ToArray());
    }
}
