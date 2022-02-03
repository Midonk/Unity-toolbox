using UnityEngine.EventSystems;

namespace DebugMenu
{
    internal class MenuVoidButton : MenuButtonBase
    {
        public override void OnUpdateSelected(BaseEventData eventData)
        {
            throw new System.NotImplementedException();
        }

        protected override void Execute()
        {
            DebugAttributeRegistry.InvokeMethod(_path);
        }
    }
}