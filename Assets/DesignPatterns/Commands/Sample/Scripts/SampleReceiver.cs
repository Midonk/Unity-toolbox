using UnityEngine;
using Patterns.Command;

public class SampleReceiver : MonoBehaviour, IReceiver<SampleReceiver>
{
    #region Properties

    public int ActivationCount => _executionNumber;
         
    #endregion


    #region Unity API

    private void OnGUI() 
    {
        if(GUILayout.Button("Execute"))
        {
            _logInfos = false;
            ExecuteCommand(new SampleCommand());
        }
        
        if(GUILayout.Button("Execute with log"))
        {
            _logInfos = true;
            ExecuteCommand(new SampleCommand());
        }
    }

    #endregion


    #region Main

    public void DoSomething()
    {
        _executionNumber++;
    }
         
    #endregion


    #region Interface Implementation

    public void ExecuteCommand(ICommand<SampleReceiver> command)
    {
        command.Execute(this);
        if(_logInfos && command is ILoggableCommand<SampleReceiver>)
        {
            ILoggableCommand<SampleReceiver> logCommand = (ILoggableCommand<SampleReceiver>) command;
            Debug.Log(logCommand.Info);
        }
    }

    #endregion

    
    #region Private Fields

    private int _executionNumber = 0;
    private bool _logInfos;
         
    #endregion
}