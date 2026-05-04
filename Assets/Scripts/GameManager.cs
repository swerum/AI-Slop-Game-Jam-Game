using System.Collections;
using System.Collections.Generic;
using Player.Input;
using UnityEngine;
using UnityEngine.Assertions;

public enum GameState
{
    GameOver,
    MainMenu,
    GamePlay, 
    Pause,
}

public class GameManager : MonoBehaviour
{
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _gameOverMenu;
    [SerializeField] private GameObject _pauseMenu;
    private GameState _currentState;
    private bool _pauseStateMachines = true;
    public bool PauseStateMachines { get {return _pauseStateMachines; }}
    private static GameManager _instance;
    public static GameManager Instance {get {return _instance; }}
    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void OnEnable()
    {
        _inputManager.NavigateEvent += HandleNavigate;
        _inputManager.SelectEvent += HandleSelect; 
        _inputManager.SetInputType(InputType.UI);
        _currentState = GameState.MainMenu;
        DisableAllMenus();
        _mainMenu.SetActive(true);
        _pauseStateMachines = true;
    }
    private void HandleNavigate(Vector2 navigation) {}
    public void OpenMenuFromGameplay(GameState menu)
    {
        Assert.IsTrue(_currentState == GameState.GamePlay);
        _inputManager.SetInputType(InputType.UI);
        _pauseStateMachines = true;
        switch(menu)
        {
            case GameState.GameOver:
                _gameOverMenu.SetActive(true);
                break;
            case GameState.Pause:
                _pauseMenu.SetActive(true);
                break;
        }
        _currentState = menu;
    }

    private void HandleSelect(bool isPressed)
    {
        if (_currentState == GameState.GameOver) return;
        DisableAllMenus();
        switch (_currentState)
        {
            case GameState.MainMenu:
                _inputManager.SetInputType(InputType.Player);
                _currentState = GameState.GamePlay;
                _pauseStateMachines = false;
                break;
            case GameState.GameOver:
                break;
            case GameState.Pause:
                _inputManager.SetInputType(InputType.Player);
                _currentState = GameState.GamePlay;
                _pauseStateMachines = false;
                break; 
            case GameState.GamePlay:
                Debug.LogError("Handle Select is a UI Event and shouldn't be able to be called while GameState is Gameplay.");
                break;
        }
    }
    private void DisableAllMenus()
    {
        _mainMenu.SetActive(false);
        _gameOverMenu.SetActive(false);
        _pauseMenu.SetActive(false);
    }
}
