using System.Drawing;
using System.Windows.Input;

namespace Rubberduck.UI.Bars.Framework
{
    public interface ICommandBarItem : ISelectableBarItem
    {
        ICommand Command { get; }
    }

    public interface ICommandBarItem<out TCommand> : ICommandBarItem
        where TCommand : ICommand
    {
        new TCommand Command { get; }
    }

    public class CommandBarItem : SelectableBarItem, ICommandBarItem
    {
        protected CommandBarItem(ICommand command)
        {
            Command = command;
        }

        public ICommand Command { get; }

        private static string GetDefaultKey(ICommand command)
        {
            var key = command.GetType().Name;
            if (key.EndsWith("Command"))
            {
                key = key.Substring(0, key.Length - "Command".Length);
            }

            return key;
        }
    }

    public class CommandBarItem<TCommand> : CommandBarItem, ICommandBarItem<TCommand>
        where TCommand : ICommand
    {
        public CommandBarItem(TCommand command)
            : base(command)
        {
            Command = command;
        }

        public new TCommand Command { get; }
        ICommand ICommandBarItem.Command => Command;
    }
}
