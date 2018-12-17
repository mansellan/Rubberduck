using System.Resources;
using Rubberduck.UI.Bars.Framework;
using Rubberduck.UI.Command;
using Rubberduck.UI.UnitTesting.Commands;

namespace Rubberduck.UI.Bars.MenuBars
{
    public class UnitTestsMenuBar : MenuBar
    {
        protected override void DoInitialize()
        {
            AddCommand<TestExplorerCommand>("TestMenu_TextExplorer");
            AddCommand<RunAllTestsCommand>("TestMenu_RunAllTests");

            AddSeparator();
            AddCommand<AddTestModuleCommand>("TestExplorerMenu_AddTestModule");
            AddCommand<AddTestMethodCommand>("TestExplorerMenu_AddTestMethod");
            AddCommand<AddTestMethodExpectedErrorCommand>("TestExplorerMenu_AddExpectedErrorTestMethod");
        }
    }
}
