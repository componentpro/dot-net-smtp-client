using System;

namespace SmtpSendMail
{
    public class ContentTextBox : System.Windows.Forms.TextBox
    {
        // Indicates whether the message was modified.
        private bool _changed = false;

        /// <summary>
        /// Gets or sets the boolean flag indicating whether the content has been changed.
        /// </summary>
        public bool Changed
        {
            get
            {
                return _changed;
            }
            set
            {
                _changed = value;
            }
        }

        /// <summary>
        /// Handles the TextChanged event.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            _changed = true;
        }
    }
}
