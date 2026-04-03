using System.Collections.Generic;
using UnityEngine;

public class GameOrchestrator : MonoBehaviour
{
    [SerializeField]
    private BaseGameController[] _gameControllers;

    [SerializeField]
    private GameData[] _gamedatas;

    private Dictionary<GameType, Dictionary<GameTypeInternal, BaseGameController>> _gameconntrollersLookup;
    private Dictionary<GameType, Dictionary<GameTypeInternal, GameData>> _gamedatasLookup;


    private void Start()
    {
        _gameconntrollersLookup = new Dictionary<GameType, Dictionary<GameTypeInternal, BaseGameController>>();
        foreach (var controller in _gameControllers)
        {
            if (!_gameconntrollersLookup.ContainsKey(controller.GameType))
            {
                _gameconntrollersLookup[controller.GameType] = new Dictionary<GameTypeInternal, BaseGameController>();
            }

            _gameconntrollersLookup[controller.GameType][controller.InternalGameType] = controller;
        }

        _gamedatasLookup = new Dictionary<GameType, Dictionary<GameTypeInternal, GameData>>();

        foreach (var data in _gamedatas)
        {
            if (!_gamedatasLookup.ContainsKey(data.GameType))
            {
                _gamedatasLookup[data.GameType] = new Dictionary<GameTypeInternal, GameData>();
            }

            _gamedatasLookup[data.GameType][data.InternalGameType] = data;
        }

        var spawned = Instantiate(_gameconntrollersLookup[GameType.VOCABULARY][GameTypeInternal.VOCABULARY_DRAW_LETTER]);
        spawned.TryInit(_gamedatasLookup[GameType.VOCABULARY][GameTypeInternal.VOCABULARY_DRAW_LETTER]);
    }
}
