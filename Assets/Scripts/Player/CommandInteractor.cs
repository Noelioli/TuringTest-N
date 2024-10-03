using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CommandInteractor : Interactor
{
    Queue<Command> _commands = new Queue<Command>();

    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private GameObject _pointerPrefab;
    [SerializeField] private Camera _cam;

    private Command _currentCommand;

    private void Start()
    {
        _input = PlayerInput.GetInstance();
    }

    public override void Interact()
    {
        if (_input._commandPressed)
        {
            Ray ray = _cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                if (hitInfo.transform.CompareTag("Ground"))
                {
                    GameObject pointer = Instantiate(_pointerPrefab);
                    pointer.transform.position = hitInfo.point;
                    _commands.Enqueue(new MoveCommand(_agent, hitInfo.point));
                }
                else if (hitInfo.transform.CompareTag("Builder"))
                {
                    _commands.Enqueue(new BuildCommand(_agent, hitInfo.transform.GetComponent<Builder>()));
                }
            }
        }
        ProcessComands();
    }

    void ProcessComands()
    {
        if (_currentCommand != null && !_currentCommand._isComplete)
            return;

        if (_commands.Count == 0 )
            return;

        _currentCommand = _commands.Dequeue();
        _currentCommand.Execute();
    }
}
