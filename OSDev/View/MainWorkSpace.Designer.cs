namespace OSDev.View
{
    partial class MainWorkSpace
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWorkSpace));
            this.textBoxCommand = new System.Windows.Forms.TextBox();
            this.richTextBoxCommandResult = new System.Windows.Forms.RichTextBox();
            this.listBoxDirectoryContent = new System.Windows.Forms.ListBox();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuItemOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemRename = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemCreate = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuItemAddDirectory = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemAddFile = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonBack = new System.Windows.Forms.Button();
            this.buttonExit = new System.Windows.Forms.Button();
            this.buttonChangeUser = new System.Windows.Forms.Button();
            this.buttonComplete = new System.Windows.Forms.Button();
            this.labelCurrentDirectory = new System.Windows.Forms.Label();
            this.contextMenuStrip.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxCommand
            // 
            this.textBoxCommand.Location = new System.Drawing.Point(12, 540);
            this.textBoxCommand.Name = "textBoxCommand";
            this.textBoxCommand.Size = new System.Drawing.Size(801, 20);
            this.textBoxCommand.TabIndex = 0;
            // 
            // richTextBoxCommandResult
            // 
            this.richTextBoxCommandResult.Location = new System.Drawing.Point(12, 414);
            this.richTextBoxCommandResult.Name = "richTextBoxCommandResult";
            this.richTextBoxCommandResult.ReadOnly = true;
            this.richTextBoxCommandResult.Size = new System.Drawing.Size(934, 117);
            this.richTextBoxCommandResult.TabIndex = 1;
            this.richTextBoxCommandResult.Text = "";
            // 
            // listBoxDirectoryContent
            // 
            this.listBoxDirectoryContent.BackColor = System.Drawing.Color.AliceBlue;
            this.listBoxDirectoryContent.ContextMenuStrip = this.contextMenuStrip;
            this.listBoxDirectoryContent.FormattingEnabled = true;
            this.listBoxDirectoryContent.Location = new System.Drawing.Point(12, 66);
            this.listBoxDirectoryContent.Name = "listBoxDirectoryContent";
            this.listBoxDirectoryContent.Size = new System.Drawing.Size(934, 342);
            this.listBoxDirectoryContent.TabIndex = 2;
            this.listBoxDirectoryContent.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBoxDirectoryContent_MouseDoubleClick);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemOpen,
            this.menuItemDelete,
            this.menuItemRename,
            this.toolStripSeparator1,
            this.menuItemCreate});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(160, 98);
            // 
            // menuItemOpen
            // 
            this.menuItemOpen.Name = "menuItemOpen";
            this.menuItemOpen.Size = new System.Drawing.Size(159, 22);
            this.menuItemOpen.Text = "открыть";
            this.menuItemOpen.Click += new System.EventHandler(this.menuItemOpen_Click);
            // 
            // menuItemDelete
            // 
            this.menuItemDelete.Name = "menuItemDelete";
            this.menuItemDelete.Size = new System.Drawing.Size(159, 22);
            this.menuItemDelete.Text = "удалить";
            this.menuItemDelete.Click += new System.EventHandler(this.menuItemDelete_Click);
            // 
            // menuItemRename
            // 
            this.menuItemRename.Name = "menuItemRename";
            this.menuItemRename.Size = new System.Drawing.Size(159, 22);
            this.menuItemRename.Text = "переименовать";
            this.menuItemRename.Click += new System.EventHandler(this.menuItemRename_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(156, 6);
            // 
            // menuItemCreate
            // 
            this.menuItemCreate.DropDown = this.contextMenuStrip1;
            this.menuItemCreate.Name = "menuItemCreate";
            this.menuItemCreate.Size = new System.Drawing.Size(159, 22);
            this.menuItemCreate.Text = "создать";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemAddDirectory,
            this.menuItemAddFile});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.OwnerItem = this.menuItemCreate;
            this.contextMenuStrip1.Size = new System.Drawing.Size(107, 48);
            // 
            // menuItemAddDirectory
            // 
            this.menuItemAddDirectory.Name = "menuItemAddDirectory";
            this.menuItemAddDirectory.Size = new System.Drawing.Size(106, 22);
            this.menuItemAddDirectory.Text = "папку";
            this.menuItemAddDirectory.Click += new System.EventHandler(this.menuItemAddDirectory_Click);
            // 
            // menuItemAddFile
            // 
            this.menuItemAddFile.Name = "menuItemAddFile";
            this.menuItemAddFile.Size = new System.Drawing.Size(106, 22);
            this.menuItemAddFile.Text = "файл";
            this.menuItemAddFile.Click += new System.EventHandler(this.menuItemAddFile_Click);
            // 
            // buttonBack
            // 
            this.buttonBack.BackColor = System.Drawing.Color.DarkTurquoise;
            this.buttonBack.Location = new System.Drawing.Point(12, 11);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(47, 23);
            this.buttonBack.TabIndex = 3;
            this.buttonBack.Text = "Назад";
            this.buttonBack.UseVisualStyleBackColor = false;
            this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click);
            // 
            // buttonExit
            // 
            this.buttonExit.BackColor = System.Drawing.Color.DarkTurquoise;
            this.buttonExit.Location = new System.Drawing.Point(871, 12);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(75, 23);
            this.buttonExit.TabIndex = 4;
            this.buttonExit.Text = "Выход";
            this.buttonExit.UseVisualStyleBackColor = false;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // buttonChangeUser
            // 
            this.buttonChangeUser.BackColor = System.Drawing.Color.DarkTurquoise;
            this.buttonChangeUser.Location = new System.Drawing.Point(728, 12);
            this.buttonChangeUser.Name = "buttonChangeUser";
            this.buttonChangeUser.Size = new System.Drawing.Size(137, 23);
            this.buttonChangeUser.TabIndex = 5;
            this.buttonChangeUser.Text = "Сменить пользователя";
            this.buttonChangeUser.UseVisualStyleBackColor = false;
            this.buttonChangeUser.Click += new System.EventHandler(this.buttonChangeUser_Click);
            // 
            // buttonComplete
            // 
            this.buttonComplete.BackColor = System.Drawing.Color.DarkTurquoise;
            this.buttonComplete.Location = new System.Drawing.Point(819, 537);
            this.buttonComplete.Name = "buttonComplete";
            this.buttonComplete.Size = new System.Drawing.Size(127, 23);
            this.buttonComplete.TabIndex = 6;
            this.buttonComplete.Text = "Подтвердить";
            this.buttonComplete.UseVisualStyleBackColor = false;
            this.buttonComplete.Click += new System.EventHandler(this.buttonComplete_Click);
            // 
            // labelCurrentDirectory
            // 
            this.labelCurrentDirectory.AutoSize = true;
            this.labelCurrentDirectory.Location = new System.Drawing.Point(13, 47);
            this.labelCurrentDirectory.Name = "labelCurrentDirectory";
            this.labelCurrentDirectory.Size = new System.Drawing.Size(0, 13);
            this.labelCurrentDirectory.TabIndex = 7;
            // 
            // MainWorkSpace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(958, 572);
            this.Controls.Add(this.labelCurrentDirectory);
            this.Controls.Add(this.buttonComplete);
            this.Controls.Add(this.buttonChangeUser);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.buttonBack);
            this.Controls.Add(this.listBoxDirectoryContent);
            this.Controls.Add(this.richTextBoxCommandResult);
            this.Controls.Add(this.textBoxCommand);
            this.MaximumSize = new System.Drawing.Size(974, 611);
            this.MinimumSize = new System.Drawing.Size(974, 611);
            this.Name = "MainWorkSpace";
            this.Text = "DIMAS OS";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWorkSpace_FormClosing);
            this.Load += new System.EventHandler(this.MainWorkSpace_Load);
            this.contextMenuStrip.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxCommand;
        private System.Windows.Forms.RichTextBox richTextBoxCommandResult;
        private System.Windows.Forms.ListBox listBoxDirectoryContent;
        private System.Windows.Forms.Button buttonBack;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.Button buttonChangeUser;
        private System.Windows.Forms.Button buttonComplete;
        private System.Windows.Forms.Label labelCurrentDirectory;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem menuItemOpen;
        private System.Windows.Forms.ToolStripMenuItem menuItemDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem menuItemCreate;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuItemAddDirectory;
        private System.Windows.Forms.ToolStripMenuItem menuItemAddFile;
        private System.Windows.Forms.ToolStripMenuItem menuItemRename;
    }
}