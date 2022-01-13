using System.Text;
using System.Collections.Generic;
using UnityEngine;

//CLOSED

namespace Thomas.Test.New
{
    [System.Serializable]
    public class EditorInputTrigger
    {
        #region Exposed

        [SerializeField] private InputTrigger[] _triggers;

        #endregion


        #region Properties

        public ICommand[] Commands
        {
            get
            {
                var commands = new List<ICommand>();
                for (int i = 0; i < _triggers.Length; i++)
                {
                    var trigger = _triggers[i];
                    commands.AddRange(trigger.commands);
                }

                return commands.ToArray();
            }
        }

        #endregion


        #region Main

        /// <summary>
        ///     Retreive the commands of the first corresponding trigger
        /// </summary>
        /// <param name="evt">GUI event to process</param>
        /// <returns>Commands corresponding to the matched trigger</returns>
        public ICommand[] GetCommands(Event evt)
        {
            var commands = new List<ICommand>();
            for (int i = 0; i < _triggers.Length; i++)
            {
                var trigger = _triggers[i];
                if(trigger.eventType != evt.type) continue;
                if(evt.isKey)
                {
                    var foundCommands = CheckKey(evt, trigger);
                    if(foundCommands is null) continue;

                    commands.AddRange(foundCommands);
                    evt.Use();
                    break;
                }

                else if(evt.isMouse)
                {
                    var foundCommands = CheckMouse(evt, trigger);
                    if(foundCommands is null) continue;
                    
                    commands.AddRange(foundCommands);
                    evt.Use();
                    break;
                }
            }
            
            return commands.ToArray();
        }

        /// <summary>
        /// Provide useful informations about which input activate which command
        /// </summary>
        /// <returns></returns>
        public string GetInputInfo()
        {
            var infos = new StringBuilder();
            for (int i = 0; i < _triggers.Length; i++)
            {
                var trigger = _triggers[i];
                if(trigger.key == KeyCode.None)
                {
                    BuildMouseInfo(trigger, infos);
                }

                else
                {
                    BuildKeyInfo(trigger, infos);
                }

                if(i == _triggers.Length - 1) continue;

                infos.AppendLine();
            }

            return infos.ToString();
        }
             
        #endregion


        #region Plumbery

        private void BuildKeyInfo(InputTrigger trigger, StringBuilder infos)
        {
            if(trigger.ExcludeMoifiers)
            {
                infos.Append($"{trigger.modifiers.ToString()} + ");
            }

            infos.Append($"{trigger.key} to {trigger.name}"); 
        }

        private void BuildMouseInfo(InputTrigger trigger, StringBuilder infos)
        {
            if(trigger.ExcludeMoifiers)
            {
                infos.Append($"{trigger.modifiers.ToString()} + ");
            }

            infos.Append($"{trigger.mouseButton} to {trigger.name}"); 
        }

        private ICommand[] CheckKey(Event evt, InputTrigger trigger)
        {
            if(evt.keyCode != trigger.key) return null;
            if(evt.modifiers != trigger.modifiers) return null;

            return trigger.commands;
        }

        private ICommand[] CheckMouse(Event evt, InputTrigger trigger)
        {
            if(evt.button != (int)trigger.mouseButton) return null;
            if(evt.modifiers != trigger.modifiers) return null;

            return trigger.commands;
        }
             
        #endregion

        
        [System.Serializable]
        private struct InputTrigger
        {
            public string name;
            public EventType eventType;
            public KeyCode key;
            public EventModifiers modifiers;
            public MouseButton mouseButton;
            public Command[] commands;

            public bool ExcludeMoifiers => ((EventModifiers.None | EventModifiers.FunctionKey | EventModifiers.Numeric | EventModifiers.CapsLock) & modifiers) != modifiers;
        }

        private enum MouseButton
        {
            LeftClick = 0,
            RightClick = 1,
            MiddleClick = 2,
        }
    }
}