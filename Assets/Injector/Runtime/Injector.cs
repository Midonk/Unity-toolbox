using System;
using System.Reflection;
using UnityEngine;

[System.Serializable]
public class Injector
{
    public Injector()
    {
        SourceType = null;
    }


    #region Exposed

    [SerializeField] protected UnityEngine.Object _targetObject;
    [SerializeField] protected UnityEngine.Object _sourceObject;
    
    #endregion


    public virtual void Inject()
    {
        if(!CanInject) return;

        var sourceValue = sourceMember.GetValue(_sourceObject);
        targetMember.SetValue(_targetObject, sourceValue);
        Debug.Log($"Injected {_sourceObject.name} ({sourceMember.Name}) => {_targetObject.name} ({targetMember.Name})");
    }


    #region Private Fields

    public MemberInfo targetMember;
    public MemberInfo sourceMember;

    public int SelectedTargetIndex { get; set; }
    public int SelectedSourceIndex { get; set; }
    public Type SourceType { get; protected set; }
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

    #endregion
}