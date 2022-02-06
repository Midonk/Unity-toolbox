using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DebugMenu
{
    public abstract class MenuButtonBase : Selectable, ICancelHandler, ISubmitHandler, IPointerClickHandler
    {
        [SerializeField] protected Text _textUI;

        protected string Name { get; private set; }
        
        public virtual void Build(string path)
        {
            _path = path;
            var nameIndex = path.LastIndexOf('/');
            Name = nameIndex < 0 ? path : path.Remove(0, nameIndex + 1);
            name = Name;
            _textUI.text = Name;
        }

        public abstract void Execute();

        public void OnCancel(BaseEventData eventData)
        {
            MenuRootPanel.Instance.Return();
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

        protected string _path;
    }
}