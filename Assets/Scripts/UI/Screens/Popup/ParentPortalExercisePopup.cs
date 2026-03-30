using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ParentPortalExercisePopup : Screen
{
    [SerializeField]
    private TMP_Text _exerciseText;

    [SerializeField]
    private TMP_InputField _excerciseInputField;

    [SerializeField]
    private ConditionDefinition _excerciseCondition;

    private IConditionService _conditionService;
    private IExercise _exercise;
    private ExerciseData _exerciseData;

    public override void Initialize(IUINavigator uiNavigator)
    {
        base.Initialize(uiNavigator);

        if (_conditionService == null || _exercise == null)
        {
            _conditionService = ConditionManager.Instance;
            _exercise = GetComponent<IExercise>();
        }

        if (_conditionService == null || _exercise == null)
        {
            Debug.LogError($"condition service or exercise isn't assigned to {this.gameObject.name}");
        }
    }

    public override void Open()
    {
        base.Open();
        Setup();
    }

    public override void Close()
    {
        base.Close();
        _excerciseInputField.text = "";
    }

    private void Setup()
    {
        _exerciseData = _exercise.Get();

        _exerciseText.text = _exerciseData.GetData();
        _excerciseInputField.text = "";
    }

    public void OnSubmitClicked()
    {
        bool isCorrect = _exerciseData.GetResult() == _excerciseInputField.text.Trim(' ');
        _conditionService.Set(_excerciseCondition.UniqueID, isCorrect);
    }
}