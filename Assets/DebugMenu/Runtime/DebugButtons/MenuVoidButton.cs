using TF.DebugMenu.Core;

namespace TF.DebugMenu.Buttons
{
    internal class MenuVoidButton : MenuButtonBase
    {
        public override void Execute()
        {
            DebugAttributeRegistry.InvokeMethod(_path);
        }
    }
}