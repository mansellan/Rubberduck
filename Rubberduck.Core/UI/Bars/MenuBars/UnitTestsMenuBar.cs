using System.Drawing;
using Rubberduck.Resources;
using Rubberduck.UI.Command;
using Rubberduck.UI.UnitTesting.Commands;

namespace Rubberduck.UI.Bars.MenuBars
{
    public class UnitTestsMenuBar : MenuBar
    {
        public UnitTestsMenuBar(string captionResourceKey, Image image = null, Image mask = null)
            : base(captionResourceKey, image, mask)
        {            
        }

        protected override void DoInitialize()
        {
            AddCommand<TestExplorerCommand>("TestMenu_TextExplorer");
            AddCommand<RunAllTestsCommand>("TestMenu_RunAllTests", CommandBarIcons.AllLoadedTests, CommandBarIcons.AllLoadedTestsMask);

            AddSeparator();
            AddCommand<AddTestModuleCommand>("TestExplorerMenu_AddTestModule");
            AddCommand<AddTestMethodCommand>("TestExplorerMenu_AddTestMethod", CommandBarIcons.flask, CommandBarIcons.flask_mask);
            AddCommand<AddTestMethodExpectedErrorCommand>("TestExplorerMenu_AddExpectedErrorTestMethod", CommandBarIcons.flask_exclamation, CommandBarIcons.flask_exclamation_mask);
        }
    }
}
