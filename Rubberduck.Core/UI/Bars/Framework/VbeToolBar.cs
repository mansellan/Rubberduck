using System;
using System.Collections.Generic;
using Rubberduck.VBEditor.SafeComWrappers;
using Rubberduck.VBEditor.SafeComWrappers.Abstract;

namespace Rubberduck.UI.Bars.Framework
{
    public interface IVbeToolBar : IDisposable
    {
        CommandBarPosition Position { get; }
        void Initialize();
    }

    public class VbeToolBar : IVbeToolBar
    {
        private readonly string _name;
        private readonly IVBE _vbe;
        private readonly ToolBar _toolBar;
        private IList<ICommandBarControl> _controls = new List<ICommandBarControl>();

        public VbeToolBar(string name, IVBE vbe, ToolBar toolBar, CommandBarPosition position = CommandBarPosition.Top) 
        {
            _name = name;
            _vbe = vbe;
            _toolBar = toolBar;
            Position = position;
        }

        public CommandBarPosition Position { get; protected set; }

        void IVbeToolBar.Initialize()
        {
            _toolBar.Initialize();
            var beginsGroup = false;

            using (var commandBars = _vbe.CommandBars)
            using (var commandBar = commandBars.Add(_name, Position))
            using (var controls = commandBar.Controls)
            {
                commandBar.IsVisible = true;                
                
                foreach (var barItem in _toolBar)
                {
                    switch (barItem)
                    {
                        case SeparatorBarItem _:
                            beginsGroup = true;
                            break;

                        case MenuBar _:
                            // TODO - see if VBE tool can be hacked to support dropdowns... for now, do nothing.
                            beginsGroup = false;
                            break;

                        case CommandBarItem commandBarItem:
                            var toolBarButton = controls.AddButton();
                            toolBarButton.BeginsGroup = beginsGroup;
                            toolBarButton.Style = ButtonStyle.IconAndCaption; // TODO!!
                            toolBarButton.Picture = commandBarItem.Image.Image;
                            toolBarButton.Mask = commandBarItem.Image.Mask;
                            toolBarButton.IsVisible = true; // commandBarItem.Visible;
                            toolBarButton.IsEnabled = true; // commandBarItem.Enabled;
                            toolBarButton.Caption = commandBarItem.Caption;
                            //toolBarButton.TooltipText = commandBarItem.TooltipText;
                            // toolBarButton.Click = TODO;
                            _controls.Add(toolBarButton);

                            beginsGroup = false;
                            break;
                    }
                }
            }
        }

        public void Dispose()
        {
            foreach (var control in _controls)
            {
                control.Dispose();
            }
        }
    }
}
