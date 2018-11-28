using System.ComponentModel;
using System.Runtime.CompilerServices;
using Rubberduck.Annotations;

namespace Rubberduck.UI.Bars
{
    public interface IBarItem : INotifyPropertyChanged
    {
        bool Visible { get; }
        void EvaluateAvailability(object state);
    }

    public abstract class BarItem : IBarItem
    {
        private bool _visible;
        
        public bool Visible
        {
            get => _visible;
            private set => SetAndNotify(ref _visible, value);
        }

        public virtual void EvaluateAvailability(object state)
        {
            Visible = state is BarItemAvailability availability && availability != BarItemAvailability.Hidden;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void SetAndNotify<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (field.Equals(value))
            {
                return;
            }

            field = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
