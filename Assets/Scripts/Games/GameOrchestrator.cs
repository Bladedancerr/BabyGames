using System.Collections.Generic;
using UnityEngine;

public class GameOrchestrator : MonoBehaviour
{
    [SerializeField]
    private BaseGameController[] _gameControllers;

    private GameData[] _gamedatas;

    private Dictionary<GameType, BaseGameController> _gameconntrollersLookup;

    private void Start()
    {
        foreach (var controller in _gameControllers)
        {
            Debug.Log($"controller: gametype{controller.GameType}");
        }

        _gameconntrollersLookup = new Dictionary<GameType, BaseGameController>();
        foreach (var controller in _gameControllers)
        {
            _gameconntrollersLookup.Add(controller.GameType, controller);
        }

        Instantiate(_gameconntrollersLookup[GameType.VOCABULARY]);
    }
}
