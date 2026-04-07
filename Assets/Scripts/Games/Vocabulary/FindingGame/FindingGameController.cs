using System;
using UnityEngine;

public class FindingGameController : BaseGameController<FindingVocabularyGameData>
{
    private IPointerInputProvider _inputProvider;

    [SerializeField]
    private LayerMask _interactableLayer;

    private IPressable _currentInteractable;

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        _inputProvider.Tick();
    }

    public override void FinishGame()
    {
    }

    public override void Init()
    {
        _inputProvider = new MouseInputProvider(Camera.main);
        _inputProvider.OnPointerDown += HandlePointerDown;
    }

    private void HandlePointerDown(Vector2 touchWorldPos)
    {
        RaycastHit2D hit = Physics2D.Raycast(touchWorldPos, Vector2.zero, 0f, _interactableLayer);

        if (hit.collider != null)
        {
            if (hit.collider.TryGetComponent<IPressable>(out var interactable))
            {
                _currentInteractable = interactable;
                _currentInteractable.Press();
            }
        }
    }

    public override void ResetGame()
    {
    }

    public override void StartGame()
    {
    }

    private void OnEnable()
    {
        FindingGameInteractable.OnCharacterPressed += OnCharacterPressed;
    }

    private void OnDisable()
    {
        _inputProvider.OnPointerDown -= HandlePointerDown;
        FindingGameInteractable.OnCharacterPressed -= OnCharacterPressed;

    }

    private void OnCharacterPressed(CharacterData data)
    {
        Debug.Log($"controller received character press: {data.CharacterName}");
        if (_gameData.CorrectCharacterDatas.Contains(data))
        {
            Debug.Log("controller received correct character press");
        }
        else
        {
            Debug.Log("controller received incorrect character press");
        }
    }
}