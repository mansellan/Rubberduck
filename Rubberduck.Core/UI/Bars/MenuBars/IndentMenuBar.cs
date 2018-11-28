using Rubberduck.UI.Command;

namespace Rubberduck.UI.Bars.MenuBars
{
    public class IndentMenuBar : MenuBar
    {
        public IndentMenuBar(string captionResourceKey)
            : base(captionResourceKey)
        {            
        }

        protected override void DoInitialize()
        {
            AddCommand<IndentCurrentProcedureCommand>("IndentCurrentProcedure");
            AddCommand<IndentCurrentModuleCommand>("IndentCurrentModule");
            AddCommand<IndentCurrentProjectCommand>("IndentCurrentProject");
            AddCommand<NoIndentAnnotationCommand>("NoIndentAnnotation");
        }
    }
}
