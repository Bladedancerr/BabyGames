using System;
using UnityEngine;

public class FindingAllGameInteractable : MonoBehaviour, IPressable
{
    [SerializeField]
    private CharacterData _characterData;

    public static Action<CharacterData> OnCharacterPressed;

    public void Press()
    {
        OnCharacterPressed?.Invoke(_characterData);
    }
}
