using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ParentPortalExcercisePopup : Screen
{
    [SerializeField]
    private TMP_InputField _excerciseInputField;

    [SerializeField]
    private ConditionDefinition _excerciseCondition;

    [SerializeField]
    private string _testExcerciseValue;

    private IConditionService _conditionService;

    private void Start()
    {
        _conditionService = ConditionManager.Instance;
    }

    public void OnSubmitClicked()
    {
        bool isCorrect = _excerciseInputField.text == _testExcerciseValue;
        _conditionService.Set(_excerciseCondition.UniqueID, isCorrect);
    }

    private void OnDisable()
    {
        _excerciseInputField.text = "";
    }
}