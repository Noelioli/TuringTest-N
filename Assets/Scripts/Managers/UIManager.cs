    using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Health _playerHealth;
    [SerializeField] private Animator _animator;

    public TMP_Text _textHealth;
    public GameObject _gameOverText;

    [SerializeField] private LevelManager _endingLevel;

    // Start is called before the first frame update
    void Start()
    {
        _gameOverText.SetActive(false);
    }

    private void OnEnable()
    {
        _playerHealth.OnHealthUpdated += OnHealthUpdate;
        _playerHealth.OnDeath += OnDeath;
    }

    private void OnDestroy()
    {
        _playerHealth.OnHealthUpdated -= OnHealthUpdate;
    }

    void OnHealthUpdate(float health)
    {
        _textHealth.text = Mathf.Floor(health).ToString();
    }

    void OnDeath()
    {
        _gameOverText.SetActive(true);
        _endingLevel.EndLevel(); //Added parts here to play death animation, could of been added elsewhere
        _animator.SetBool("PlayerDied", true);
    }
}
