using System;
using System.Windows.Forms;

namespace SmtpSendMail
{
    public partial class PasswordPrompt : Form
    {
        public PasswordPrompt()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets the typed password.
        /// </summary>
        public string Password
        {
            get
            {
                return txtPassword.Text;
            }
        }

        /// <summary>
        /// Handles the OK button's Click event.
        /// </summary>
        /// <param name="sender">The button object.</param>
        /// <param name="e">The event arguments.</param>
        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}