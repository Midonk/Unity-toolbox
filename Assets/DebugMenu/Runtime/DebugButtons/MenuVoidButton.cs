using TF.DebugMenu.Core;

namespace TF.DebugMenu.Buttons
{
    internal class MenuVoidButton : MenuButtonBase
    {
        #region Main

        public override void Execute()
        {
            DebugAttributeRegistry.InvokeMethod(_path);
        }
            
        #endregion
    }
}