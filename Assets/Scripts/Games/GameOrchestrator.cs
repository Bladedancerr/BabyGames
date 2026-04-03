using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameOrchestrator : MonoBehaviour
{
    [SerializeField]
    private GameLibrary[] _gameLibraries;

    // key is maingametype, in inner dict key is internalgametype + index separated with _
    private Dictionary<GameType, Dictionary<string, GameData>> _gameDataLookup;

    // key is maingametype + internalgametype separated with _
    private Dictionary<string, BaseGameController> _controllersLookup;

    private string _merger = "_";

    private void Awake()
    {
        SetupLookups();
    }

    private void Start()
    {
        // for testing
        var game = GetGame(GameType.VOCABULARY, GameTypeInternal.VOCABULARY_DRAW_LETTER);
        var data = GetGameData(GameType.VOCABULARY, GameTypeInternal.VOCABULARY_DRAW_LETTER, 0);

        if (game == null || data == null)
        {
            return;
        }

        if (game.TryInit(data))
        {
            game.StartGame();
        }
    }

    private void SetupLookups()
    {
        _gameDataLookup = new Dictionary<GameType, Dictionary<string, GameData>>();
        _controllersLookup = new Dictionary<string, BaseGameController>();

        foreach (var lib in _gameLibraries)
        {
            if (!_gameDataLookup.ContainsKey(lib.MainType))
            {
                _gameDataLookup[lib.MainType] = new Dictionary<string, GameData>();
            }

            foreach (var internalLib in lib.GameInternalLibraries)
            {
                string controllerKey = GetKey(_merger, lib.MainType, internalLib.GameTypeInternal);

                _controllersLookup[controllerKey] = internalLib.BaseGameController;

                for (int i = 0; i < internalLib.GameDatas.Length; i++)
                {
                    string dataKey = GetKey(_merger, internalLib.GameTypeInternal, i);
                    _gameDataLookup[lib.MainType][dataKey] = internalLib.GameDatas[i];
                }
            }
        }

        LogLookups();
    }

    private BaseGameController GetGame(GameType type, GameTypeInternal internalType)
    {
        string controllerKey = GetKey(_merger, type, internalType);

        var controller = _controllersLookup[controllerKey];

        if (controller == null)
        {
            return null;
        }

        return Instantiate(controller);
    }

    private GameData GetGameData(GameType type, GameTypeInternal internalType, int index)
    {
        string dataKey = GetKey(_merger, internalType, index);
        var data = _gameDataLookup[type][dataKey];

        if (data == null)
        {
            return null;
        }

        return data;
    }

    private void LogLookups()
    {
        foreach (var data in _gameDataLookup)
        {
            Debug.Log($"data lookup: key: {data.Key}");
            foreach (var gameData in data.Value)
            {
                Debug.Log($"data lookup inner: key: {gameData.Key} value: {gameData.Value}");
            }
        }

        foreach (var controller in _controllersLookup)
        {
            Debug.Log($"controller lookup: key: {controller.Key} value: {controller.Value}");
        }
    }

    private string GetKey(string merger, params object[] args)
    {
        return string.Join(merger, args);
    }
}
