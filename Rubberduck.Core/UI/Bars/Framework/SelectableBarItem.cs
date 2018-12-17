using System;
using System.Drawing;
using System.Globalization;
using System.Resources;
using Rubberduck.Resources.Menus;

namespace Rubberduck.UI.Bars.Framework
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
        private readonly ResourceManager _resourceManager;
        private string _caption;
        private string _toolTipText;
        private bool _enabled;

        protected SelectableBarItem(string captionResourceKey = null, string toolTipTextKey = null)
        {
            _resourceManager = Resources.Menus.Bars.ResourceManager;

            Caption = _resourceManager.GetString(captionResourceKey);
        }

        public bool Enabled
        {
            get => _enabled;
            private set => SetAndNotify(ref _enabled, value);
        }

        public string Caption
        {
            get => _caption;
            private set => SetAndNotify(ref _caption, value);
        }
        public void SetCaption(string captionKey, params object[] args)
        {
            Caption = string.Format(_resourceManager.GetString(captionKey), args);
        }

        public string ToolTipText
        {
            get => _caption;
            private set => SetAndNotify(ref _toolTipText, value);
        }
        public void SetToolTipText(string toolTipTextKey, params object[] args)
        {
            ToolTipText = string.Format(_resourceManager.GetString(toolTipTextKey), args);
        }

        public Image Image { get; }
        public Image Mask { get; }

        public override void EvaluateAvailability(object state)
        {
            Enabled = state is BarItemAvailability availability && availability != BarItemAvailability.Disabled;
            base.EvaluateAvailability(state);
        }

        protected virtual ResourceManager ResourceManager { get; set; }
    }
}
