    using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Health _playerHealth;

    public TMP_Text _textHealth;
    public GameObject _gameOverText;

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
    }
}
