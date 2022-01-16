using System;
using System.Reflection;
using UnityEngine;

[System.Serializable]
public class Injector
{
    public Injector(object source, Action preprocessInjection = null)
    {
        SetSource(source);
        _preprocessInjection = preprocessInjection;
    }
    
    public Injector()
    {
        SourceType = null;
    }


    #region Exposed

    [SerializeField] private UnityEngine.Object _targetObject;
    [SerializeField] private UnityEngine.Object _sourceObject;
    [SerializeField] private string _targetMemberName;
    [SerializeField] private string _sourceMemberName;
    
    #endregion


    public void Inject()
    {
        if(!CanInject) return;

        _preprocessInjection?.Invoke();
        var sourceValue = _source ?? m_sourceMember.GetValue(_sourceObject);
        m_targetMember.SetValue(_targetObject, sourceValue);
        Debug.Log($"Injected {sourceValue} => {m_targetMember.Name}");
    }


    #region Private Fields

    public MemberInfo m_targetMember;
    public MemberInfo m_sourceMember;
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
        set => _selectedSourceIndex = value;
    }


    public Type SourceType { get; private set; }
    public void SetSource(object source)
    {
        _source = source;
        SourceType = source.GetType();
    }

    private object _source;
    private int _selectedSourceIndex;
    private int _selectedTargetIndex;
    private Action _preprocessInjection;
         
    #endregion
}