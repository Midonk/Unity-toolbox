using UnityEngine.EventSystems;

namespace DebugMenu
{
    internal class MenuNavigationButton : MenuButtonBase
    {
        public override void OnUpdateSelected(BaseEventData eventData)
        {
            throw new System.NotImplementedException();
        }

        protected override void Execute()
        {
            MenuRootPanel.Instance.ChangePanel(_path);
        }
    }
}