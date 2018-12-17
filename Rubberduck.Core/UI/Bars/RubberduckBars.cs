using Rubberduck.Resources;
using Rubberduck.UI.Bars.Framework;
using Rubberduck.UI.Bars.MenuBars;
using Rubberduck.UI.Bars.ToolBars;

namespace Rubberduck.UI.Bars
{
    public class RubberduckBars : VbeBars
    {
        public RubberduckToolBar ToolBar { get; private set; }
        public RubberduckMenuBar MainMenu { get; private set; }
        public CodePaneContextMenuBar CodePane { get; private set; }
        public ProjectExplorerContextMenuBar ProjectExplorer { get; private set; }
        public FormDesignerContextMenuBar FormDesigner { get; private set; }

        protected override void DoInitialize()
        {
            ToolBar = AddToolBar<RubberduckToolBar>("RubberduckToolbar");

            MainMenu = AddMenuBar<RubberduckMenuBar>("RubberduckMenu",
                (VBEKind.Hosted, KnownId.VBA.Bar.Menu_Main, KnownId.VBA.BarItem.Menu_Window),
                (VBEKind.Standalone, KnownId.VB6.Bar.Menu_Main, KnownId.VB6.BarItem.Menu_Window));

            CodePane = AddMenuBar<CodePaneContextMenuBar>("RubberduckMenu",
                (VBEKind.Hosted, KnownId.VBA.Bar.ContextMenu_CodeWindow, KnownId.VBA.BarItem.Command_ListProperties),
                (VBEKind.Standalone, KnownId.VB6.Bar.ContextMenu_CodeWindow, KnownId.VB6.BarItem.Command_ListProperties));

            ProjectExplorer = AddMenuBar<ProjectExplorerContextMenuBar>("RubberduckMenu",
                (VBEKind.Hosted, KnownId.VBA.Bar.ContextMenu_ProjectExplorer, KnownId.VBA.BarItem.Command_ProjectProperties),
                (VBEKind.Standalone, KnownId.VB6.Bar.ContextMenu_ProjectExplorer, KnownId.VB6.BarItem.Command_ProjectProperties));

            FormDesigner = AddMenuBar<FormDesignerContextMenuBar>("RubberduckMenu",
                (VBEKind.Hosted, KnownId.VBA.Bar.ContextMenu_FormDesigner, KnownId.VBA.BarItem.Command_ViewCode),
                (VBEKind.Hosted, KnownId.VBA.Bar.ContextMenu_FormDesigner_Control, KnownId.VBA.BarItem.Command_ViewCode),
                (VBEKind.Standalone, KnownId.VB6.Bar.ContextMenu_FormDesigner_Controls, KnownId.VB6.BarItem.Command_ViewCode));
        }
    }
}
