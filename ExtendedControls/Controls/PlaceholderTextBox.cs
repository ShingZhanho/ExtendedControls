using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ExtendedControls.Controls
{
    public class PlaceholderTextBox : TextBox
    {
        private bool _inheritTextColor;
        
        private bool _inheritTextFont;

        private bool _isPlaceholderEnabled = true;

        private string _placeholderText;

        private static readonly object EVENT_PLACEHOLDERTEXTCHANGED = new object();

        [Browsable(true)]
        [DefaultValue(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Localizable(false)]
        [Category("CatAppearance")]
        public bool InheritTextColor
        {
            get => _inheritTextColor;
            set => _inheritTextColor = value;
        }

        [Browsable(true)]
        [DefaultValue("Placeholder text")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Localizable(true)]
        [Category("CatAppearance")]
        public string PlaceholderText
        {
            get => _placeholderText;
            set => _placeholderText = value;
        }
        
        public event EventHandler PlaceholderTextChanged
        {
            add => Events.AddHandler(EVENT_PLACEHOLDERTEXTCHANGED, value);
            remove => Events.RemoveHandler(EVENT_PLACEHOLDERTEXTCHANGED, value);
        }

        protected virtual void OnPlaceholderTextChanged(EventArgs e)
        {
            if (!(Events[EVENT_PLACEHOLDERTEXTCHANGED] is EventHandler eventHandler)) return;
            eventHandler(this, e);
        }

        protected override void OnTextAlignChanged(EventArgs e)
        {
            TogglePlaceholderTextState();
            base.OnTextAlignChanged(e);
        }

        private void TogglePlaceholderTextState()
        {
            _isPlaceholderEnabled = TextAlign == HorizontalAlignment.Left;
            placeholderTextLabel.Visible = _isPlaceholderEnabled;
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            placeholderTextLabel = new Label();
            SuspendLayout();
            // 
            // placeholderTextLabel
            // 
            placeholderTextLabel.BackColor = BackColor;
            placeholderTextLabel.Location = new Point(0, 0);
            placeholderTextLabel.Name = "placeholderTextLabel";
            placeholderTextLabel.Size = new Size(100, 23);
            placeholderTextLabel.TabIndex = 0;
            placeholderTextLabel.Visible = false;
            if (Text == string.Empty)
            {
                placeholderTextLabel.Text = PlaceholderText;
                placeholderTextLabel.Visible = true;
            }
            ResumeLayout(false);
        }

        private Label placeholderTextLabel;
    }
}