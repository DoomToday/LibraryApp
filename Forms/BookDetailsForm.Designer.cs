namespace LibraryCatalog.Forms
{
    partial class BookDetailsForm
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
            txtTitle = new TextBox();
            txtAuthor = new TextBox();
            txtKeywords = new TextBox();
            labelName = new Label();
            label1 = new Label();
            label2 = new Label();
            btnSave = new Button();
            btnCancel = new Button();
            pbCoverPreview = new PictureBox();
            btnBrowseCover = new Button();
            btnClearCover = new Button();
            ((System.ComponentModel.ISupportInitialize)pbCoverPreview).BeginInit();
            SuspendLayout();
            // 
            // txtTitle
            // 
            txtTitle.Font = new Font("Sylfaen", 14F);
            txtTitle.Location = new Point(186, 49);
            txtTitle.Name = "txtTitle";
            txtTitle.Size = new Size(449, 44);
            txtTitle.TabIndex = 0;
            // 
            // txtAuthor
            // 
            txtAuthor.Font = new Font("Sylfaen", 14F);
            txtAuthor.Location = new Point(186, 118);
            txtAuthor.Name = "txtAuthor";
            txtAuthor.Size = new Size(449, 44);
            txtAuthor.TabIndex = 1;
            // 
            // txtKeywords
            // 
            txtKeywords.Font = new Font("Sylfaen", 14F);
            txtKeywords.Location = new Point(186, 193);
            txtKeywords.Name = "txtKeywords";
            txtKeywords.Size = new Size(449, 44);
            txtKeywords.TabIndex = 2;
            // 
            // labelName
            // 
            labelName.AutoSize = true;
            labelName.Font = new Font("Sylfaen", 14F);
            labelName.Location = new Point(97, 49);
            labelName.Name = "labelName";
            labelName.Size = new Size(83, 36);
            labelName.TabIndex = 3;
            labelName.Text = "Name";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Sylfaen", 14F);
            label1.Location = new Point(79, 118);
            label1.Name = "label1";
            label1.Size = new Size(101, 36);
            label1.TabIndex = 4;
            label1.Text = "Author";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Sylfaen", 14F);
            label2.Location = new Point(46, 193);
            label2.Name = "label2";
            label2.Size = new Size(134, 36);
            label2.TabIndex = 5;
            label2.Text = "Keywords";
            // 
            // btnSave
            // 
            btnSave.Font = new Font("Sylfaen", 14F);
            btnSave.Location = new Point(186, 274);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(194, 49);
            btnSave.TabIndex = 6;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Font = new Font("Sylfaen", 14F);
            btnCancel.Location = new Point(441, 274);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(194, 49);
            btnCancel.TabIndex = 7;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // pbCoverPreview
            // 
            pbCoverPreview.Location = new Point(1025, 49);
            pbCoverPreview.Name = "pbCoverPreview";
            pbCoverPreview.Size = new Size(314, 274);
            pbCoverPreview.TabIndex = 8;
            pbCoverPreview.TabStop = false;
            // 
            // btnBrowseCover
            // 
            btnBrowseCover.Font = new Font("Sylfaen", 14F);
            btnBrowseCover.Location = new Point(706, 76);
            btnBrowseCover.Name = "btnBrowseCover";
            btnBrowseCover.Size = new Size(245, 49);
            btnBrowseCover.TabIndex = 9;
            btnBrowseCover.Text = "Browse image...";
            btnBrowseCover.UseVisualStyleBackColor = true;
            btnBrowseCover.Click += btnBrowseCover_Click;
            // 
            // btnClearCover
            // 
            btnClearCover.Font = new Font("Sylfaen", 14F);
            btnClearCover.Location = new Point(706, 149);
            btnClearCover.Name = "btnClearCover";
            btnClearCover.Size = new Size(245, 49);
            btnClearCover.TabIndex = 10;
            btnClearCover.Text = "Clear Image";
            btnClearCover.UseVisualStyleBackColor = true;
            btnClearCover.Click += btnClearCover_Click;
            // 
            // BookDetailsForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1377, 371);
            Controls.Add(btnClearCover);
            Controls.Add(btnBrowseCover);
            Controls.Add(pbCoverPreview);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(labelName);
            Controls.Add(txtKeywords);
            Controls.Add(txtAuthor);
            Controls.Add(txtTitle);
            Name = "BookDetailsForm";
            Text = "Add Book";
            ((System.ComponentModel.ISupportInitialize)pbCoverPreview).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtTitle;
        private TextBox txtAuthor;
        private TextBox txtKeywords;
        private Label labelName;
        private Label label1;
        private Label label2;
        private Button btnSave;
        private Button btnCancel;
        private PictureBox pbCoverPreview;
        private Button btnBrowseCover;
        private Button btnClearCover;
    }
}