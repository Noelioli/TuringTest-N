using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLightBehaviour : MonoBehaviour
{
    [SerializeField] private string _doorColour;
    [SerializeField] private Material _offColour;
    [SerializeField] private Material _onColour;
    private MeshRenderer _renderer;
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        _renderer = GetComponent<MeshRenderer>();
        _renderer.material = _offColour;
    }

    // Update is called once per frame
    void Update()
    {
       if (gameManager.GetKeycard(_doorColour))
        {
            _renderer.material = _onColour;
        }
    }
}
