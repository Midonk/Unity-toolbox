using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TF.DebugMenu.Core
{
    public abstract class MenuButtonBase : Selectable, ICancelHandler, ISubmitHandler, IPointerClickHandler
    {
        #region Exposed

        [SerializeField] protected Text _textUI;

        #endregion


        #region Properties

        protected string Name { get; private set; }

        #endregion


        #region Unity API

        public void OnCancel(BaseEventData eventData)
        {
            DebugMenuHandler.Instance.Return();
        }

        public void OnSubmit(BaseEventData eventData)
        {
            Execute();
            eventData.Use();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Execute();
            eventData.Use();
        }

        #endregion

        
        #region Main

        /// <summary>
        /// Setup the button behaviour
        /// </summary>
        public virtual void Build(string path)
        {
            _path = path;
            var nameIndex = path.LastIndexOf('/');
            Name = nameIndex < 0 ? path : path.Remove(0, nameIndex + 1);
            name = Name;
            _textUI.text = Name;
        }

        /// <summary>
        /// Function supposed to preprocess the debug method call
        /// </summary>
        public abstract void Execute();

        #endregion


        #region Private Fields

        protected string _path;
            
        #endregion
    }
}