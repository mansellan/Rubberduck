using System.Drawing;

namespace Rubberduck.UI.Bars.MenuBars
{
    public interface IMenuBarFactory
    {
        TMenuBar Create<TMenuBar>(string captionResourceKey, Image image = null, Image mask = null) where TMenuBar : MenuBar;
    }
}
