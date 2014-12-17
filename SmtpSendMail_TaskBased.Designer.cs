using System.Drawing;
using System.Windows.Forms;

namespace SmtpSendMail
{
    partial class SmtpSendMail
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SmtpSendMail));
            ComponentPro.Net.Mail.SmtpConfig smtpConfig1 = new ComponentPro.Net.Mail.SmtpConfig();
            this.btnAddAttachment = new System.Windows.Forms.Button();
            this.txtSubject = new System.Windows.Forms.TextBox();
            this.lblSubject = new System.Windows.Forms.Label();
            this.stsMain = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.tssStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.txtBody = new System.Windows.Forms.TextBox();
            this.cbxAttachment = new System.Windows.Forms.ComboBox();
            this.txtBcc = new System.Windows.Forms.TextBox();
            this.lblBcc = new System.Windows.Forms.Label();
            this.txtCc = new System.Windows.Forms.TextBox();
            this.lblCc = new System.Windows.Forms.Label();
            this.lblAttachment = new System.Windows.Forms.Label();
            this.txtTo = new System.Windows.Forms.TextBox();
            this.lblTo = new System.Windows.Forms.Label();
            this.txtFrom = new System.Windows.Forms.TextBox();
            this.lblFrom = new System.Windows.Forms.Label();
            this.btnRemoveAttachment = new System.Windows.Forms.Button();
            this.tsbSend = new System.Windows.Forms.ToolStripButton();
            this.tsbCancel = new System.Windows.Forms.ToolStripButton();
            this.tsbSettings = new System.Windows.Forms.ToolStripButton();
            this.fileButton = new System.Windows.Forms.Button();
            this.newMessageButton = new System.Windows.Forms.ToolStripButton();
            this.openMessageButton = new System.Windows.Forms.ToolStripButton();
            this.saveButton = new System.Windows.Forms.ToolStripButton();
            this.saveAsButton = new System.Windows.Forms.ToolStripButton();
            this.saveAttachmentsButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.cutButton = new System.Windows.Forms.ToolStripButton();
            this.copyButton = new System.Windows.Forms.ToolStripButton();
            this.pasteButton = new System.Windows.Forms.ToolStripButton();
            this.selectAllButton = new System.Windows.Forms.ToolStripButton();
            this.client = new ComponentPro.Net.Mail.Smtp(this.components);
            this.toolbarMain = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.stsMain.SuspendLayout();
            this.toolbarMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAddAttachment
            // 
            this.btnAddAttachment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddAttachment.Location = new System.Drawing.Point(529, 179);
            this.btnAddAttachment.Name = "btnAddAttachment";
            this.btnAddAttachment.Size = new System.Drawing.Size(56, 20);
            this.btnAddAttachment.TabIndex = 7;
            this.btnAddAttachment.Text = "Add";
            this.btnAddAttachment.Click += new System.EventHandler(this.btnAddAttachment_Click);
            // 
            // txtSubject
            // 
            this.txtSubject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSubject.Location = new System.Drawing.Point(78, 155);
            this.txtSubject.Name = "txtSubject";
            this.txtSubject.Size = new System.Drawing.Size(566, 20);
            this.txtSubject.TabIndex = 5;
            this.txtSubject.TextChanged += new System.EventHandler(this.txtSubject_TextChanged);
            // 
            // lblSubject
            // 
            this.lblSubject.Location = new System.Drawing.Point(10, 159);
            this.lblSubject.Name = "lblSubject";
            this.lblSubject.Size = new System.Drawing.Size(62, 14);
            this.lblSubject.TabIndex = 125;
            this.lblSubject.Text = "Subject:";
            // 
            // stsMain
            // 
            this.stsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar,
            this.tssStatus});
            this.stsMain.Location = new System.Drawing.Point(0, 514);
            this.stsMain.Name = "stsMain";
            this.stsMain.Size = new System.Drawing.Size(653, 22);
            this.stsMain.TabIndex = 123;
            // 
            // toolStripProgressBar
            // 
            this.toolStripProgressBar.Name = "toolStripProgressBar";
            this.toolStripProgressBar.Size = new System.Drawing.Size(100, 16);
            this.toolStripProgressBar.Visible = false;
            // 
            // tssStatus
            // 
            this.tssStatus.Name = "tssStatus";
            this.tssStatus.Size = new System.Drawing.Size(38, 17);
            this.tssStatus.Text = "Ready";
            // 
            // txtBody
            // 
            this.txtBody.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBody.Location = new System.Drawing.Point(9, 204);
            this.txtBody.Multiline = true;
            this.txtBody.Name = "txtBody";
            this.txtBody.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtBody.Size = new System.Drawing.Size(635, 304);
            this.txtBody.TabIndex = 9;
            // 
            // cbxAttachment
            // 
            this.cbxAttachment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxAttachment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxAttachment.FormattingEnabled = true;
            this.cbxAttachment.Location = new System.Drawing.Point(78, 179);
            this.cbxAttachment.Name = "cbxAttachment";
            this.cbxAttachment.Size = new System.Drawing.Size(447, 21);
            this.cbxAttachment.TabIndex = 6;
            // 
            // txtBcc
            // 
            this.txtBcc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBcc.Location = new System.Drawing.Point(78, 131);
            this.txtBcc.Name = "txtBcc";
            this.txtBcc.Size = new System.Drawing.Size(566, 20);
            this.txtBcc.TabIndex = 4;
            // 
            // lblBcc
            // 
            this.lblBcc.Location = new System.Drawing.Point(10, 135);
            this.lblBcc.Name = "lblBcc";
            this.lblBcc.Size = new System.Drawing.Size(62, 14);
            this.lblBcc.TabIndex = 119;
            this.lblBcc.Text = "Bcc:";
            // 
            // txtCc
            // 
            this.txtCc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCc.Location = new System.Drawing.Point(78, 107);
            this.txtCc.Name = "txtCc";
            this.txtCc.Size = new System.Drawing.Size(566, 20);
            this.txtCc.TabIndex = 3;
            // 
            // lblCc
            // 
            this.lblCc.Location = new System.Drawing.Point(10, 111);
            this.lblCc.Name = "lblCc";
            this.lblCc.Size = new System.Drawing.Size(62, 14);
            this.lblCc.TabIndex = 117;
            this.lblCc.Text = "Cc:";
            // 
            // lblAttachment
            // 
            this.lblAttachment.Location = new System.Drawing.Point(10, 183);
            this.lblAttachment.Name = "lblAttachment";
            this.lblAttachment.Size = new System.Drawing.Size(64, 14);
            this.lblAttachment.TabIndex = 107;
            this.lblAttachment.Text = "Attachment:";
            // 
            // txtTo
            // 
            this.txtTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTo.Location = new System.Drawing.Point(78, 83);
            this.txtTo.Name = "txtTo";
            this.txtTo.Size = new System.Drawing.Size(566, 20);
            this.txtTo.TabIndex = 2;
            // 
            // lblTo
            // 
            this.lblTo.Location = new System.Drawing.Point(10, 86);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(62, 14);
            this.lblTo.TabIndex = 103;
            this.lblTo.Text = "To:";
            // 
            // txtFrom
            // 
            this.txtFrom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFrom.Location = new System.Drawing.Point(78, 59);
            this.txtFrom.Name = "txtFrom";
            this.txtFrom.Size = new System.Drawing.Size(566, 20);
            this.txtFrom.TabIndex = 1;
            // 
            // lblFrom
            // 
            this.lblFrom.Location = new System.Drawing.Point(10, 63);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new System.Drawing.Size(64, 14);
            this.lblFrom.TabIndex = 102;
            this.lblFrom.Text = "From:";
            // 
            // btnRemoveAttachment
            // 
            this.btnRemoveAttachment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveAttachment.Enabled = false;
            this.btnRemoveAttachment.Location = new System.Drawing.Point(587, 179);
            this.btnRemoveAttachment.Name = "btnRemoveAttachment";
            this.btnRemoveAttachment.Size = new System.Drawing.Size(56, 20);
            this.btnRemoveAttachment.TabIndex = 8;
            this.btnRemoveAttachment.Text = "Remove";
            this.btnRemoveAttachment.Click += new System.EventHandler(this.btnRemoveAttachment_Click);
            // 
            // tsbSend
            // 
            this.tsbSend.AutoSize = false;
            this.tsbSend.Image = ((System.Drawing.Image)(resources.GetObject("tsbSend.Image")));
            this.tsbSend.Name = "tsbSend";
            this.tsbSend.Size = new System.Drawing.Size(49, 49);
            this.tsbSend.Text = "Send";
            this.tsbSend.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbSend.Click += new System.EventHandler(this.tsbSend_Click);
            // 
            // tsbCancel
            // 
            this.tsbCancel.AutoSize = false;
            this.tsbCancel.Enabled = false;
            this.tsbCancel.Image = ((System.Drawing.Image)(resources.GetObject("tsbCancel.Image")));
            this.tsbCancel.Name = "tsbCancel";
            this.tsbCancel.Size = new System.Drawing.Size(49, 49);
            this.tsbCancel.Text = "Cancel";
            this.tsbCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbCancel.Click += new System.EventHandler(this.tsbCancel_Click);
            // 
            // tsbSettings
            // 
            this.tsbSettings.AutoSize = false;
            this.tsbSettings.Image = ((System.Drawing.Image)(resources.GetObject("tsbSettings.Image")));
            this.tsbSettings.Name = "tsbSettings";
            this.tsbSettings.Size = new System.Drawing.Size(49, 49);
            this.tsbSettings.Text = "Settings";
            this.tsbSettings.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbSettings.Click += new System.EventHandler(this.tsbSettings_Click);
            // 
            // fileButton
            // 
            this.fileButton.Location = new System.Drawing.Point(0, 0);
            this.fileButton.Name = "fileButton";
            this.fileButton.Size = new System.Drawing.Size(0, 0);
            this.fileButton.TabIndex = 0;
            // 
            // newMessageButton
            // 
            this.newMessageButton.AutoSize = false;
            this.newMessageButton.Image = ((System.Drawing.Image)(resources.GetObject("newMessageButton.Image")));
            this.newMessageButton.Name = "newMessageButton";
            this.newMessageButton.Size = new System.Drawing.Size(49, 49);
            this.newMessageButton.Text = "New";
            this.newMessageButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.newMessageButton.Click += new System.EventHandler(this.newMessageButton_Click);
            // 
            // openMessageButton
            // 
            this.openMessageButton.AutoSize = false;
            this.openMessageButton.Image = ((System.Drawing.Image)(resources.GetObject("openMessageButton.Image")));
            this.openMessageButton.Name = "openMessageButton";
            this.openMessageButton.Size = new System.Drawing.Size(49, 49);
            this.openMessageButton.Text = "Open";
            this.openMessageButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.openMessageButton.Click += new System.EventHandler(this.openMessageButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.AutoSize = false;
            this.saveButton.Image = ((System.Drawing.Image)(resources.GetObject("saveButton.Image")));
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(49, 49);
            this.saveButton.Text = "Save";
            this.saveButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // saveAsButton
            // 
            this.saveAsButton.AutoSize = false;
            this.saveAsButton.Image = ((System.Drawing.Image)(resources.GetObject("saveAsButton.Image")));
            this.saveAsButton.Name = "saveAsButton";
            this.saveAsButton.Size = new System.Drawing.Size(49, 49);
            this.saveAsButton.Text = "Save as";
            this.saveAsButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.saveAsButton.Click += new System.EventHandler(this.saveAsButton_Click);
            // 
            // saveAttachmentsButton
            // 
            this.saveAttachmentsButton.AutoSize = false;
            this.saveAttachmentsButton.Image = ((System.Drawing.Image)(resources.GetObject("saveAttachmentsButton.Image")));
            this.saveAttachmentsButton.Name = "saveAttachmentsButton";
            this.saveAttachmentsButton.Size = new System.Drawing.Size(69, 49);
            this.saveAttachmentsButton.Text = "Attachments";
            this.saveAttachmentsButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.saveAttachmentsButton.ToolTipText = "Save Attachments As";
            this.saveAttachmentsButton.Click += new System.EventHandler(this.saveAttachmentsButton_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(251, 6);
            // 
            // cutButton
            // 
            this.cutButton.AutoSize = false;
            this.cutButton.Image = ((System.Drawing.Image)(resources.GetObject("cutButton.Image")));
            this.cutButton.Name = "cutButton";
            this.cutButton.Size = new System.Drawing.Size(49, 49);
            this.cutButton.Text = "Cut";
            this.cutButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.cutButton.Click += new System.EventHandler(this.cutButton_Click);
            // 
            // copyButton
            // 
            this.copyButton.AutoSize = false;
            this.copyButton.Image = ((System.Drawing.Image)(resources.GetObject("copyButton.Image")));
            this.copyButton.Name = "copyButton";
            this.copyButton.Size = new System.Drawing.Size(49, 49);
            this.copyButton.Text = "Copy";
            this.copyButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.copyButton.Click += new System.EventHandler(this.copyButton_Click);
            // 
            // pasteButton
            // 
            this.pasteButton.AutoSize = false;
            this.pasteButton.Image = ((System.Drawing.Image)(resources.GetObject("pasteButton.Image")));
            this.pasteButton.Name = "pasteButton";
            this.pasteButton.Size = new System.Drawing.Size(49, 49);
            this.pasteButton.Text = "Paste";
            this.pasteButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.pasteButton.Click += new System.EventHandler(this.pasteButton_Click);
            // 
            // selectAllButton
            // 
            this.selectAllButton.AutoSize = false;
            this.selectAllButton.Image = ((System.Drawing.Image)(resources.GetObject("selectAllButton.Image")));
            this.selectAllButton.Name = "selectAllButton";
            this.selectAllButton.Size = new System.Drawing.Size(49, 49);
            this.selectAllButton.Text = "Select All";
            this.selectAllButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.selectAllButton.Click += new System.EventHandler(this.selectAllButton_Click);
            // 
            // client
            // 
            this.client.Config = smtpConfig1;
            this.client.Progress += new ComponentPro.Net.Mail.SmtpProgressEventHandler(this.smtpClient_Progress);
            // 
            // toolbarMain
            // 
            this.toolbarMain.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolbarMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newMessageButton,
            this.openMessageButton,
            this.saveButton,
            this.saveAsButton,
            this.saveAttachmentsButton,
            this.toolStripSeparator1,
            this.tsbSend,
            this.tsbCancel,
            this.tsbSettings,
            this.toolStripSeparator2,
            this.pasteButton,
            this.cutButton,
            this.copyButton,
            this.selectAllButton});
            this.toolbarMain.Location = new System.Drawing.Point(0, 0);
            this.toolbarMain.Name = "toolbarMain";
            this.toolbarMain.Size = new System.Drawing.Size(653, 52);
            this.toolbarMain.TabIndex = 126;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 52);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 52);
            // 
            // SmtpSendMail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(653, 536);
            this.Controls.Add(this.txtSubject);
            this.Controls.Add(this.lblSubject);
            this.Controls.Add(this.stsMain);
            this.Controls.Add(this.txtBody);
            this.Controls.Add(this.cbxAttachment);
            this.Controls.Add(this.txtBcc);
            this.Controls.Add(this.lblBcc);
            this.Controls.Add(this.txtCc);
            this.Controls.Add(this.lblCc);
            this.Controls.Add(this.btnAddAttachment);
            this.Controls.Add(this.lblAttachment);
            this.Controls.Add(this.txtTo);
            this.Controls.Add(this.lblTo);
            this.Controls.Add(this.txtFrom);
            this.Controls.Add(this.lblFrom);
            this.Controls.Add(this.btnRemoveAttachment);
            this.Controls.Add(this.toolbarMain);
            this.MinimumSize = new System.Drawing.Size(357, 289);
            this.Name = "SmtpSendMail";
            this.Text = "Ultimate Mail Composer and Sender";
            this.stsMain.ResumeLayout(false);
            this.stsMain.PerformLayout();
            this.toolbarMain.ResumeLayout(false);
            this.toolbarMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAddAttachment;
        private System.Windows.Forms.Label lblAttachment;
        private System.Windows.Forms.TextBox txtTo;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.TextBox txtFrom;
        private System.Windows.Forms.Label lblFrom;
        private System.Windows.Forms.Button btnRemoveAttachment;
        private System.Windows.Forms.ToolStripButton tsbSend;
        private System.Windows.Forms.ToolStripButton tsbSettings;
        private System.Windows.Forms.TextBox txtCc;
        private System.Windows.Forms.Label lblCc;
        private System.Windows.Forms.TextBox txtBcc;
        private System.Windows.Forms.Label lblBcc;
        private System.Windows.Forms.ComboBox cbxAttachment;
        private System.Windows.Forms.TextBox txtBody;
        private System.Windows.Forms.StatusStrip stsMain;
        private System.Windows.Forms.ToolStripStatusLabel tssStatus;
        private System.Windows.Forms.TextBox txtSubject;
        private System.Windows.Forms.Label lblSubject;
        private System.Windows.Forms.Button fileButton;
        private System.Windows.Forms.ToolStripButton newMessageButton;
        private System.Windows.Forms.ToolStripButton openMessageButton;
        private System.Windows.Forms.ToolStripButton saveButton;
        private System.Windows.Forms.ToolStripButton saveAsButton;
        private System.Windows.Forms.ToolStripButton saveAttachmentsButton;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripButton cutButton;
        private System.Windows.Forms.ToolStripButton copyButton;
        private System.Windows.Forms.ToolStripButton pasteButton;
        private System.Windows.Forms.ToolStripButton selectAllButton;
        private ComponentPro.Net.Mail.Smtp client;
        private System.Windows.Forms.ToolStripButton tsbCancel;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar;
        private System.Windows.Forms.ToolStrip toolbarMain;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripSeparator toolStripSeparator2;
    }
}

