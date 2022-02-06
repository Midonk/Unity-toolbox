namespace DebugMenu
{
    internal class MenuVoidButton : MenuButtonBase
    {
        public override void Execute()
        {
            DebugAttributeRegistry.InvokeMethod(_path);
        }
    }
}