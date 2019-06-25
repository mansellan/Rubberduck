using Rubberduck.Resources;
using Rubberduck.UI.Bars.Framework;
using Rubberduck.UI.Command;

namespace Rubberduck.UI.Bars.MenuBars
{
    public class NavigateMenuBar : MenuBar
    {
        protected override void DoInitialize()
        {
            AddCommand<CodeExplorerCommand>("RubberduckMenu_CodeExplorer");
            AddCommand<RegexSearchReplaceCommand>("RubberduckMenu_RegexSearchReplace");

            AddSeparator();
            //AddCommand<FindSymbolCommand>("ContextMenu_FindSymbol", CommandBarIcons.FindSymbol, CommandBarIcons.FindSymbolMask);
            AddCommand<FindAllReferencesCommand>("ContextMenu_FindAllReferences");
            AddCommand<FindAllImplementationsCommand>("ContextMenu_GoToImplementation");
        }
    }
}
