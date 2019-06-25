using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Rubberduck.VBEditor.SafeComWrappers;
using Rubberduck.VBEditor.SafeComWrappers.Abstract;

namespace Rubberduck.UI.Bars.Framework
{
    public interface IVbeMenuBar : IDisposable
    {
        void Initialize();
    }

    public class VbeMenuBar : IVbeMenuBar
    {
        private readonly IVBE _vbe;
        private readonly ICommandBarControls _parent;
        private readonly int? _beforeControlIndex;
        private ICommandBarPopup _popup;
        private readonly MenuBar _menuBar;
        private readonly bool _beginsGroup;
        private readonly IDictionary<ICommandBarItem, ICommandBarControl> _commandBarItems = new Dictionary<ICommandBarItem, ICommandBarControl>();
        private readonly IList<IVbeMenuBar> _vbeMenuBars = new List<IVbeMenuBar>();
        private readonly CompositeDisposable _disposables = new CompositeDisposable();

        public VbeMenuBar(IVBE vbe, MenuBar menuBar, ICommandBarControls parent, int? beforeControlId)
            : this(vbe, menuBar, parent, false)
        {
            if (beforeControlId.HasValue) 
            {
                _beforeControlIndex = parent.GetIndex(beforeControlId.Value);
            }
        }

        private VbeMenuBar(IVBE vbe, MenuBar menuBar, ICommandBarControls parent, bool beginsGroup)
        {
            _vbe = vbe;
            _parent = parent;
            _menuBar = menuBar;
            _beginsGroup = beginsGroup;
            _disposables.Add(_parent);
        }

        void IVbeMenuBar.Initialize()
        {
            _menuBar.Initialize();

            var beginsGroup = _beginsGroup;
            using (_parent)
            {
                _popup = _parent.AddPopup(_beforeControlIndex);
            }                
            _popup.BeginsGroup = beginsGroup;           
            _disposables.Add(_popup);
            _popup.Caption = _menuBar.Caption;
            
            
            using (var controls = _popup.Controls)
            {
                foreach (var barItem in _menuBar)
                {
                    switch (barItem)
                    {
                        case SeparatorBarItem _:
                            beginsGroup = true;
                            break;

                        case MenuBar menuBar:
                            using (var childControls = _popup.Controls)
                            {
                                var vbeMenuBar = new VbeMenuBar(_vbe, menuBar, childControls, beginsGroup) as IVbeMenuBar;                                
                                vbeMenuBar.Initialize();
                                _vbeMenuBars.Add(vbeMenuBar);                                
                            }
                            beginsGroup = false;
                            break;

                        case CommandBarItem commandBarItem:
                            var button = controls.AddButton();
                            _commandBarItems[commandBarItem] = button;

                            button.Picture = commandBarItem.Image.Image;
                            button.Mask = commandBarItem.Image.Mask;
                            button.ApplyIcon();

                            button.BeginsGroup = beginsGroup;
                            button.Caption = commandBarItem.Caption;
                            //button.ShortcutText = commandBarItem.ShortcutText;
                            //button.TooltipText = commandBarItem.TooltipText;

                            _disposables.Add(button);
                            _disposables
                                .Add(Observable.FromEventPattern<CommandBarButtonClickEventArgs>(h => button.Click += h, h => button.Click -= h)
                                .Subscribe(_ =>  commandBarItem.Command.Execute(null)));

                            beginsGroup = false;
                            break;

                    }

                    barItem.PropertyChanged += BarItem_PropertyChanged;
                }
            }
        }

        private void BarItem_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                
            }
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}
