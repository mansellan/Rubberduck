using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace Rubberduck.UI.Bars.Framework
{
    public interface IBarItemFactory
    {
        ICommandBarItem<TCommand> CreateCommandBarItem<TCommand>(string captionKey = null, string toolTipTextKey = null)
            where TCommand : class, ICommand;

        TMenuBar CreateMenuBar<TMenuBar>(string captionKey = null, string toolTipTextKey = null)
            where TMenuBar : MenuBar;

        SeparatorBarItem CreateSeparatorBarItem(int index);
    }

    public class BarItemFactory : IBarItemFactory
    {
        private readonly IDictionary<Type, ICommand> _commands;
        private readonly Func<SeparatorBarItem> _separatorFactory;
        private readonly IMenuBarFactory _menuBarFactory;

        public BarItemFactory(IEnumerable<ICommand> commands, IMenuBarFactory menuBarFactory, Func<SeparatorBarItem> separatorFactory)
        {
            _commands = commands.ToDictionary(command => command.GetType());
            _menuBarFactory = menuBarFactory;
            _separatorFactory = separatorFactory;
        }

        public ICommandBarItem<TCommand> CreateCommandBarItem<TCommand>(string captionKey = null, string toolTipTextKey = null)
            where TCommand : class, ICommand 
        {
            if (_commands.TryGetValue(typeof(TCommand), out var command))
            {
                return new CommandBarItem<TCommand>(command as TCommand, captionKey, toolTipTextKey);
            }
            throw new ApplicationException("TODO");         
        }

        public TMenuBar CreateMenuBar<TMenuBar>(string captionKey = null, string toolTipTextKey = null)
            where TMenuBar : MenuBar
        {
            return _menuBarFactory.Create<TMenuBar>(captionKey, toolTipTextKey);
        }

        public SeparatorBarItem CreateSeparatorBarItem(int index)
        {
            return _separatorFactory();
        }
    }
}
