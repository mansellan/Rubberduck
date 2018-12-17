using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using Rubberduck.Resources;
using Rubberduck.VBEditor.SafeComWrappers;
using Rubberduck.VBEditor.SafeComWrappers.Abstract;

namespace Rubberduck.UI.Bars.Framework
{
    public interface IVbeBars : IDisposable
    {
        void Initialize();
        IVBE Vbe { set; }
        IAddIn AddIn { set; }
        IMenuBarFactory MenuBarFactory { set; }
        IToolBarFactory ToolBarFactory { set; }
    }

    public abstract class VbeBars : IVbeBars
    {
        IVBE IVbeBars.Vbe
        {
            set => _vbe = value;
        }

        IAddIn IVbeBars.AddIn
        {
            set => _addIn = value;
        }

        IMenuBarFactory IVbeBars.MenuBarFactory
        {
            set => _menuBarFactory = value;
        }

        IToolBarFactory IVbeBars.ToolBarFactory
        {
            set => _toolBarFactory = value;
        }

        private readonly IList<IVbeMenuBar> _vbeMenuBars = new List<IVbeMenuBar>();
        private readonly IList<IVbeToolBar> _vbeToolBars = new List<IVbeToolBar>();
        private IVBE _vbe;
        private IAddIn _addIn;
        private IMenuBarFactory _menuBarFactory;
        private IToolBarFactory _toolBarFactory;

        public void Initialize()
        {
            Debug.Assert(_vbe != null, "Unable to initialize VBE bars - VBE not set.");
            Debug.Assert(_addIn != null, "Unable to initialize VBE bars - AddIn not set.");
            Debug.Assert(_menuBarFactory != null, "Unable to initialize VBE bars - MenuBarFactory not set.");
            Debug.Assert(_toolBarFactory != null, "Unable to initialize VBE bars - ToolBarFactory not set.");

            DoInitialize();

            foreach (var vbeMenuBar in _vbeMenuBars)
            {
                vbeMenuBar.Initialize();
            }

            foreach (var vbeToolBar in _vbeToolBars)
            {
                vbeToolBar.Initialize();
            }
        }

        protected abstract void DoInitialize();        

        public void Dispose()
        {
            foreach (var vbeMenuBar in _vbeMenuBars)
            {
                vbeMenuBar.Dispose();
            }

            foreach (var vbeToolBar in _vbeToolBars)
            {
                vbeToolBar.Dispose();
            }
        }

        protected TMenuBar AddMenuBar<TMenuBar>(string captionResourceKey, params (VBEKind VbeKind, int ParentId, int? BeforeControlId)[] locations) 
            where TMenuBar : MenuBar
        {
            var vbeLocations = locations.Where(l => l.VbeKind == _vbe.Kind).ToArray();
            if (vbeLocations.Any() == false)
            {
                return null;
            }

            var menuBar = _menuBarFactory.Create<TMenuBar>(captionResourceKey);
            foreach (var vbeLocation in vbeLocations)
            {
                using (var commandBars = _vbe.CommandBars)
                using (var parentMenu = commandBars[vbeLocation.ParentId])
                {
                    _vbeMenuBars.Add(new VbeMenuBar(_vbe, menuBar, parentMenu.Controls, vbeLocation.BeforeControlId));
                }
            }

            return menuBar;          
        }

        protected TToolBar AddToolBar<TToolBar>(string name, CommandBarPosition position = CommandBarPosition.Top)
            where TToolBar : ToolBar
        {
            var toolBar = _toolBarFactory.Create<TToolBar>();
            _vbeToolBars.Add(new VbeToolBar(name, _vbe, toolBar, position));            
            return toolBar;
        }
    }
}
