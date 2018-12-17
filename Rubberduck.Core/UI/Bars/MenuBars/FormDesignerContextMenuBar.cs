using Rubberduck.UI.Bars.Framework;
using Rubberduck.UI.Command;
using Rubberduck.UI.Command.Refactorings;

namespace Rubberduck.UI.Bars.MenuBars
{
    public class FormDesignerContextMenuBar : MenuBar
    {
        public FormDesignerContextMenuBar(string captionResourceKey)
            : base(captionResourceKey)
        {
        }

        protected override void DoInitialize()
        {
            AddCommand<FormDesignerRefactorRenameCommand>("RefactorMenu_Rename");
            AddCommand<FormDesignerFindAllReferencesCommand>("ContextMenu_FindAllReferences");
        }
    }
}
