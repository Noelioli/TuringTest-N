using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeycardBehaviour : MonoBehaviour
{
    [SerializeField] private string _colourName;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameManager.SetKeycard(_colourName);
            Destroy(gameObject);
        }
    }
}
