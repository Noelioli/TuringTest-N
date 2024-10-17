using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private LevelManager[] _levels;

    public static GameManager _instance;

    private GameState _currentState;
    private LevelManager _currentLevel;
    private int _currentLevelIndex = 0;
    private bool _isInputActive = true;

    public int scene;

    [Header("Keycards")]
    public List<string> keycardColours;
    Dictionary<string, bool> keycards = new Dictionary<string, bool>();

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
    }

    public static GameManager GetInstance()
    {
        return _instance;
    }

    public bool IsInputActive()
    {
        return _isInputActive;
    }

    void Start()
    {
        StartKeycards();

        if (_levels.Length > 0)
        {
            ChangeState(GameState.Briefing, _levels[_currentLevelIndex]);
        }
    }

    public void ChangeState(GameState state, LevelManager level)
    {
        _currentLevel = level;
        _currentState = state;

        switch (_currentState)
        {
            case GameState.Briefing:
                StartBriefing(); break;

            case GameState.LevelStart:
                InitiateLevel(); break;

            case GameState.LevelIn:
                RunLevel(); break;

            case GameState.LevelEnd:
                CompleteLevel(); break;

            case GameState.GameOver:
                GameOver(); break;

            case GameState.GameEnd:
                GameEnd(); break;
        }
    }

    public enum GameState
    {
        Briefing,
        LevelStart,
        LevelIn,
        LevelEnd,
        GameOver,
        GameEnd
    }



    private void StartBriefing()
    {
        Debug.Log("Briefing Started");

        //Disable Player Input
        _isInputActive = false;

        ChangeState(GameState.LevelStart, _currentLevel);
    }

    private void InitiateLevel()
    {
        Debug.Log("Level Starting");

        _isInputActive = true;
        _currentLevel.StartLevel();

        ChangeState(GameState.LevelIn, _currentLevel);
    }

    private void RunLevel()
    {
        Debug.Log("Level in" + _currentLevel.gameObject.name);
    }

    private void CompleteLevel()
    {
        Debug.Log("Level End");

        ChangeState(GameState.LevelStart, _levels[++_currentLevelIndex]);
    }

    private void GameOver()
    {
        Debug.Log("Game over, you lose");

        StartCoroutine(SceneWaitTime());
    }

    private void GameEnd()
    {
        Debug.Log("Game over, you win!");
    }

    void StartKeycards()
    {
        foreach (string keycard in keycardColours)
        {
            keycards.Add(keycard, false);
        }
    }

    public void SetKeycard(string name)
    {
        keycards[name] = true;
    }

    public bool GetKeycard(string name)
    {
        return keycards[name];
    }

    IEnumerator SceneWaitTime()
    {
        _isInputActive = false;

        yield return new WaitForSeconds(5f);

        SceneManager.LoadScene(scene);
    }
}
