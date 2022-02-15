using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour
{
    #region Exposed

    [SerializeField]
    private TouchBehaviour[] _touchBehaviours;

    [SerializeField]
    private bool _debug;

    #endregion

    
    #region Events

    public delegate void TouchCountHandler(int touchCount);
    public event TouchCountHandler OnTouchCountChanged;

    #endregion

    
    #region Properties

    private int TouchCount
    {
        get => _touchCount;
        set
        {
            var correctedValue = Mathf.Max(0, value);
            if(correctedValue != _touchCount)
            {
                _touchCount = correctedValue;
                if (_debug) Debug.Log($"Touch count changed => <color=cyan>{_touchCount}</color>");
                OnTouchCountChanged?.Invoke(_touchCount);
            }
        }
    }
         
    #endregion


    #region Unity API

    private void OnEnable()
    {
        OnTouchCountChanged += CleanupActiveBehaviours;
        OnTouchCountChanged += ActivateBehaviours;
    }

    private void OnDisable() 
    {
        OnTouchCountChanged -= CleanupActiveBehaviours;
        OnTouchCountChanged -= ActivateBehaviours;
    }

    void Update()
    {
        TouchCount = Input.touchCount;
        var dt = Time.deltaTime;
        
        for (int i = 0; i < _activeBehaviours.Count; i++)
        {
            var behaviour = _activeBehaviours[i];
            behaviour.Tick(dt);
        }
    }
         
    #endregion


    #region Main

    private void CleanupActiveBehaviours(int touchCount)
    {
        if(_debug) Debug.Log($"Cleaning <color=cyan>{_activeBehaviours.Count}</color> active behaviours");
        _behavioursToTest = new List<TouchBehaviour>();
        _behavioursToTest.AddRange(_touchBehaviours);
        for (int i = _activeBehaviours.Count - 1; i >= 0; i--)
        {
            var behaviour = _activeBehaviours[i];
            _behavioursToTest.Remove(behaviour);
            if(behaviour.HasEnoughTouch(touchCount)) continue;

            behaviour.Deactivate();
            _activeBehaviours.Remove(behaviour);
        }

        if(_debug) Debug.Log($"<color=cyan>{_activeBehaviours.Count}</color> remaining active behaviours");
    }

    private void ActivateBehaviours(int touchCount)
    {
        var behavioursToActivate = new List<TouchBehaviour>();
        for (int i = 0; i < _behavioursToTest.Count; i++)
        {
            var behaviour = _behavioursToTest[i];
            if(!behaviour.HasEnoughTouch(touchCount)) continue;

            behavioursToActivate.Add(behaviour);
        }

        for (int i = 0; i < behavioursToActivate.Count; i++)
        {
            var behaviour = behavioursToActivate[i];
            behaviour.Activate();
            _activeBehaviours.Add(behaviour);
        }

        if(_debug) Debug.Log($"Activated <color=cyan>{behavioursToActivate.Count}</color> behaviours\n" + 
                             $"Total active behaviours: <color=cyan>{_activeBehaviours.Count}</color>");
    }
         
    #endregion

    
    #region Private Fields

    private int _touchCount;   
    private List<TouchBehaviour> _behavioursToTest;
    private List<TouchBehaviour> _activeBehaviours = new List<TouchBehaviour>();

    #endregion
}