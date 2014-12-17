using System;
using System.Windows.Forms;
using ComponentPro.Net;
using ComponentPro.Net.Mail;

namespace SmtpSendMail
{
    public partial class Settings : Form
    {
        private SecurityMode _security;
        private bool _sign;
        private bool _encrypt;
        private string _cert;
        private int _suites;

        private string _server;
        private int _port;
        private int _timeout;
        private string _userName;
        private string _password;
        private SmtpAuthenticationMethod _method;

        private string _proxyServer;
        private string _proxyUserName;
        private string _proxyPassword;
        private string _proxyDomain;
        private int _proxyPort;
        private ProxyHttpConnectAuthMethod _proxyAuthenticationMethod;
        private ProxyType _proxyType;

        public Settings()
        {
            InitializeComponent();

            Util.PopulateEnum(typeof(SecurityMode), cbxSec);
            Util.PopulateEnum(typeof(SmtpAuthenticationMethod), cbxMethod);
        }

        /// <summary>
        /// Handles the OK button's Click event.
        /// </summary>
        /// <param name="sender">The button object.</param>
        /// <param name="e">The event arguments.</param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            _sign = chkSign.Checked;
            _encrypt = chkEncrypt.Checked;
            switch (cbxSec.SelectedIndex)
            {
                case 0:
                    _security = SecurityMode.None;
                    break;

                case 1:
                    _security = SecurityMode.Explicit;
                    break;

                case 2:
                    _security = SecurityMode.Implicit;
                    break;
            }

            if (txtServer.Text.Trim().Length == 0)
            {
                MessageBox.Show("Please enter SMTP server", "Error");
                return;
            }

            _server = txtServer.Text;

            try
            {
                _timeout = int.Parse(txtTimeout.Text);
            }
            catch (FormatException)
            {
                _timeout = 60000;
                MessageBox.Show("Invalid timeout", "Error");
                return;
            }

            try
            {
                int serverport = int.Parse(txtPort.Text);
                if (serverport < 1 || serverport > 65535)
                {
                    MessageBox.Show("Invalid port, port must be from 1->65535", "Error");
                    return;
                }
                _port = serverport;
            }
            catch (FormatException)
            {
                MessageBox.Show("Invalid port, port must be from 1->65535", "Error");
                return;
            }

            if (txtProxyPort.Text.Length > 0)
                try
                {
                    int pport;
                    pport = int.Parse(txtProxyPort.Text);
                    if (pport < 1 || pport > 65535)
                    {
                        MessageBox.Show("Invalid port number, must be between 1 and 65535");
                        return;
                    }
                    _proxyPort = pport;
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Invalid port: " + exc.Message);
                    return;
                }

            if (txtProxyPassword.Text.Length > 0 && txtProxyUserName.Text.Length == 0)
            {
                MessageBox.Show("Please enter user name");
                return;
            }

            _userName = txtUserName.Text;
            _password = txtPassword.Text;

            _method = (SmtpAuthenticationMethod)cbxMethod.SelectedItem;
            _cert = txtCertificate.Text;

            _proxyServer = txtProxyHost.Text;
            _proxyType = (ProxyType)cbxType.SelectedIndex;
            _proxyAuthenticationMethod = (ProxyHttpConnectAuthMethod)cbxProxyMethod.SelectedIndex;
            _proxyUserName = txtProxyUserName.Text;
            _proxyPassword = txtProxyPassword.Text;
            _proxyDomain = txtProxyDomain.Text;

            this.DialogResult = DialogResult.OK;
        }

        public SecurityMode Security
        {
            get
            {
                return _security;
            }
            set
            {
                _security = value;
            }
        }

        public bool Sign
        {
            get
            {
                return _sign;
            }
            set
            {
                _sign = value;
            }
        }

        public bool Encrypt
        {
            get
            {
                return _encrypt;
            }
            set
            {
                _encrypt = value;
            }
        }

        public string Cert
        {
            get
            {
                return _cert;
            }
            set
            {
                _cert = value;
            }
        }

        public string Server
        {
            get
            {
                return _server;
            }
            set
            {
                _server = value;
            }
        }

        public int Port
        {
            get
            {
                return _port;
            }
            set
            {
                _port = value;
            }
        }

        public int Timeout
        {
            get
            {
                return _timeout;
            }
            set
            {
                _timeout = value;
            }
        }

        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = value;
            }
        }

        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
            }
        }

        public SmtpAuthenticationMethod Method
        {
            get
            {
                return _method;
            }
            set
            {
                _method = value;
            }
        }

        public int Suites
        {
            get { return _suites; }
            set { _suites = value; }
        }

        #region Proxy



        public string ProxyServer
        {
            get
            {
                return _proxyServer;
            }
            set
            {
                _proxyServer = value;
            }
        }

        public string ProxyUserName
        {
            get
            {
                return _proxyUserName;
            }
            set
            {
                _proxyUserName = value;
            }
        }

        public string ProxyPassword
        {
            get
            {
                return _proxyPassword;
            }
            set
            {
                _proxyPassword = value;
            }
        }

        public string ProxyDomain
        {
            get
            {
                return _proxyDomain;
            }
            set
            {
                _proxyDomain = value;
            }
        }

        public int ProxyPort
        {
            get
            {
                return _proxyPort;
            }
            set
            {
                _proxyPort = value;
            }
        }

        public ProxyHttpConnectAuthMethod ProxyAuthenticationMethod
        {
            get
            {
                return _proxyAuthenticationMethod;
            }
            set
            {
                _proxyAuthenticationMethod = value;
            }
        }

        public ProxyType ProxyType
        {
            get
            {
                return _proxyType;
            }
            set
            {
                _proxyType = value;
            }
        }

        #endregion

        /// <summary>
        /// Handles the form's Load event.
        /// </summary>
        /// <param name="sender">The form object.</param>
        /// <param name="e">The event arguments.</param>
        private void SecuritySettings_Load(object sender, EventArgs e)
        {
            txtCertificate.Text = _cert;

            chkEncrypt.Checked = _encrypt;
            chkSign.Checked = _sign;

            switch (_security)
            {
                case SecurityMode.Implicit:
                    cbxSec.SelectedIndex = 2;
                    break;

                case SecurityMode.Explicit:
                    cbxSec.SelectedIndex = 1;
                    break;

                case SecurityMode.None:
                    cbxSec.SelectedIndex = 0;
                    break;
            }

            if (_timeout >= 1000)
                txtTimeout.Text = _timeout.ToString();
            txtServer.Text = _server;
            if (_port > 0)
                txtPort.Text = _port.ToString();
            txtUserName.Text = _userName;
            txtPassword.Text = _password;

            switch (_method)
            {
                case SmtpAuthenticationMethod.Auto:
                    cbxMethod.SelectedIndex = 0;
                    break;
                case SmtpAuthenticationMethod.SaslCramMd5:
                    cbxMethod.SelectedIndex = 1;
                    break;
                case SmtpAuthenticationMethod.SaslDigestMd5:
                    cbxMethod.SelectedIndex = 2;
                    break;
                case SmtpAuthenticationMethod.SaslLogin:
                    cbxMethod.SelectedIndex = 3;
                    break;
                case SmtpAuthenticationMethod.SaslNtlm:
                    cbxMethod.SelectedIndex = 4;
                    break;
                case SmtpAuthenticationMethod.SaslPlain:
                    cbxMethod.SelectedIndex = 5;
                    break;
            }

            txtProxyUserName.Text = _proxyUserName;
            txtProxyPassword.Text = _proxyPassword;
            txtProxyDomain.Text = _proxyDomain;
            txtProxyHost.Text = _proxyServer;
            if (_proxyPort > 0)
                txtProxyPort.Text = _proxyPort.ToString();
            cbxProxyMethod.SelectedIndex = (int)_proxyAuthenticationMethod;
            cbxType.SelectedIndex = (int)_proxyType;
        }

        /// <summary>
        /// Handles the Certificate browe button's Click event.
        /// </summary>
        /// <param name="sender">The button object.</param>
        /// <param name="e">The event arguments.</param>
        private void btnCertBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Select a certificate file";
            dlg.FileName = txtCertificate.Text;
            dlg.Filter = "All files|*.*";
            dlg.FilterIndex = 1;
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtCertificate.Text = dlg.FileName;
            }
        }

        /// <summary>
        /// Handles the combo box proxy type's SelectedIndexChanged event.
        /// </summary>
        /// <param name="sender">The combo box.</param>
        /// <param name="e">The event arguments.</param>
        private void cbx_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool enable = cbxType.SelectedIndex > 0;

            cbxProxyMethod.Enabled = cbxType.SelectedIndex == (int)ProxyType.HttpConnect; // Authentication method is available for HTTP Connect only.
            txtProxyDomain.Enabled = cbxProxyMethod.Enabled && cbxProxyMethod.SelectedIndex == (int)ProxyHttpConnectAuthMethod.Ntlm; // Domain is available for NTLM authentication method only.
            txtProxyUserName.Enabled = enable/* && cbxType.SelectedIndex != (int)ProxyType.SendToProxy*/; // User name and password are ignored with SendToProxy proxy type.
            txtProxyPassword.Enabled = enable/* && cbxType.SelectedIndex != (int)ProxyType.SendToProxy*/;
            txtProxyHost.Enabled = enable; // Proxy host and port are not available in NoProxy type.
            txtProxyPort.Enabled = enable;
        }
    }
}