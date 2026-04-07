using System;
using UnityEngine;

public class FindingGameController : BaseGameController<GameData>
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
}