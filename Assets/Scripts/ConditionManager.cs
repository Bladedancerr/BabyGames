using System.Collections.Generic;
using UnityEngine;

public class ConditionManager : MonoBehaviour, IConditionService
{
    [SerializeField]
    private ConditionsContainer _conditionsContainer;

    private Dictionary<string, ConditionDefinition> _conditionsLookup;
    private Dictionary<string, bool> _conditionValues;

    public static ConditionManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        SetupLookups();
    }

    public void Set(string uID, bool value)
    {
        if (_conditionValues.ContainsKey(uID))
        {
            _conditionValues[uID] = value;

            if (_conditionsLookup[uID].ResetOnChange)
            {
                _conditionValues[uID] = !value;
            }
            Debug.Log($"conditionsmanager set {uID} to {value}");
        }
    }

    public bool Get(string uID)
    {
        if (_conditionValues.TryGetValue(uID, out bool value))
        {
            return value;
        }

        return false;
    }

    public bool CheckAll(string[] uIDs)
    {
        foreach (var uID in uIDs)
        {
            if (!_conditionValues[uID])
            {
                return false;
            }
        }

        return true;
    }

    private void SetupLookups()
    {
        if (_conditionsContainer == null)
        {
            Debug.LogError($"conditions container isn't assigned to {this.gameObject.name}");
            return;
        }

        _conditionsLookup = new Dictionary<string, ConditionDefinition>();
        _conditionValues = new Dictionary<string, bool>();

        foreach (var c in _conditionsContainer.Conditions)
        {
            if (c == null)
            {
                continue;
            }

            _conditionsLookup.Add(c.UniqueID, c);
            _conditionValues.Add(c.UniqueID, false);
        }
    }

    //// for testing

    // [SerializeField]
    // private ConditionDefinition _testCondition;

    // private void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.Space) && _testCondition != null)
    //     {
    //         _conditionValues[_testCondition.UniqueID] = !_conditionValues[_testCondition.UniqueID];
    //     }
    // }
}
