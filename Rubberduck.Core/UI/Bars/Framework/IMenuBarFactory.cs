namespace Rubberduck.UI.Bars.Framework
{
    public interface IMenuBarFactory
    {
        TMenuBar Create<TMenuBar>(string captionKey, string toolTipTextKey = null) where TMenuBar : MenuBar;
    }
}
