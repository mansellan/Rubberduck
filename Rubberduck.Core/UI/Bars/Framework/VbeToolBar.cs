using Rubberduck.VBEditor.SafeComWrappers;

namespace Rubberduck.UI.Bars
{
    public interface IVbeToolBar
    {
        CommandBarPosition Position { get; }
    }

    public abstract class VbeToolBar : ToolBar
    {
        protected VbeToolBar(string key) 
            : base(key)
        {
        }

        public CommandBarPosition Position { get; protected set; }
    }
}
