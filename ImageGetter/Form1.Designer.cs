namespace ImageGetter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tableUI = new System.Windows.Forms.DataGridView();
            this.inLoad = new System.Windows.Forms.Button();
            this.clearTable = new System.Windows.Forms.Button();
            this.downloadImages = new System.Windows.Forms.Button();
            this.msgBox = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.tableUI)).BeginInit();
            this.SuspendLayout();
            // 
            // tableUI
            // 
            this.tableUI.AllowDrop = true;
            this.tableUI.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tableUI.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.tableUI.Location = new System.Drawing.Point(123, 85);
            this.tableUI.Name = "tableUI";
            this.tableUI.RowHeadersWidth = 51;
            this.tableUI.Size = new System.Drawing.Size(776, 277);
            this.tableUI.TabIndex = 0;
            // 
            // inLoad
            // 
            this.inLoad.Location = new System.Drawing.Point(133, 13);
            this.inLoad.Name = "inLoad";
            this.inLoad.Size = new System.Drawing.Size(132, 49);
            this.inLoad.TabIndex = 1;
            this.inLoad.Text = "Adatok betöltése fájlból (csv)";
            this.inLoad.UseVisualStyleBackColor = true;
            this.inLoad.Click += new System.EventHandler(this.inLoad_Click);
            // 
            // clearTable
            // 
            this.clearTable.Location = new System.Drawing.Point(290, 13);
            this.clearTable.Name = "clearTable";
            this.clearTable.Size = new System.Drawing.Size(132, 49);
            this.clearTable.TabIndex = 2;
            this.clearTable.Text = "Tábla Ürítése";
            this.clearTable.UseVisualStyleBackColor = true;
            this.clearTable.Click += new System.EventHandler(this.clearTable_Click);
            // 
            // downloadImages
            // 
            this.downloadImages.Location = new System.Drawing.Point(767, 13);
            this.downloadImages.Name = "downloadImages";
            this.downloadImages.Size = new System.Drawing.Size(132, 49);
            this.downloadImages.TabIndex = 3;
            this.downloadImages.Text = "Letöltés";
            this.downloadImages.UseVisualStyleBackColor = true;
            this.downloadImages.Click += new System.EventHandler(this.downloadImages_Click);
            // 
            // msgBox
            // 
            this.msgBox.AccessibleRole = System.Windows.Forms.AccessibleRole.StaticText;
            this.msgBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.msgBox.ForeColor = System.Drawing.SystemColors.MenuText;
            this.msgBox.Location = new System.Drawing.Point(123, 570);
            this.msgBox.Name = "msgBox";
            this.msgBox.ReadOnly = true;
            this.msgBox.Size = new System.Drawing.Size(776, 138);
            this.msgBox.TabIndex = 4;
            this.msgBox.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::ImageGetter.Properties.Resources.FileShark_Background;
            this.ClientSize = new System.Drawing.Size(1061, 731);
            this.Controls.Add(this.msgBox);
            this.Controls.Add(this.downloadImages);
            this.Controls.Add(this.clearTable);
            this.Controls.Add(this.inLoad);
            this.Controls.Add(this.tableUI);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "A Cápa";
            ((System.ComponentModel.ISupportInitialize)(this.tableUI)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView tableUI;
        private System.Windows.Forms.Button inLoad;
        private System.Windows.Forms.Button clearTable;
        private System.Windows.Forms.Button downloadImages;
        private System.Windows.Forms.RichTextBox msgBox;
    }
}

