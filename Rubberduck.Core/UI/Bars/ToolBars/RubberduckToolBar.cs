using Rubberduck.Resources;
using Rubberduck.UI.Bars.Framework;
using Rubberduck.UI.Command;
using Rubberduck.VBEditor.SafeComWrappers;

namespace Rubberduck.UI.Bars.ToolBars
{
    public class RubberduckToolBar : ToolBar
    {
        public ICommandBarItem<RefreshCommand> Refresh { get; private set; }

        public RubberduckToolBar()
            : base()
        {
        }

        protected override void DoInitialize()
        {
            Refresh = AddCommand<RefreshCommand>("RubberduckMenu_Refresh"); //, ButtonStyle.IconAndCaption); - TODO - separate factories...
        }

        protected override void DoEvaluateAvailability(object state)
        {
            //Hide(this);
        }
    }
}
