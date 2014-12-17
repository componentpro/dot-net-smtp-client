using System;
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows.Forms;
using ComponentPro;
using SmtpSendMail.Security;
using ComponentPro.Net;
using ComponentPro.Net.Mail;
using System.IO;
using ComponentPro.Security.Certificates;

namespace SmtpSendMail
{
    public partial class SmtpSendMail : Form
    {
        private readonly bool _exception;
        private SecurityMode _sec;
        private bool _sign;
        private bool _encrypt;
        private string _cert;
        private bool _tls;
        private bool _ssl;
        private int _suites;
        private bool _changed;
        private string _mailFileName;
        private AttachmentCollection _attachments;
        private bool _internalChanges;

        private string _server;
        private int _port;
        private string _userName;
        private string _password;
        private SmtpAuthenticationMethod _method;
        private int _timeout;

        #region Proxy

        private string _proxyServer;
        private string _proxyUserName;
        private string _proxyPassword;
        private string _proxyDomain;
        private int _proxyPort;
        private ProxyHttpConnectAuthMethod _proxyAuthenticationMethod;
        private ProxyType _proxyType;

        #endregion

        private bool _disconnect;
        private bool _disconnected;

        public SmtpSendMail()
        {
            try
            {
                InitializeComponent();
            }
            catch (ComponentPro.Licensing.Mail.UltimateLicenseException exc)
            {
                MessageBox.Show(exc.Message, "Error");
                _exception = true;
                return;
            }

            // Attachs the TextChanged event handler to all text boxes.
            txtBcc.TextChanged += txt_TextChanged;
            txtBody.TextChanged += txt_TextChanged;
            txtCc.TextChanged += txt_TextChanged;
            txtFrom.TextChanged += txt_TextChanged;
            txtSubject.TextChanged += txt_TextChanged;
            txtTo.TextChanged += txt_TextChanged;

            // New messages.
            NewMesssage();

#if !Framework4_5
            this.client.CertificateRequired += this.client_CertificateRequired;
            this.client.DisconnectCompleted += this.smtpClient_DisconnectCompleted;
            this.client.AuthenticateCompleted += this.smtpClient_LoginCompleted;
            this.client.CertificateReceived += this.client_CertificateReceived;
            this.client.ConnectCompleted += this.smtpClient_ConnectCompleted;
            this.client.SendCompleted += this.smtpClient_SendCompleted;
#endif
        }

        /// <summary>
        /// Handles the form's Load event.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (_exception)
                this.Close();

            // Load settings from the Registry.
            LoadConfig();
        }

        #region Config

        /// <summary>
        /// Loads settings from the Registry.
        /// </summary>
        void LoadConfig()
        {
            _sec = (SecurityMode)Util.GetIntProperty("SecurityMode", 0);
            _sign = (string)Util.GetProperty("Sign", "False") == "True";
            _encrypt = (string)Util.GetProperty("Encrypt", "False") == "True";
            _cert = (string)Util.GetProperty("Cert", string.Empty);
            _tls = (string)Util.GetProperty("TLS", "True") == "True";
            _ssl = (string)Util.GetProperty("SSL", "True") == "True";
            _suites = Util.GetIntProperty("Suites", 0);
            
            _server = (string)Util.GetProperty("SmtpServer", string.Empty);
            _port = Util.GetIntProperty("SmtpPort", 25);
            _userName = (string)Util.GetProperty("UserName", string.Empty);
            _password = (string)Util.GetProperty("Password", string.Empty);
            _method = (SmtpAuthenticationMethod)Util.GetIntProperty("SmtpAuthenticationMethod", 0);
            _timeout = Util.GetIntProperty("Timeout", 60000);

            _proxyServer = (string)Util.GetProperty("ProxyServer", string.Empty);
            _proxyUserName = (string)Util.GetProperty("ProxyUserName", string.Empty);
            _proxyPassword = (string)Util.GetProperty("ProxyPassword", string.Empty);
            _proxyDomain = (string)Util.GetProperty("ProxyDomain", string.Empty);
            _proxyPort = Util.GetIntProperty("ProxyPort", 1080);
            _proxyAuthenticationMethod = (ProxyHttpConnectAuthMethod)Util.GetIntProperty("ProxyAuthentication", 0);
            _proxyType = (ProxyType)Util.GetIntProperty("ProxyType", 0);
        }

        /// <summary>
        /// Saves settings to the Registry.
        /// </summary>
        void SaveConfig()
        {
            Util.SaveProperty("SecurityMode", (int)_sec);
            Util.SaveProperty("Sign", _sign);
            Util.SaveProperty("Encrypt", _encrypt);
            Util.SaveProperty("Cert", _cert);
            Util.SaveProperty("TLS", _tls);
            Util.SaveProperty("SSL", _ssl);
            Util.SaveProperty("Suites", _suites);

            Util.SaveProperty("SmtpServer", _server);
            Util.SaveProperty("SmtpPort", _port);
            Util.SaveProperty("UserName", _userName);
            Util.SaveProperty("Password", _password);
            Util.SaveProperty("SmtpAuthenticationMethod", (int)_method);
            Util.SaveProperty("Timeout", _timeout);

            Util.SaveProperty("ProxyServer", _proxyServer);
            Util.SaveProperty("ProxyUserName", _proxyUserName);
            Util.SaveProperty("ProxyPassword", _proxyPassword);
            Util.SaveProperty("ProxyDomain", _proxyDomain);
            Util.SaveProperty("ProxyPort", _proxyPort);
            Util.SaveProperty("ProxyAuthentication", (int)_proxyAuthenticationMethod);
            Util.SaveProperty("ProxyType", (int)_proxyType);
        }

        #endregion

        /// <summary>
        /// Handles the content text box's TextChanged event.
        /// </summary>
        /// <param name="sender">The content text box.</param>
        /// <param name="e">The event arguments.</param>
        void txt_TextChanged(object sender, EventArgs e)
        {
            // Changes the form's title if needed.
            bool b = _changed;
            // Message content has been changed.
            _changed = true;
            saveButton.Enabled = true;
            if (b == false)
                SetTitle();
        }

        /// <summary>
        /// Sets the title.
        /// </summary>
        private void SetTitle()
        {
            if (!_internalChanges)
            {
                // Sets form's title.
                string str = "Ultimate Mail Composer and Sender";

                if (_mailFileName != null)
                    str += " - " + Path.GetFileName(_mailFileName);

                if (txtSubject.Text.Length > 0)
                    str += " - " + txtSubject.Text;

                if (_changed)
                    str += "*";

                this.Text = str;
            }
        }

        /// <summary>
        /// Enables/disables the dialog.
        /// </summary>
        /// <param name="enable"></param>
        private void EnableDialog(bool enable)
        {
            Util.EnableCloseButton(this, enable);

            // Enable/Disable form's elements.
            newMessageButton.Enabled = enable;
            openMessageButton.Enabled = enable;
            saveButton.Enabled = enable;
            saveAsButton.Enabled = enable;
            saveAttachmentsButton.Enabled = enable;

            tsbCancel.Enabled = !enable;
            tsbSend.Enabled = enable;
            tsbSettings.Enabled = enable;

            pasteButton.Enabled = enable;
            cutButton.Enabled = enable;
            copyButton.Enabled = enable;
            selectAllButton.Enabled = enable;

            toolStripProgressBar.Visible = !enable;
            toolStripProgressBar.Value = 0;

            txtFrom.Enabled = enable;
            txtBcc.Enabled = enable;
            txtBody.Enabled = enable;
            txtCc.Enabled = enable;
            txtSubject.Enabled = enable;
            txtTo.Enabled = enable;
            cbxAttachment.Enabled = enable;
            btnAddAttachment.Enabled = enable;
            btnRemoveAttachment.Enabled = !enable ? false : (cbxAttachment.Items.Count > 0);
        }

        /// <summary>
        /// Sets the title.
        /// </summary>
        private void txtSubject_TextChanged(object sender, EventArgs e)
        {
            // Changes the form's title whenever the subject is changed.
            SetTitle();
        }

        /// <summary>
        /// Creates new message.
        /// </summary>
        private void NewMesssage()
        {
            _internalChanges = true;
            txtBcc.Text = string.Empty;
            txtBody.Text = string.Empty;
            txtCc.Text = string.Empty;
            txtFrom.Text = string.Empty;
            txtSubject.Text = string.Empty;
            txtTo.Text = string.Empty;
            cbxAttachment.Items.Clear();
            _mailFileName = null;
            _attachments = null;
            _changed = false;
            _internalChanges = false;
            SetTitle();
            saveAttachmentsButton.Enabled = false;
            saveButton.Enabled = false;
        }

        /// <summary>
        /// Opens an existing message.
        /// </summary>
        /// <param name="fileName"></param>
        private void OpenMessage(string fileName)
        {
            _internalChanges = true;
            // Create a new message object.
            MailMessage message = new MailMessage();
            _mailFileName = fileName;
            // Load message from the specified file.
            message.Load(fileName);

            // Update subject and content text boxes.
            txtSubject.Text = message.Subject;
            txtBody.Text = message.BodyText.Replace("\n", "\r\n");

            StringBuilder sb = new StringBuilder();

            foreach (MailAddress to in message.To)
            {
                string address = to.ToString();
                if (address.Length > 0)
                {
                    sb.Append(address);
                    sb.Append(';');
                }
            }
            if (sb.Length > 0)
                sb.Length--;
            txtTo.Text = sb.ToString();

            sb.Length = 0;
            foreach (MailAddress bcc in message.Bcc)
            {
                string address = bcc.ToString();
                if (address.Length > 0)
                {
                    sb.Append(address);
                    sb.Append(';');
                }
            }
            if (sb.Length > 0)
                sb.Length--;
            txtBcc.Text = sb.ToString();

            sb.Length = 0;
            foreach (MailAddress cc in message.Cc)
            {
                string address = cc.ToString();
                if (address.Length > 0)
                {
                    sb.Append(address);
                    sb.Append(';');
                }
            }
            if (sb.Length > 0)
                sb.Length--;
            txtCc.Text = sb.ToString();

            sb.Length = 0;
            foreach (MailAddress from in message.From)
            {
                string address = from.ToString();
                if (address.Length > 0)
                {
                    sb.Append(address);
                    sb.Append(';');
                }
            }
            if (sb.Length > 0)
                sb.Length--;
            txtFrom.Text = sb.ToString();

            cbxAttachment.Items.Clear();

            // Update attachment combo box.
            if (message.Attachments.Count > 0)
            {
                _attachments = new AttachmentCollection();
                foreach (Attachment att in message.Attachments)
                {
                    _attachments.Add(att);
                    cbxAttachment.Items.Add(Path.GetFileName(att.FileName));
                }
                cbxAttachment.SelectedIndex = 0;
                btnRemoveAttachment.Enabled = true;
                saveAttachmentsButton.Enabled = true;
            }
            else
                saveAttachmentsButton.Enabled = false;
            _changed = false;            
            saveButton.Enabled = false;
            _internalChanges = false;
        }

        /// <summary>
        /// Shows Save as dialog and save the message to the specified file.
        /// </summary>
        private void SaveMessageAs()
        {
            SaveFileDialog dlg = new SaveFileDialog();
            try
            {
                dlg.OverwritePrompt = true;
                dlg.Filter = "Email files (*.eml)|*.eml|All files (*.*)|*.*";
                dlg.FilterIndex = 1;
                dlg.Title = "Save the mail as";
                if (dlg.ShowDialog() != DialogResult.OK)
                    return;

                _mailFileName = dlg.FileName;
                SaveMessageAs(dlg.FileName);                
            }
            catch
            {
                MessageBox.Show(dlg.FileName + " is not an email file", "Error");
            }
        }

        /// <summary>
        /// Creates a new Message object.
        /// </summary>
        /// <returns>A new Message object.</returns>
        private MailMessage CreateMessage()
        {
            MailMessage message = new MailMessage();

            message.Subject = txtSubject.Text;
            message.BodyText = txtBody.Text;

            foreach (string to in txtTo.Text.Split(new char[] { ';', ',' }))
            {
                string email = to.Trim();
                if (email.Length > 0)
                    message.To.Add(email);
            }

            foreach (string bcc in txtBcc.Text.Split(new char[] { ';', ',' }))
            {
                string email = bcc.Trim();
                if (email.Length > 0)
                    message.Bcc.Add(email);
            }

            foreach (string cc in txtCc.Text.Split(new char[] { ';', ',' }))
            {
                string email = cc.Trim();
                if (email.Length > 0)
                    message.Cc.Add(email);
            }

            foreach (string from in txtFrom.Text.Split(new char[] { ';', ',' }))
            {
                string email = from.Trim();
                if (email.Length > 0)
                    message.From.Add(email);
            }

            if (_attachments != null)
                foreach (Attachment att in _attachments)
                {
                    message.Attachments.Add(att);
                }

            return message;
        }

        /// <summary>
        /// Saves the current message as a file.
        /// </summary>
        /// <param name="fileName"></param>
        private void SaveMessageAs(string fileName)
        {
            MailMessage message = CreateMessage();
            message.Save(fileName);
            _changed = false;
            saveButton.Enabled = false;
            SetTitle();
        }

        /// <summary>
        /// Saves message, if message file name has not been set, a Save file as dialog will be shown to ask for the destination file name.
        /// </summary>
        private void SaveMessage()
        {
            if (_mailFileName == null)
            {
                SaveMessageAs();
            }
            else
            {
                try
                {
                    SaveMessageAs(_mailFileName);
                }
                catch (Exception exc)
                {
                    MessageBox.Show(string.Format("Unable to save the message to file: '{0}'\r\n{1}", _mailFileName, exc.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
        }

        /// <summary>
        /// Saves attachments to a specific folder.
        /// </summary>
        private void SaveAttachmentAs(string folder)
        {
            foreach (Attachment att in _attachments)
            {
                att.Save(Path.Combine(folder, att.FileName));
            }
        }

        /// <summary>
        /// Saves attachments to a specific folder. A browing folder dialog will be shown.
        /// </summary>
        private void SaveAttachmentAs()
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() != DialogResult.OK)
                return;

            string path = folderBrowserDialog.SelectedPath;
            SaveAttachmentAs(path);
        }

        /// <summary>
        /// If the message has not been saved, shows a message box asking user to save the changes.
        /// </summary>
        /// <returns>A bool value indicating whether the closing action should be cancelled.</returns>
        private bool AskSaving()
        {
            if (_changed)
            {
                DialogResult result = MessageBox.Show("Do you want to save the changes you have made?", "Smtp Send Mail", MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Cancel)
                    return true;
                if (result == DialogResult.Yes)
                    SaveMessage();
            }

            return false;
        }

        private void newMessageButton_Click(object sender, EventArgs e)
        {
            if (!AskSaving())
                NewMesssage();
        }

        private void openMessageButton_Click(object sender, EventArgs e)
        {
            if (!AskSaving())
            {
                OpenFileDialog dlg = new OpenFileDialog();
                try
                {
                    dlg.Filter = "Email files (*.eml)|*.eml|Outlook files (*.msg)|*.msg|All files (*.*)|*.*";
                    dlg.FilterIndex = 1;
                    dlg.Title = "Select an email file";
                    // Show open file dialog.
                    if (dlg.ShowDialog() != DialogResult.OK)
                        return;

                    OpenMessage(dlg.FileName);
                    SetTitle();
                }
                catch
                {
                    MessageBox.Show(dlg.FileName + " is not an email file", "Error");
                }
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (_changed)
                SaveMessage();
        }

        private void saveAsButton_Click(object sender, EventArgs e)
        {
            SaveMessageAs();
        }

        private void saveAttachmentsButton_Click(object sender, EventArgs e)
        {
            SaveAttachmentAs();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cutButton_Click(object sender, EventArgs e)
        {
            txtBody.Cut();
        }

        private void copyButton_Click(object sender, EventArgs e)
        {
            txtBody.Copy();
        }

        private void pasteButton_Click(object sender, EventArgs e)
        {
            txtBody.Paste();
        }

        private void selectAllButton_Click(object sender, EventArgs e)
        {
            txtBody.SelectAll();
        }

        /// <summary>
        /// Handles the add attachment button's Click event.
        /// </summary>
        /// <param name="sender">The button object.</param>
        /// <param name="e">The event arguments.</param>
        private void btnAddAttachment_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            try
            {
                bool b = _changed;

                dlg.Filter = "All files (*.*)|*.*";
                dlg.FilterIndex = 1;
                dlg.Title = "Select a file";
                dlg.Multiselect = true;
                if (dlg.ShowDialog() != DialogResult.OK)
                    return;

                // Creates an AttachmentCollection if _attachments is null.
                if (_attachments == null)
                    _attachments = new AttachmentCollection();

                // Adds selected files to the attachment collection and the combo box as well.
                foreach (string fileName in dlg.FileNames)
                {
                    _attachments.Add(fileName);
                    cbxAttachment.Items.Add(System.IO.Path.GetFileName(fileName));
                }
                // Selects the last item.
                cbxAttachment.SelectedIndex = cbxAttachment.Items.Count - 1;                

                btnRemoveAttachment.Enabled = true;
                _changed = true;
                saveButton.Enabled = true;
                saveAttachmentsButton.Enabled = true;
                if (b == false) // sets the title if needed.
                    SetTitle();
            }
            catch
            {
                MessageBox.Show(dlg.FileName + " is not a file", "Error");
            }
        }

        /// <summary>
        /// Handles the remove attachment button's Click event.
        /// </summary>
        /// <param name="sender">The button object.</param>
        /// <param name="e">The event arguments.</param>
        private void btnRemoveAttachment_Click(object sender, EventArgs e)
        {
            bool b = _changed;

            // Removes an attachment from the collection.
            _attachments.RemoveAt(cbxAttachment.SelectedIndex);
            
            // and the combo box.
            if (_attachments.Count > 0)
            {
                // and the combo box.
                cbxAttachment.Items.RemoveAt(cbxAttachment.SelectedIndex);
            }
            else
            {
                cbxAttachment.SelectedItem = null;
                cbxAttachment.Items.Clear();
            }

            if (cbxAttachment.Items.Count == 0)
            {
                // Disables the "Remove" button. 
                btnRemoveAttachment.Enabled = false;
                _attachments = null;
                saveAttachmentsButton.Enabled = false;
            }
            else if (cbxAttachment.SelectedIndex == -1)
                cbxAttachment.SelectedIndex = cbxAttachment.Items.Count - 1;

            _changed = true;
            saveButton.Enabled = true;
            if (b == false)
                SetTitle();
        }

        /// <summary>
        /// Handles the Send toolbar button's Click event.
        /// </summary>
        /// <param name="sender">The button object.</param>
        /// <param name="e">The event arguments.</param>
#if Framework4_5
        async void tsbSend_Click(object sender, EventArgs e)
#else
        void tsbSend_Click(object sender, EventArgs e)
#endif
        {
            if (string.IsNullOrEmpty(_server))
            {
                MessageBox.Show("Smtp server is not set, please click on the Settings toolbar button to configure", "Error");
                return;
            }

            EnableDialog(false);
            _disconnected = false;

            // Set timeout.
            client.Timeout = _timeout;

            WebProxyEx proxy = new WebProxyEx();
            client.Proxy = proxy;
            if (_proxyServer.Length > 0 && _proxyPort > 0)
            {
                // Set proxy settings.
                proxy.Server = _proxyServer;
                proxy.Port = _proxyPort;
                proxy.UserName = _proxyUserName;
                proxy.Password = _proxyPassword;
                proxy.Domain = _proxyDomain;
                proxy.ProxyType = _proxyType;
                proxy.AuthenticationMethod = _proxyAuthenticationMethod;
            }

#if Framework4_5
            try
            {
                // Asynchronously connect to the SMTP server.
                await client.ConnectAsync(_server, _port, _sec);
            }
            catch (Exception exc)
            {
                Util.ShowError(exc);
                Disconnect();
                return;
            }

            Login();
#else
            // Asynchronously connect to the SMTP server.
            client.ConnectAsync(_server, _port, _sec);
#endif
        }

#if !Framework4_5
        /// <summary>
        /// Handles the client's ConnectCompleted event.
        /// </summary>
        /// <param name="sender">The client object.</param>
        /// <param name="e">The event arguments.</param>
        private void smtpClient_ConnectCompleted(object sender, ExtendedAsyncCompletedEventArgs<string> e)
        {
            if (e.Error != null)
            {
                Util.ShowError(e.Error);
                Disconnect();
                return;
            }

            Login();
        }
#endif

#if Framework4_5
        async void Login()
#else
        void Login()
#endif
        {
            if (_disconnect)
            {
                Disconnect();
                return;
            }

            // Authentication is required?
            if (!string.IsNullOrEmpty(_userName))
            {
                SetStatusText("Logging in...");

#if Framework4_5
                try
                {
                    // Asynchronously login to the server.
                    await client.AuthenticateAsync(_userName, _password, _method);
                }
                catch (Exception exc)
                {
                    Util.ShowError(exc);
                    Disconnect();
                    return;
                }

                SendMessage();
#else
                // Asynchronously login to the server.
                client.AuthenticateAsync(_userName, _password, _method);
#endif
            }
            else
                // Immediately send the message without authenticating.
                SendMessage();
        }

#if !Framework4_5
        /// <summary>
        /// Handles the client's AuthenticateCompleted event.
        /// </summary>
        /// <param name="sender">The client object.</param>
        /// <param name="e">The event arguments.</param>
        private void smtpClient_LoginCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                Util.ShowError(e.Error);
                Disconnect();
                return;
            }

            SendMessage();
        }
#endif

        /// <summary>
        /// Sends the message.
        /// </summary>
#if Framework4_5
        async void SendMessage()
#else
        void SendMessage()
#endif
        {
            if (_disconnect)
            {
                Disconnect();
                return;
            }

            SetStatusText("Sending message...");

#if Framework4_5
            try
            {
                // Asynchronously send the message.
                await client.SendAsync(CreateMessage());
            }
            catch (Exception exc)
            {
                Util.ShowError(exc);
                Disconnect();
                return;
            }

            MessageBox.Show("Message sent.", "Smtp Send Mail");
            Disconnect();
#else
            client.SendAsync(CreateMessage());
#endif
        }

#if !Framework4_5
        /// <summary>
        /// Handles the client's SendCompleted event.
        /// </summary>
        /// <param name="sender">The client object.</param>
        /// <param name="e">The event arguments.</param>
        private void smtpClient_SendCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                Util.ShowError(e.Error);
                Disconnect();
                return;
            }

            if (_disconnect)
            {
                Disconnect();
                return;
            }
            MessageBox.Show("Message sent.", "Smtp Send Mail");
            Disconnect();
        }
#endif

        /// <summary>
        /// Handles the Settings toolbar button's Click event.
        /// </summary>
        /// <param name="sender">The button object.</param>
        /// <param name="e">The event arguments.</param>
        private void tsbSettings_Click(object sender, EventArgs e)
        {
            Settings sec = new Settings();
            sec.Security = _sec;
            sec.Sign = _sign;
            sec.Encrypt = _encrypt;
            sec.Cert = _cert;
            sec.Suites = _suites;
            sec.Server = _server;
            sec.Port = _port;
            sec.UserName = _userName;
            sec.Password = _password;
            sec.Method = _method;
            sec.Timeout = _timeout;

            // Proxy Settings
            sec.ProxyServer = _proxyServer;
            sec.ProxyPort = _proxyPort;
            sec.ProxyUserName = _proxyUserName;
            sec.ProxyPassword = _proxyPassword;
            sec.ProxyDomain = _proxyDomain;
            sec.ProxyAuthenticationMethod = _proxyAuthenticationMethod;
            sec.ProxyType = _proxyType;

            // Popups the Settings form.
            if (sec.ShowDialog() == DialogResult.OK)
            {
                _sec = sec.Security;
                _sign = sec.Sign;
                _encrypt = sec.Encrypt;
                _cert = sec.Cert;
                _suites = sec.Suites;
                _server = sec.Server;
                _port = sec.Port;
                _userName = sec.UserName;
                _password = sec.Password;
                _method = sec.Method;
                _timeout = sec.Timeout;

                _proxyServer = sec.ProxyServer;
                _proxyPort = sec.ProxyPort;
                _proxyUserName = sec.ProxyUserName;
                _proxyPassword = sec.ProxyPassword;
                _proxyDomain = sec.ProxyDomain;
                _proxyAuthenticationMethod = sec.ProxyAuthenticationMethod;
                _proxyType = sec.ProxyType;
            }
        }

        /// <summary>
        /// Handles the form's Closing event.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        protected override void OnClosing(CancelEventArgs e)
        {
            if (AskSaving())
                e.Cancel = true;
            else
                SaveConfig();

            base.OnClosing(e);
        }

        /// <summary>
        /// Handles the form's Closing event.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        protected override void OnClosed(EventArgs e)
        {
            if (tsbCancel.Enabled)
            {
                _disconnect = true;

                while (!_disconnected)
                {
                    System.Threading.Thread.Sleep(50);
                    System.Windows.Forms.Application.DoEvents();
                }
            }
            
            base.OnClosed(e);
        }

        /// <summary>
        /// Handles the Cancel button's Click event.
        /// </summary>
        /// <param name="sender">The button object.</param>
        /// <param name="e">The event arguments.</param>
        private void tsbCancel_Click(object sender, EventArgs e)
        {
            _disconnect = true;
            client.Cancel();
            tsbCancel.Enabled = false;
        }

        /// <summary>
        /// Sets the status text.
        /// </summary>
        /// <param name="str">The new status text.</param>
        private void SetStatusText(string str)
        {
            tssStatus.Text = str;
        }

        /// <summary>
        /// Sets the status text.
        /// </summary>
        /// <param name="str">The new status format text.</param>
        /// <param name="parameters">The parameters.</param>
        private void SetStatusText(string str, params object[] parameters)
        {
            tssStatus.Text = string.Format(str, parameters);
        }

        /// <summary>
        /// Closes the connection and release used resources.
        /// </summary>
#if Framework4_5
        async void Disconnect()
#else
        void Disconnect()
#endif
        {
            SetStatusText("Disconnecting...");
#if Framework4_5
            try
            {
                await client.DisconnectAsync();
            }
            catch (Exception ex)
            {
                Util.ShowError(ex);
            }

            SetDisconnectedStatus();
#else
            client.DisconnectAsync();
#endif
        }

        void SetDisconnectedStatus()
        {
            SetStatusText("Ready");
            EnableDialog(true);
            _disconnect = false;
            _disconnected = true;
        }

#if !Framework4_5
        /// <summary>
        /// Handles the client's DisconnectCompleted event.
        /// </summary>
        /// <param name="sender">The client object.</param>
        /// <param name="e">The event arguments.</param>
        private void smtpClient_DisconnectCompleted(object sender, ExtendedAsyncCompletedEventArgs<string> e)
        {
            if (e.Error != null)
            {
                Util.ShowError(e.Error);
            }

            SetDisconnectedStatus();
        }
#endif

        /// <summary>
        /// Handles the client's Progress event.
        /// </summary>
        /// <param name="sender">The SMTP client object.</param>
        /// <param name="e">The event arguments.</param>
        private void smtpClient_Progress(object sender, SmtpProgressEventArgs e)
        {
            if (e.State == MailClientTransferState.Sending)
            {
                SetStatusText("Sending message... {0} bytes sent, {1}% completed", e.BytesTransferred, (int)e.Percentage);
                toolStripProgressBar.Value = (int) e.Percentage;
            }
        }

        #region Certificate

        /// <summary>
        /// Returns all issues of the given certificate.
        /// </summary>
        private static string GetCertProblem(CertificateVerificationStatus status, int code, ref bool showAddTrusted)
        {
            switch (status)
            {
                case CertificateVerificationStatus.TimeNotValid:
                    return "Server's certificate has expired or is not valid yet.";

                case CertificateVerificationStatus.Revoked:
                    return "Server's certificate has been revoked.";

                case CertificateVerificationStatus.UnknownCA:
                    return "Server's certificate was issued by an unknown authority.";

                case CertificateVerificationStatus.RootNotTrusted:
                    showAddTrusted = true;
                    return "Server's certificate was issued by an untrusted authority.";

                case CertificateVerificationStatus.IncompleteChain:
                    return "Server's certificate does not chain up to a trusted root authority.";

                case CertificateVerificationStatus.Malformed:
                    return "Server's certificate is malformed.";

                case CertificateVerificationStatus.CNNotMatch:
                    return "Server hostname does not match the certificate.";

                case CertificateVerificationStatus.UnknownError:
                    return string.Format("Error {0:x} encountered while validating server's certificate.", code);

                default:
                    return status.ToString();
            }
        }

        void client_CertificateReceived(object sender, ComponentPro.Security.CertificateReceivedEventArgs e)
        {
            CertValidator dlg = new CertValidator();

            CertificateVerificationStatus status = e.Status;

            CertificateVerificationStatus[] values = (CertificateVerificationStatus[])Enum.GetValues(typeof(CertificateVerificationStatus));

            StringBuilder sbIssues = new StringBuilder();
            bool showAddTrusted = false;
            for (int i = 0; i < values.Length; i++)
            {
                // Matches the validation status?
                if ((status & values[i]) == 0)
                    continue;

                // The issue is processed.
                status ^= values[i];

                sbIssues.AppendFormat("{0}\r\n", GetCertProblem(values[i], e.ErrorCode, ref showAddTrusted));
            }

            dlg.Certificate = e.ServerCertificates[0];
            dlg.Issues = sbIssues.ToString();
            dlg.ShowAddToTrustedList = showAddTrusted;

            dlg.ShowDialog();

            e.AddToTrustedRoot = dlg.AddToTrustedList;
            e.Accept = dlg.Accepted;
        }

        private void client_CertificateRequired(object sender, ComponentPro.Security.CertificateRequiredEventArgs e)
        {
            // If the client cert file is specified.
            if (!string.IsNullOrEmpty(_cert))
            {
                // Load Certificate.
                PasswordPrompt passdlg = new PasswordPrompt();
                // Ask for cert's passpharse
                if (passdlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    X509Certificate2 clientCert = new X509Certificate2(_cert, passdlg.Password);
                    e.Certificates = new X509Certificate2Collection(clientCert);
                    return;
                }

                // Password has not been provided.
            }
            CertProvider dlg = new CertProvider();
            dlg.ShowDialog();
            // Get the selected certificate.
            e.Certificates = new X509Certificate2Collection(dlg.SelectedCertificate);
        }

        #endregion
    }
}