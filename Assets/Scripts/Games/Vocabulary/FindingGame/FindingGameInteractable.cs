using System;
using UnityEngine;

public class FindingGameInteractable : MonoBehaviour, IPressable
{
    [SerializeField]
    private CharacterData _characterData;

    public static event Action<CharacterData> OnCharacterPressed;

    public void Press()
    {
        Debug.Log($"item pressed: {this.gameObject.name}, data: {_characterData.CharacterName}");
        OnCharacterPressed?.Invoke(_characterData);
    }
}
