namespace SlippiGUI
{
    partial class Form1
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
            this.clipLengthTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.exportButton = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.exportLabel = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.importLabel = new System.Windows.Forms.Label();
            this.importButton = new System.Windows.Forms.Button();
            this.searchTextBox = new System.Windows.Forms.TextBox();
            this.searchLabel = new System.Windows.Forms.Label();
            this.refreshButton = new System.Windows.Forms.Button();
            this.exportMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.thanksLabel = new System.Windows.Forms.Label();
            this.patreonLabel = new System.Windows.Forms.Label();
            this.aitchPatreonLink = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.numDisplayedFilesLabel = new System.Windows.Forms.Label();
            this.namingStyleBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.openImportButton = new System.Windows.Forms.Button();
            this.openExportButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.saveStateButtonComboBox = new System.Windows.Forms.ComboBox();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // clipLengthTextBox
            // 
            this.clipLengthTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clipLengthTextBox.Location = new System.Drawing.Point(1090, 90);
            this.clipLengthTextBox.Name = "clipLengthTextBox";
            this.clipLengthTextBox.Size = new System.Drawing.Size(100, 26);
            this.clipLengthTextBox.TabIndex = 8;
            this.clipLengthTextBox.TextChanged += new System.EventHandler(this.clipLengthTextBox_TextChanged);
            this.clipLengthTextBox.Leave += new System.EventHandler(this.clipLengthTextBox_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(995, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 20);
            this.label2.TabIndex = 9;
            this.label2.Text = "Clip Length";
            // 
            // exportButton
            // 
            this.exportButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.exportButton.Location = new System.Drawing.Point(607, 42);
            this.exportButton.Name = "exportButton";
            this.exportButton.Size = new System.Drawing.Size(172, 36);
            this.exportButton.TabIndex = 10;
            this.exportButton.Text = "Choose Export Folder";
            this.exportButton.UseVisualStyleBackColor = true;
            this.exportButton.Click += new System.EventHandler(this.exportButton_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridView1.Location = new System.Drawing.Point(12, 208);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1310, 518);
            this.dataGridView1.TabIndex = 17;
            this.dataGridView1.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dataGridView1_CellBeginEdit);
            this.dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            this.dataGridView1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dataGridView1_MouseDown);
            // 
            // exportLabel
            // 
            this.exportLabel.AutoSize = true;
            this.exportLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exportLabel.Location = new System.Drawing.Point(603, 11);
            this.exportLabel.Name = "exportLabel";
            this.exportLabel.Size = new System.Drawing.Size(109, 24);
            this.exportLabel.TabIndex = 20;
            this.exportLabel.Text = "exportLabel";
            // 
            // importLabel
            // 
            this.importLabel.AutoSize = true;
            this.importLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.importLabel.Location = new System.Drawing.Point(286, 11);
            this.importLabel.Name = "importLabel";
            this.importLabel.Size = new System.Drawing.Size(108, 24);
            this.importLabel.TabIndex = 23;
            this.importLabel.Text = "importLabel";
            // 
            // importButton
            // 
            this.importButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.importButton.Location = new System.Drawing.Point(290, 42);
            this.importButton.Name = "importButton";
            this.importButton.Size = new System.Drawing.Size(172, 36);
            this.importButton.TabIndex = 22;
            this.importButton.Text = "Choose Replay Folder";
            this.importButton.UseVisualStyleBackColor = true;
            this.importButton.Click += new System.EventHandler(this.importButton_Click);
            // 
            // searchTextBox
            // 
            this.searchTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchTextBox.Location = new System.Drawing.Point(922, 170);
            this.searchTextBox.Name = "searchTextBox";
            this.searchTextBox.Size = new System.Drawing.Size(182, 26);
            this.searchTextBox.TabIndex = 24;
            this.searchTextBox.TextChanged += new System.EventHandler(this.searchTextBox_TextChanged);
            // 
            // searchLabel
            // 
            this.searchLabel.AutoSize = true;
            this.searchLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchLabel.Location = new System.Drawing.Point(751, 173);
            this.searchLabel.Name = "searchLabel";
            this.searchLabel.Size = new System.Drawing.Size(165, 20);
            this.searchLabel.TabIndex = 25;
            this.searchLabel.Text = "Search (by File Name)";
            // 
            // refreshButton
            // 
            this.refreshButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.refreshButton.Location = new System.Drawing.Point(1140, 171);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(182, 25);
            this.refreshButton.TabIndex = 26;
            this.refreshButton.Text = "Refresh";
            this.refreshButton.UseVisualStyleBackColor = true;
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
            // 
            // exportMenuStrip
            // 
            this.exportMenuStrip.Font = new System.Drawing.Font("Segoe UI", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exportMenuStrip.Name = "exportMenuStrip";
            this.exportMenuStrip.Size = new System.Drawing.Size(61, 4);
            this.exportMenuStrip.Click += new System.EventHandler(this.exportMenuStrip_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(28, 13);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(228, 65);
            this.button1.TabIndex = 27;
            this.button1.Text = "How to Use";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // thanksLabel
            // 
            this.thanksLabel.AutoSize = true;
            this.thanksLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.thanksLabel.Location = new System.Drawing.Point(14, 802);
            this.thanksLabel.Name = "thanksLabel";
            this.thanksLabel.Size = new System.Drawing.Size(1156, 26);
            this.thanksLabel.TabIndex = 28;
            this.thanksLabel.Text = "Special Thanks: Only possible thanks to Aitch for his hard work on the Slippi -> " +
    "TM-CE backend and RWing! Check out";
            // 
            // patreonLabel
            // 
            this.patreonLabel.AutoSize = true;
            this.patreonLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.patreonLabel.Location = new System.Drawing.Point(13, 758);
            this.patreonLabel.Name = "patreonLabel";
            this.patreonLabel.Size = new System.Drawing.Size(357, 26);
            this.patreonLabel.TabIndex = 29;
            this.patreonLabel.Text = "Consider supporting me (Fiction) on";
            // 
            // aitchPatreonLink
            // 
            this.aitchPatreonLink.AutoSize = true;
            this.aitchPatreonLink.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.aitchPatreonLink.Location = new System.Drawing.Point(1166, 803);
            this.aitchPatreonLink.Name = "aitchPatreonLink";
            this.aitchPatreonLink.Size = new System.Drawing.Size(156, 25);
            this.aitchPatreonLink.TabIndex = 30;
            this.aitchPatreonLink.TabStop = true;
            this.aitchPatreonLink.Text = "Aitch\'s Patreon";
            this.aitchPatreonLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.aitchPatreonLink_LinkClicked);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.label1.Location = new System.Drawing.Point(445, 758);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(459, 26);
            this.label1.TabIndex = 31;
            this.label1.Text = "if you want to help support my coding projects!";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel1.Location = new System.Drawing.Point(363, 759);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(87, 25);
            this.linkLabel1.TabIndex = 32;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Patreon";
            // 
            // numDisplayedFilesLabel
            // 
            this.numDisplayedFilesLabel.AutoSize = true;
            this.numDisplayedFilesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numDisplayedFilesLabel.Location = new System.Drawing.Point(12, 174);
            this.numDisplayedFilesLabel.Name = "numDisplayedFilesLabel";
            this.numDisplayedFilesLabel.Size = new System.Drawing.Size(19, 20);
            this.numDisplayedFilesLabel.TabIndex = 33;
            this.numDisplayedFilesLabel.Text = "--";
            // 
            // namingStyleBox
            // 
            this.namingStyleBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.namingStyleBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.namingStyleBox.FormattingEnabled = true;
            this.namingStyleBox.Location = new System.Drawing.Point(1090, 50);
            this.namingStyleBox.Name = "namingStyleBox";
            this.namingStyleBox.Size = new System.Drawing.Size(232, 28);
            this.namingStyleBox.TabIndex = 34;
            this.namingStyleBox.SelectedIndexChanged += new System.EventHandler(this.frameComboBox_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(982, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 20);
            this.label3.TabIndex = 35;
            this.label3.Text = "Naming Style";
            // 
            // openImportButton
            // 
            this.openImportButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.openImportButton.Location = new System.Drawing.Point(290, 86);
            this.openImportButton.Name = "openImportButton";
            this.openImportButton.Size = new System.Drawing.Size(172, 36);
            this.openImportButton.TabIndex = 36;
            this.openImportButton.Text = "Open Replay Folder";
            this.openImportButton.UseVisualStyleBackColor = true;
            this.openImportButton.Click += new System.EventHandler(this.openImportButton_Click);
            // 
            // openExportButton
            // 
            this.openExportButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.openExportButton.Location = new System.Drawing.Point(607, 86);
            this.openExportButton.Name = "openExportButton";
            this.openExportButton.Size = new System.Drawing.Size(172, 36);
            this.openExportButton.TabIndex = 37;
            this.openExportButton.Text = "Open Export Folder";
            this.openExportButton.UseVisualStyleBackColor = true;
            this.openExportButton.Click += new System.EventHandler(this.openExportButton_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(944, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(140, 20);
            this.label4.TabIndex = 39;
            this.label4.Text = "Save State Button";
            // 
            // saveStateButtonComboBox
            // 
            this.saveStateButtonComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.saveStateButtonComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveStateButtonComboBox.FormattingEnabled = true;
            this.saveStateButtonComboBox.Location = new System.Drawing.Point(1090, 13);
            this.saveStateButtonComboBox.Name = "saveStateButtonComboBox";
            this.saveStateButtonComboBox.Size = new System.Drawing.Size(232, 28);
            this.saveStateButtonComboBox.TabIndex = 38;
            this.saveStateButtonComboBox.SelectedIndexChanged += new System.EventHandler(this.saveStateButtonComboBox_SelectedIndexChanged);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(28, 88);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(228, 34);
            this.button2.TabIndex = 40;
            this.button2.Text = "Report Bugs";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1346, 857);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.saveStateButtonComboBox);
            this.Controls.Add(this.openExportButton);
            this.Controls.Add(this.openImportButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.namingStyleBox);
            this.Controls.Add(this.numDisplayedFilesLabel);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.aitchPatreonLink);
            this.Controls.Add(this.patreonLabel);
            this.Controls.Add(this.thanksLabel);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.refreshButton);
            this.Controls.Add(this.searchLabel);
            this.Controls.Add(this.searchTextBox);
            this.Controls.Add(this.importLabel);
            this.Controls.Add(this.importButton);
            this.Controls.Add(this.exportLabel);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.exportButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.clipLengthTextBox);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Fiction\'s Melee Save State Clipper 0.2";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form1_DragEnter);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox clipLengthTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button exportButton;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label exportLabel;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label importLabel;
        private System.Windows.Forms.Button importButton;
        private System.Windows.Forms.TextBox searchTextBox;
        private System.Windows.Forms.Label searchLabel;
        private System.Windows.Forms.Button refreshButton;
        private System.Windows.Forms.ContextMenuStrip exportMenuStrip;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label thanksLabel;
        private System.Windows.Forms.Label patreonLabel;
        private System.Windows.Forms.LinkLabel aitchPatreonLink;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label numDisplayedFilesLabel;
        private System.Windows.Forms.ComboBox namingStyleBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button openImportButton;
        private System.Windows.Forms.Button openExportButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox saveStateButtonComboBox;
        private System.Windows.Forms.Button button2;
    }
}

