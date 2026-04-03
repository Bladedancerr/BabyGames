using UnityEngine;

// [CreateAssetMenu(fileName = "GameData", menuName = "Game/Data")]
public abstract class GameData : ScriptableObject
{
    public GameType GameType;
    public GameTypeInternal InternalGameType;
    public int ID;
}
