using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private bool _isFinalLevel;
    [SerializeField] private bool _isDeath;

    public UnityEvent _onLevelStart;
    public UnityEvent _onLevelEnd;

    public void StartLevel()
    {
        _onLevelStart?.Invoke();
    }

    public void EndLevel()
    {
        _onLevelEnd?.Invoke();

        if (_isFinalLevel)
        {
            GameManager.GetInstance().ChangeState(GameManager.GameState.GameEnd, this);
        }
        else if (_isDeath)
        {
            GameManager.GetInstance().ChangeState(GameManager.GameState.GameOver, this);
        }
        else
        {
            GameManager.GetInstance().ChangeState(GameManager.GameState.LevelEnd, this);
        }
    }
}
