using UnityEngine;

public class FindingGameInteractable : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log("interaction started");
    }
}
