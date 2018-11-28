using System;
using Rubberduck.UI.Command;

namespace Rubberduck.UI.Bars.MenuBars
{
    public class RubberduckMenuBarTest : MenuBar
    {
        public RubberduckMenuBarTest()
            : base("RubberduckMenuBar")
        {
        }

        [Order(1)]
        public RefreshCommand RefreshCommand { get; set; }

        [Order(2)]
        public SeparatorBarItem Separator1 { get; set; }

    }

    public class Order : Attribute
    {
        public Order(int ordinal)
        {
        }
    }
}
