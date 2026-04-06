using UnityEngine;

public class FindingGameController : BaseGameController<GameData>
{
    private IInputProvider _inputProvider;

    [SerializeField]
    private LayerMask _interactableLayer;

    private void Update()
    {
        if (Input.touchCount <= 0)
        {
            return;
        }
        Touch touch = Input.GetTouch(0);
        if (touch.phase != TouchPhase.Began)
        {
            return;
        }

        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(touch.position);
        Vector2 touchPosWorld = new Vector2(worldPoint.x, worldPoint.y);

        RaycastHit2D hit = Physics2D.Raycast(touchPosWorld, Vector2.zero, 0f, _interactableLayer);

        if (hit.collider != null)
        {
            Debug.Log("Touch detected on: " + hit.collider.name);

            if (hit.collider.TryGetComponent<IInteractable>(out var interactable))
            {
                interactable.Interact();
            }
        }
    }

    public override void FinishGame()
    {
    }

    public override void Init()
    {
    }

    public override void ResetGame()
    {
    }

    public override void StartGame()
    {
    }
}