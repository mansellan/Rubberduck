using System.Drawing;
using System.Windows.Input;

namespace Rubberduck.UI.Bars
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
        public CommandBarItem(ICommand command, string captionResourceKey = null, Image image  = null, Image mask = null)
            : base(captionResourceKey, image, mask)
        {
            Command = command;
        }

        public ICommand Command { get; }
    }

    public class CommandBarItem<TCommand> : CommandBarItem, ICommandBarItem<TCommand>
        where TCommand : ICommand
    {
        public CommandBarItem(TCommand command, string captionResourceKey = null, Image image = null, Image mask = null)
            : base(command, captionResourceKey, image, mask)
        {
            Command = command;
        }

        public new TCommand Command { get; }
        ICommand ICommandBarItem.Command => Command;
    }
}
