using System.Drawing;

namespace Rubberduck.UI.Bars
{
    public abstract class MenuBar : Bar
    {
        protected MenuBar(string captionResourceKey, Image image = null, Image mask = null)
            : base(captionResourceKey, image, mask)
        {            
        }
    }
}
