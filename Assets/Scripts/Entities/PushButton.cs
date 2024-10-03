using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PushButton : MonoBehaviour, ISelectable
{
    [SerializeField] private Material _hoverColour;
    [SerializeField] private Animator _liftAnimator;
    private MeshRenderer _renderer;
    private Material _defaultColour;


    public UnityEvent _onPush;

    private void Start()
    {
        _renderer = GetComponent<MeshRenderer>();
        _defaultColour = _renderer.material;
    }

    public void IsActiveAnimationTrue()
    {
        _liftAnimator.SetBool("isActive", true);
    }

    public void IsActiveAnimationFalse()
    {
        _liftAnimator.SetBool("isActive", false);
    }

    public void OnHoverEnter()
    {
        _renderer.material = _hoverColour;
    }

    public void OnHoverExit()
    {
        _renderer.material = _defaultColour;
    }

    public void OnSelect()
    {
        //turn on animation bool
        _onPush?.Invoke();
    }
}
