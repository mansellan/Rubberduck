using System;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Windows.Forms;
using Rubberduck.Resources.Menus;

namespace Rubberduck.UI.Bars.Framework
{
    public interface ISelectableBarItem : IBarItem
    {
        bool Enabled { get; }
        string Caption { get; }
        void SetCaption(string captionKey, params object[] args);
        string ToolTipText { get; }
        void SetToolTipText(string toolTipTextKey, params object[] args);
        (Image Image, Image Mask) Image { get; }
        void SetImage(Image image, Image mask);
        void SetImage(string imageKey, string imageMask);
    }

    public abstract class SelectableBarItem : BarItem, ISelectableBarItem
    {
        private readonly ResourceManager _textResources;
        private readonly ResourceManager _iconResources;
        private string _caption;
        private string _toolTipText;
        private (Image Image, Image Mask) _image;
        private bool _enabled;

        protected SelectableBarItem(ResourceManager textResources = null, ResourceManager iconResources = null)
        {
            _textResources = textResources ?? Resources.Menus.Bars.ResourceManager;
            _iconResources = iconResources ?? Resources.CommandBarIcons.ResourceManager;
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
            Caption = captionKey == null ? "Wibble" : string.Format(_textResources?.GetString(captionKey) ?? "Wibble", args);
        }

        public string ToolTipText
        {
            get => _caption;
            private set => SetAndNotify(ref _toolTipText, value);
        }
        public void SetToolTipText(string toolTipTextKey, params object[] args)
        {
            ToolTipText = toolTipTextKey == null ? "Wibble" : string.Format(_textResources?.GetString(toolTipTextKey) ?? "Wibble", args);
        }

        public (Image Image, Image Mask) Image
        {
            get => _image;
            private set => SetAndNotify(ref _image, value);
        }

        public void SetImage(string imageKey, string maskKey)
        {
            if (imageKey != null && maskKey != null)
            {
                var image = _iconResources.GetObject(imageKey) as Image;
                var mask = _iconResources.GetObject(maskKey) as Image;
                Image = (image, mask);
            }
        }

        public void SetImage(Image image, Image mask)
        {
            if (image != null & mask != null)
            {
                Image = (image, mask);
            }
        }

        public override void EvaluateAvailability(object state)
        {
            Enabled = state is BarItemAvailability availability && availability != BarItemAvailability.Disabled;
            base.EvaluateAvailability(state);
        }

        protected virtual ResourceManager ResourceManager { get; set; }
    }
}
