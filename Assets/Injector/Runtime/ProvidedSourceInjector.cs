using System;
using UnityEngine;

[Serializable]
public class ProvidedSourceInjector<T> : Injector
{
    public ProvidedSourceInjector(T source, Action preprocessInjection = null)
    {
        _source = source;
        SourceType = _source.GetType();
        _preprocessInjection = preprocessInjection;
    }


    #region Exposed

    private Action _preprocessInjection;
    private T _source;

    #endregion


    #region Main

    public override void Inject()
    {
        if(!CanInject) return;

        _preprocessInjection?.Invoke();
        targetMember.SetValue(_targetObject, _source);
        Debug.Log($"Injected {SourceType.Name} => {_targetObject.name} ({targetMember.Name})");
    }
  
    #endregion
}