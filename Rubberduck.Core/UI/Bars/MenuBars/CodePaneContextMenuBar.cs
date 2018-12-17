using System.Resources;
using Rubberduck.UI.Bars.Framework;
using Rubberduck.UI.Command;

namespace Rubberduck.UI.Bars.MenuBars
{
    public class CodePaneContextMenuBar : MenuBar
    {
        public CodePaneContextMenuBar()
            : base("RubberduckMenu")
        {
        }

        protected override void DoInitialize()
        {
            AddMenuBar<RefactorMenuBar>("RubberduckMenu_Refactor");
            AddMenuBar<IndentMenuBar>("SmartIndenterMenu");
            AddSeparator();
            AddCommand<FindSymbolCommand>("ContextMenu_FindSymbol");
            AddCommand<FindAllReferencesCommand>("ContextMenu_FindAllReferences");
            AddCommand<FindAllImplementationsCommand>("ContextMenu_GoToImplementation");
        }
    }
}
