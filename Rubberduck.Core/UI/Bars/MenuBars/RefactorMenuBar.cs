using Rubberduck.Resources;
using Rubberduck.UI.Bars.Framework;
using Rubberduck.UI.Command.Refactorings;

namespace Rubberduck.UI.Bars.MenuBars
{
    public class RefactorMenuBar : MenuBar
    {
        protected override void DoInitialize()
        {
            AddCommand<CodePaneRefactorRenameCommand>("RefactorMenu_Rename");

            AddSeparator();
            //AddCommand<RefactorExtractMethodCommand>("RefactorMenu_ExtractMethod", CommandBarIcons.ExtractMethod, CommandBarIcons.ExtractMethodMask);
            //AddCommand<RefactorExtractInterfaceCommand>("RefactorMenu_ExtractInterface", CommandBarIcons.ExtractInterface, CommandBarIcons.ExtractInterfaceMask);
            //AddCommand<RefactorImplementInterfaceCommand>("RefactorMenu_ImplementInterface", CommandBarIcons.ImplementInterface, CommandBarIcons.ImplementInterfaceMask);

            //AddSeparator();
            //AddCommand<RefactorRemoveParametersCommand>("RefactorMenu_RemoveParameter", CommandBarIcons.RemoveParameters, CommandBarIcons.RemoveParametersMask);
            //AddCommand<RefactorReorderParametersCommand>("RefactorMenu_ReorderParameters", CommandBarIcons.ReorderParameters, CommandBarIcons.ReorderParametersMask);

            //AddSeparator();
            //AddCommand<RefactorMoveCloserToUsageCommand>("RefactorMenu_MoveCloserToUsage");
            //AddCommand<RefactorEncapsulateFieldCommand>("RefactorMenu_EncapsulateField");

            //AddSeparator();
            //AddCommand<RefactorIntroduceParameterCommand>("RefactorMenu_IntroduceParameter", CommandBarIcons.PromoteLocal, CommandBarIcons.PromoteLocalMask);
            //AddCommand<RefactorIntroduceFieldCommand>("RefactorMenu_IntroduceField", CommandBarIcons.AddVariable, CommandBarIcons.AddVariableMask);
        }
    }
}
