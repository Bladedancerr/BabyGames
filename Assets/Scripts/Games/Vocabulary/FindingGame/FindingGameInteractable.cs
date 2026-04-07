using Unity.VisualScripting;
using UnityEngine;

public class FindingGameInteractable : MonoBehaviour, IPressable
{
    [SerializeField]
    private CharacterData _characterData;

    public void Press()
    {
        Debug.Log($"item pressed: {this.gameObject.name}, data: {_characterData.CharacterName}");
    }
}
