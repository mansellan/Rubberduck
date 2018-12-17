using System.Drawing;

namespace Rubberduck.UI.Bars.Framework
{
    public interface IToolBarFactory
    {
        TToolBar Create<TToolBar>() where TToolBar : ToolBar;
    }
}
