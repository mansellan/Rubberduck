using System;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Windows.Input;
using Rubberduck.Resources.Menus;

namespace Rubberduck.UI.Bars
{
    public interface ISelectableBarItem : IBarItem
    {
        bool Enabled { get; }
        string Caption { get; }
        Image Image { get; }
        Image Mask { get; }
    }

    public abstract class SelectableBarItem : BarItem, ISelectableBarItem
    {
        private bool _enabled;
        private readonly string _captionResourceKey;
        private readonly Lazy<string> _caption;

        protected SelectableBarItem(string captionResourceKey = null, Image image = null, Image mask = null)
        {
            _captionResourceKey = captionResourceKey;
            Image = image;
            Mask = mask;

            _caption = new Lazy<string>(() => _captionResourceKey == null ? "Wibble" : RubberduckMenus.ResourceManager.GetString(_captionResourceKey, CultureInfo.CurrentUICulture));
        }

        public bool Enabled
        {
            get => _enabled;
            private set => SetAndNotify(ref _enabled, value);
        }

        // TODO - lookup caption, image and mask
        public string Caption => _caption.Value;
        public Image Image { get; }
        public Image Mask { get; }

        public override void EvaluateAvailability(object state)
        {
            Enabled = state is BarItemAvailability availability && availability != BarItemAvailability.Disabled;
            base.EvaluateAvailability(state);
        }
    }
}
