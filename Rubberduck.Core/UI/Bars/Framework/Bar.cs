using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Input;

namespace Rubberduck.UI.Bars
{
    public interface IBar : ISelectableBarItem, IReadOnlyList<IBarItem>
    {
        IBarItemFactory BarItemFactory { set; }
        void Initialize();
    }

    public abstract class Bar : SelectableBarItem, IBar
    {
        private IBarItemFactory _barItemFactory;
        private readonly IList<IBarItem> _barItems = new List<IBarItem>();
        private readonly IDictionary<IBarItem, BarItemAvailability> _barItemAvailabilities = new Dictionary<IBarItem, BarItemAvailability>();
        private int _separatorIndex = 1;

        protected Bar(string captionResourceKey = null, Image image = null, Image mask = null)
            : base(captionResourceKey, image, mask)
        {
        }

        public void Initialize()
        {
            // TODO!!!
            DoInitialize();
        }

        protected abstract void DoInitialize();

        public sealed override void EvaluateAvailability(object state)
        {
            _barItemAvailabilities.Clear();

            DoEvaluateAvailability(state);

            foreach (var barItemAvailability in _barItemAvailabilities)
            {
                barItemAvailability.Key.EvaluateAvailability(barItemAvailability.Value);
            }

            foreach (var bar in _barItems.OfType<IBar>())
            {
                bar.EvaluateAvailability(state);
            }
        }

        protected virtual void DoEvaluateAvailability(object state)
        {
            // override template
        }

        protected ICommandBarItem<TCommand> AddCommand<TCommand>(string captionResourceKey)
            where TCommand : class, ICommand
        {
            return AddCommand<TCommand>(captionResourceKey, null, null);
        }

        protected ICommandBarItem<TCommand> AddCommand<TCommand>(string captionResourceKey, Image image, Image mask)
            where TCommand : class, ICommand
        {
            var commandItem = _barItemFactory.CreateCommandBarItem<TCommand>(captionResourceKey, image, mask);
            _barItems.Add(commandItem);
            return commandItem;
        }

        protected SeparatorBarItem AddSeparator()
        {
            var separator = _barItemFactory.CreateSeparatorBarItem(_separatorIndex++);
            _barItems.Add(separator);
            return separator;
        }

        protected TMenuBar AddMenuBar<TMenuBar>(string captionResourceKey = null, Image image = null, Image mask = null)
            where TMenuBar : MenuBar
        {
            var menuBar = _barItemFactory.CreateMenuBar<TMenuBar>(captionResourceKey, image, mask);
            _barItems.Add(menuBar);
            return menuBar;
        }

        protected void Disable(ISelectableBarItem barItem)
        {
            _barItemAvailabilities[barItem] = BarItemAvailability.Disabled;
        }

        protected void Hide(IBarItem barItem)
        {
            _barItemAvailabilities[barItem] = BarItemAvailability.Hidden;
        }

        IBarItemFactory IBar.BarItemFactory
        {
            set => _barItemFactory = value;
        }
        public IBarItem this[int index] => _barItems[index];
        public int Count => _barItems.Count;
        public IEnumerator<IBarItem> GetEnumerator() => _barItems.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => _barItems.GetEnumerator();
        
    }
}
