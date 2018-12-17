using System.Drawing;
using System.Resources;

namespace Rubberduck.UI.Bars.Framework
{
    public abstract class MenuBar : Bar
    {
        protected MenuBar(string captionKey = null, string toolTipTextKey = null)
            : base(captionKey, toolTipTextKey)
        {
        }
    }
}
