using System;
using System.Reflection;
using UnityEngine;

[System.Serializable]
public class Injector
{
    public Injector(Type sourceType = null)
    {
        SourceType = sourceType;
    }


    #region Exposed

    [SerializeField] private UnityEngine.Object _targetObject;
    [SerializeField] private UnityEngine.Object _sourceObject;
    [SerializeField] private string _targetMemberName;
    [SerializeField] private string _sourceMemberName;
    
    #endregion


    public void Inject()
    {
        Debug.Log("Injection");
    }


    #region Private Fields

    private MemberInfo _targetProperty;
    private MemberInfo _sourceProperty;
    public bool CanInject
    {
        get
        {
            if (SourceType is null)
            {
                return SelectedTargetIndex > 0 && SelectedSourceIndex > 0;
            }

            return SelectedTargetIndex > 0;
        }
    }

    public int SelectedTargetIndex
    {
        get => _selectedTargetIndex;
        set =>_selectedTargetIndex = value;
    }

    public int SelectedSourceIndex
    {
        get => _selectedSourceIndex;
        set
        {
            if(value == _selectedSourceIndex) return;

            SelectedTargetIndex = 0;
            _selectedSourceIndex = value;
        }
    }
    public Type SourceType { get; private set; }

    [SerializeField] private int _selectedSourceIndex;
    [SerializeField] private int _selectedTargetIndex;
         
    #endregion
}