using UnityEngine;
using UnityEngine.Events;
using DebugMenu;

public class NoClipMode : MonoBehaviour
{
    #region Exposed

    [Header("Config")]
    [SerializeField]
    private Rigidbody _rigidbody;

    [SerializeField]
    private Collider[] _colliders;

    [SerializeField]
    private Transform _forwardRelativeTo;

    [SerializeField][Min(0.01f)]
    private float _speedIncrement = 0.01f;

    [SerializeField]
    private UnityEvent _onToggle;


    [Header("Inputs")]
    [SerializeField]
    private KeyCode _forward;

    [SerializeField]
    private KeyCode _backward;

    [SerializeField]
    private KeyCode _strafLeft;

    [SerializeField]
    private KeyCode _strafRight;

    [SerializeField]
    private KeyCode _up;

    [SerializeField]
    private KeyCode _down;

    [SerializeField]
    private KeyCode _increaseSpeed;
    
    [SerializeField]
    private KeyCode _decreaseSpeed;
         
    #endregion


    #region Public Properties

    [SerializeField]
    private float Speed
    {
        get => _speed;
        set => _speed = Mathf.Max(0.01f, value);
    }
         
    #endregion


    #region Unity API

    private void Update() 
    {
        UpdateInput();  
    }

    private void FixedUpdate() 
    {
        ApplyVelocity();
    }

    #endregion


    #region Main

    private void UpdateInput()
    {
        if(!_isActive) return;

        ModifySpeed();
        MoveHorizontaly();
        MoveVerticaly();
    }

    [DebugMenu("Player/Toggle no clip")]
    public void ToggleNoClip()
    {
        _isActive = !_isActive;
        Debug.Log($"No clip {(_isActive ? "activated" : "deactivated")}");
        _rigidbody.useGravity = !_isActive;
        foreach (var collider in _colliders)
        {
            collider.enabled = !_isActive;
        }

        _onToggle?.Invoke();
    }
    
    private void ApplyVelocity()
    {
        if(!_isActive) return;

        _rigidbody.velocity = _velocity;
    }

    #endregion

    
    #region Plumbery

    private void ModifySpeed()
    {
        if(Input.GetKey(_increaseSpeed))
        {
            Speed += _speedIncrement;
        }

        else if(Input.GetKey(_decreaseSpeed))
        {
            Speed -= _speedIncrement;
        }
    }

    private void MoveVerticaly()
    { 
        if(Input.GetKey(_up))
        {
            _velocity.y = Speed;
        }

        else if(Input.GetKey(_down))
        {
            _velocity.y = -Speed;
        }

        else if(!Input.GetKey(_up) && !Input.GetKey(_down))
        {
            _velocity.y = 0;
        }
    }

    private void MoveHorizontaly()
    {
        if(Input.GetKey(_forward))
        {
            _velocity.z = Speed;
        }

        else if(Input.GetKey(_backward))
        {
            _velocity.z = -Speed;
        }

        else if(!Input.GetKey(_forward) && !Input.GetKey(_backward))
        {
            _velocity.z = 0;
        }

        if(Input.GetKey(_strafLeft))
        {
            _velocity.x = -Speed;
        }

        else if(Input.GetKey(_strafRight))
        {
            _velocity.x = Speed;
        }
        
        else if(!Input.GetKey(_strafLeft) && !Input.GetKey(_strafRight))
        {
            _velocity.x = 0;
        }
    }
    
         
    #endregion

    
    #region Private Fields

    private bool _isActive;
    private float _speed = 0.05f;
    private Vector3 _velocity;
    private Vector3 _relativeVelocity;

    #endregion
}