using UnityEngine;
using UnityEngine.Rendering;

public class FindingAllGameController : BaseGameController<FindingAllVocabularyGameData>
{
    [SerializeField]
    private LayerMask _interactableLayer;

    private int _foundCharactersCount;

    public override void Init()
    {
        base.Init();
        FindingAllGameInteractable.OnCharacterPressed += OnCharacterPressed;
    }

    public override void StartGame()
    {
        Debug.Log($"{this.GetType()} game started");
    }

    public override void FinishGame()
    {
        base.FinishGame();
    }

    public override void ResetGame()
    {
        base.ResetGame();
        FindingAllGameInteractable.OnCharacterPressed -= OnCharacterPressed;
    }

    private void OnCharacterPressed(CharacterData data)
    {
        if (_foundCharactersCount >= _gameData.TotalCount)
        {
            return;
        }

        if (!_gameData.CorrectCharacterDatas.Contains(data))
        {
            Debug.Log("controller received incorrect character press");
            return;
        }

        _foundCharactersCount++;
        Debug.Log($"controller received correct character press: {data.CharacterName}, count: {_foundCharactersCount}");

        if (_foundCharactersCount == _gameData.TotalCount)
        {
            Debug.Log($"{this.GetType()} game finished");
            FinishGame();
        }
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
}
