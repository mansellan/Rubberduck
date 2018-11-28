using Rubberduck.Resources;
using Rubberduck.UI.Command;

namespace Rubberduck.UI.Bars.MenuBars
{
    public class RubberduckMenuBar : MenuBar
    {
        public ICommandBarItem<RefreshCommand> Refresh { get; private set;  }
        public UnitTestsMenuBar UnitTests { get; private set;  }
        public IndentMenuBar Indent { get; private set;  }
        public RefactorMenuBar Refactor { get; private set;  }
        public NavigateMenuBar Navigate { get; private set;  }
        public ToolsMenuBar Tools { get; private set;  }
        public ICommandBarItem<InspectionResultsCommand> CodeInspections { get; private set;  }
        public ICommandBarItem<SettingsCommand> Settings { get; private set;  }
        public ICommandBarItem<AboutCommand> About { get; private set;  }

        public RubberduckMenuBar(string captionResourceKey)
            : base(captionResourceKey)
        {
            
        }

        protected override void DoInitialize()
        {
            Refresh = AddCommand<RefreshCommand>("RubberduckMenu_Refresh", CommandBarIcons.arrow_circle_double, CommandBarIcons.arrow_circle_double_mask);

            AddSeparator();
            UnitTests = AddMenuBar<UnitTestsMenuBar>("RubberduckMenu_UnitTests");
            Indent = AddMenuBar<IndentMenuBar>("SmartIndenterMenu");
            Refactor = AddMenuBar<RefactorMenuBar>("RubberduckMenu_Refactor");
            Navigate = AddMenuBar<NavigateMenuBar>("RubberduckMenu_Navigate");
            Tools = AddMenuBar<ToolsMenuBar>("ToolsMenu");
            CodeInspections = AddCommand<InspectionResultsCommand>("RubberduckMenu_CodeInspections");

            AddSeparator();
            Settings = AddCommand<SettingsCommand>("RubberduckMenu_Settings");

            AddSeparator();
            About = AddCommand<AboutCommand>("RubberduckMenu_About");
        }

        protected override void DoEvaluateAvailability(object state)
        {
            //Hide(this);
        }
    }
}
