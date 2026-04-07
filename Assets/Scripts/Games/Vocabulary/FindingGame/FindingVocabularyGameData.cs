using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FindingVocabularyGameData", menuName = "Game/Data/Vocabulary/PerGame/Finding")]
public class FindingVocabularyGameData : VocabularyGameData
{
    public List<CharacterData> CorrectCharacterDatas;
    public List<CharacterData> IncorrectCharacterDatas;
}
