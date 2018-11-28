using Rubberduck.UI.Command;

namespace Rubberduck.UI.Bars.MenuBars
{
    public class ToolsMenuBar : MenuBar
    {
        public ToolsMenuBar(string captionResourceKey)
            : base(captionResourceKey)
        {            
        }

        protected override void DoInitialize()
        {
            AddCommand<CodeMetricsCommand>("RubberduckMenu_CodeMetrics");
            AddCommand<ToDoExplorerCommand>("ToolsMenu_TodoItems");
            AddCommand<RegexAssistantCommand>("ToolsMenu_RegexAssistant");
            AddCommand<ExportAllCommand>("ToolsMenu_ExportProject");
        }
    }
}
