using UnityEngine;

public class FindingGameController : BaseGameController<FindingVocabularyGameData>
{
    [SerializeField]
    private LayerMask _interactableLayer;

    // needs to be removed, orchestrator will initialize
    // private void Start()
    // {
    //     Init();
    // }

    public override void Init()
    {
        base.Init();
        FindingGameInteractable.OnCharacterPressed += OnCharacterPressed;
    }

    public override void StartGame()
    {
    }

    public override void FinishGame()
    {
        base.FinishGame();
    }

    public override void ResetGame()
    {
        base.ResetGame();
        FindingGameInteractable.OnCharacterPressed -= OnCharacterPressed;
    }

    public override void HandlePointerDown(Vector2 touchWorldPos)
    {
        RaycastHit2D hit = Physics2D.Raycast(touchWorldPos, Vector2.zero, 0f, _interactableLayer);

        if (hit.collider != null)
        {
            if (hit.collider.TryGetComponent<IPressable>(out var interactable))
            {
                interactable.Press();
            }
        }
    }

    private void OnCharacterPressed(CharacterData data)
    {
        Debug.Log($"controller received character press: {data.CharacterName}");
        if (_gameData.CorrectCharacterDatas.Contains(data))
        {
            Debug.Log("controller received correct character press");
            FinishGame();
        }
        else
        {
            Debug.Log("controller received incorrect character press");
        }
    }
}