using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Rubberduck.Resources;
using Rubberduck.UI.Bars.MenuBars;
using Rubberduck.VBEditor;
using Rubberduck.VBEditor.SafeComWrappers;
using Rubberduck.VBEditor.SafeComWrappers.Abstract;

namespace Rubberduck.UI.Bars
{
    public interface IVbeBars : IDisposable
    {
        void Initialize();
        IVBE Vbe { set; }
        IAddIn AddIn { set; }
        IMenuBarFactory MenuBarFactory { set; }
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

        private readonly IList<VbeMenuBar> _vbeMenuBars = new List<VbeMenuBar>();
        private IVBE _vbe;
        private IAddIn _addIn;
        private IMenuBarFactory _menuBarFactory;

        public void Initialize()
        {
            Debug.Assert(_vbe != null, "Unable to initialize VBE bars - VBE not set.");
            Debug.Assert(_addIn != null, "Unable to initialize VBE bars - AddIn not set.");
            Debug.Assert(_menuBarFactory != null, "Unable to initialize VBE bars - MenuBarFactory not set.");

            DoInitialize();

            foreach (var vbeMenuBar in _vbeMenuBars)
            {
                vbeMenuBar.Initialize();
            }
        }

        protected abstract void DoInitialize();        

        public void Dispose()
        {
            foreach (var vbeMenuBar in _vbeMenuBars)
            {
                vbeMenuBar.Dispose();
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
    }
}
