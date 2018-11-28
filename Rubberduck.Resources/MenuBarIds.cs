
namespace Rubberduck.Resources
{

    public struct BarItemLocation
    {
        public BarItemLocation(VBEKind vbeKind, int parentId, int id)
        {
            VbeKind = vbeKind;
            ParentId = parentId;
            Id = id;
        }

        public VBEKind VbeKind { get; }
        public int ParentId { get; }
        public int Id { get; }
    }

    public static class KnownId
    {
        public static class VB6
        {
            public static class Bar
            {
                public const int Menu_Main = 1;
                public const int ToolBar_Standard = 2;
                public const int ToolBar_Edit = 3;
                public const int Toolbar_Debug = 4;
                public const int Toolbar_FormEditor = 5;
                public const int ContextMenu_Document = 6;
                public const int ContextMenu_DataView_EditTable = 7;
                public const int ContextMenu_DataView_GoToRow = 8;
                public const int ContextMenu_DataView_ViewTable = 9;
                public const int ContextMenu_DataView_Zoom = 10;
                public const int ContextMenu_DataView_ShowPane = 11;
                public const int ContextMenu_DataView_QueryType = 12;
                public const int ContextMenu_ProjectExplorer_Insert = 13;
                public const int ContextMenu_CodeWindow_BreakMode_Toggle = 14;
                public const int ContextMenu_CodeWindow = 15;
                public const int ContextMenu_CodeWindow_BreakMode = 16;
                public const int ContextMenu_WatchWindow = 17;
                public const int ContextMenu_ImmediateWindow = 18;
                public const int ContextMenu_LocalsWindow = 19;
                public const int ContextMenu_FormDesigner = 20;
                public const int ContextMenu_FormDesigner_Controls = 21;
                public const int ContextMenu_ProjectExplorer = 22;
                public const int ContextMenu_ProjectExplorer_BreakMode = 23;
                public const int ContextMenu_FormLayoutWindow = 24;
                public const int ContextMenu_ObjectBrowser = 25;
                public const int ContextMenu_ToolBox = 26;
                public const int ContextMenu_ToolBox_Group = 27;
                public const int ContextMenu_PropertyBrowser = 28;
                public const int ContextMenu_ColorPalette = 29;
                public const int ContextMenu_ProjectExplorer_Project = 30;
                public const int ContextMenu_ProjectExplorer_Folder_Form = 31;
                public const int ContextMenu_ProjectExplorer_Folder_Module = 32;
                public const int ContextMenu_ProjectExplorer_Folder_RelatedDocuments = 33;
                public const int ContextMenu_DockedWindow = 34;
                public const int ContextMenu_DaVinciTools = 35;
                public const int ContextMenu_DataView = 36;
            }

            public static class BarItem
            {
                public const int Menu_File = 0;
                public const int Menu_Edit = 0;
                public const int Menu_View = 0;
                public const int Menu_Project = 0;
                public const int Menu_Format = 0;
                public const int Menu_Debug = 0;
                public const int Menu_Run = 0;
                public const int Menu_Query = 0;
                public const int Menu_Diagram = 0;
                public const int Menu_Tools = 0;
                public const int Menu_AddIns = 0;
                public const int Menu_Window = 30009;
                public const int Menu_Help = 0;
                public const int Command_ListProperties = 2529;
                public const int Command_ProjectProperties = 2578;
                public const int Command_UpdateUserControls = 746;
                public const int Command_ViewCode = 2558;
            }
        }

        public static class VBA
        {
            public static class Bar
            {
                public const int Menu_Main = 1;
                public const int ToolBar_Standard = 2;
                public const int ToolBar_Edit = 3;
                public const int Toolbar_Debug = 4;
                public const int Toolbar_UserForm = 5;
                public const int ContextMenu_Document = 6;
                public const int ContextMenu_ProjectExplorer_Insert = 7;
                public const int ContextMenu_CodeWindow_BreakMode_Toggle = 8;                
                public const int ContextMenu_CodeWindow = 9;
                public const int ContextMenu_CodeWindow_BreakMode = 10;
                public const int ContextMenu_WatchWindow = 11;
                public const int ContextMenu_ImmediateWindow = 12;
                public const int ContextMenu_LocalsWindow = 13;
                public const int ContextMenu_ProjectExplorer = 14;
                public const int ContextMenu_ProjectExplorer_BreakMode = 15;
                public const int ContextMenu_ObjectBrowser = 16;
                public const int ContextMenu_FormDesigner = 17;
                public const int ContextMenu_FormDesigner_Control = 18;
                public const int ContextMenu_FormDesigner_ControlGroup = 19;
                public const int ContextMenu_FormDesigner_Palette = 20;
                public const int ContextMenu_FormDesigner_Toolbox = 21;
                public const int ContextMenu_FormDesigner_MultiPageControl = 22;
                public const int ContextMenu_FormDesigner_DragDrop = 23;
                public const int ContextMenu_PropertyBrowser = 27;
                public const int ContextMenu_DockedWindow = 28;
            }

            public static class BarItem
            {
                public const int Menu_Window = 30009;
                public const int Command_ListProperties = 2529;
                public const int Command_ViewCode = 2558;
                public const int Command_ProjectProperties = 2578;
            }
        }
    }
}

