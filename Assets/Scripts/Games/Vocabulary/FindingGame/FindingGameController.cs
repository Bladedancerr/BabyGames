using System;
using UnityEngine;

public class FindingGameController : BaseGameController<GameData>
{
    private IPointerInputProvider _inputProvider;

    [SerializeField]
    private LayerMask _interactableLayer;

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
            Debug.Log("Touch detected on: " + hit.collider.name);

            if (hit.collider.TryGetComponent<IInteractable>(out var interactable))
            {
                interactable.Interact();
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