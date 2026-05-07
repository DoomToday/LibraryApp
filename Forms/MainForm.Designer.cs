namespace LibraryCatalog.Forms
{
    partial class MainForm
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
            dgvBooks = new DataGridView();
            gbSearch = new GroupBox();
            btnReset = new Button();
            btnSearch = new Button();
            cmbSearchType = new ComboBox();
            txtSearchQuery = new TextBox();
            lblStatus = new Label();
            btnAddBook = new Button();
            btnEditBook = new Button();
            btnDeleteBook = new Button();
            groupBox1 = new GroupBox();
            btnToggleStatus = new Button();
            pbCover = new PictureBox();
            label3 = new Label();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvBooks).BeginInit();
            gbSearch.SuspendLayout();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbCover).BeginInit();
            SuspendLayout();
            // 
            // dgvBooks
            // 
            dgvBooks.AllowUserToAddRows = false;
            dgvBooks.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvBooks.Location = new Point(23, 190);
            dgvBooks.Name = "dgvBooks";
            dgvBooks.ReadOnly = true;
            dgvBooks.RowHeadersVisible = false;
            dgvBooks.RowHeadersWidth = 62;
            dgvBooks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBooks.Size = new Size(1114, 558);
            dgvBooks.TabIndex = 0;
            dgvBooks.SelectionChanged += dgvBooks_SelectionChanged;
            // 
            // gbSearch
            // 
            gbSearch.Controls.Add(btnReset);
            gbSearch.Controls.Add(btnSearch);
            gbSearch.Controls.Add(cmbSearchType);
            gbSearch.Controls.Add(txtSearchQuery);
            gbSearch.Font = new Font("Sylfaen", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
            gbSearch.Location = new Point(23, 12);
            gbSearch.Name = "gbSearch";
            gbSearch.Size = new Size(578, 172);
            gbSearch.TabIndex = 1;
            gbSearch.TabStop = false;
            gbSearch.Text = "Search";
            // 
            // btnReset
            // 
            btnReset.Location = new Point(338, 108);
            btnReset.Name = "btnReset";
            btnReset.Size = new Size(215, 46);
            btnReset.TabIndex = 3;
            btnReset.Text = "Show All";
            btnReset.UseVisualStyleBackColor = true;
            btnReset.Click += btnReset_Click;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(338, 43);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(215, 46);
            btnSearch.TabIndex = 2;
            btnSearch.Text = "Search";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // cmbSearchType
            // 
            cmbSearchType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSearchType.FormattingEnabled = true;
            cmbSearchType.Location = new Point(29, 110);
            cmbSearchType.Name = "cmbSearchType";
            cmbSearchType.Size = new Size(282, 44);
            cmbSearchType.TabIndex = 1;
            // 
            // txtSearchQuery
            // 
            txtSearchQuery.Location = new Point(29, 45);
            txtSearchQuery.Name = "txtSearchQuery";
            txtSearchQuery.PlaceholderText = "Search term...";
            txtSearchQuery.Size = new Size(282, 44);
            txtSearchQuery.TabIndex = 0;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Font = new Font("Book Antiqua", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblStatus.Location = new Point(23, 772);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(69, 27);
            lblStatus.TabIndex = 2;
            lblStatus.Text = "label1";
            // 
            // btnAddBook
            // 
            btnAddBook.Font = new Font("Sylfaen", 14F);
            btnAddBook.Location = new Point(17, 43);
            btnAddBook.Name = "btnAddBook";
            btnAddBook.Size = new Size(235, 46);
            btnAddBook.TabIndex = 4;
            btnAddBook.Text = "Add Book";
            btnAddBook.UseVisualStyleBackColor = true;
            btnAddBook.Click += btnAddBook_Click;
            // 
            // btnEditBook
            // 
            btnEditBook.Font = new Font("Sylfaen", 14F);
            btnEditBook.Location = new Point(17, 108);
            btnEditBook.Name = "btnEditBook";
            btnEditBook.Size = new Size(235, 46);
            btnEditBook.TabIndex = 5;
            btnEditBook.Text = "Edit Book";
            btnEditBook.UseVisualStyleBackColor = true;
            btnEditBook.Click += btnEditBook_Click;
            // 
            // btnDeleteBook
            // 
            btnDeleteBook.Font = new Font("Sylfaen", 14F);
            btnDeleteBook.Location = new Point(285, 43);
            btnDeleteBook.Name = "btnDeleteBook";
            btnDeleteBook.Size = new Size(235, 46);
            btnDeleteBook.TabIndex = 6;
            btnDeleteBook.Text = "Delete Book";
            btnDeleteBook.UseVisualStyleBackColor = true;
            btnDeleteBook.Click += btnDeleteBook_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btnToggleStatus);
            groupBox1.Controls.Add(btnAddBook);
            groupBox1.Controls.Add(btnDeleteBook);
            groupBox1.Controls.Add(btnEditBook);
            groupBox1.Font = new Font("Sylfaen", 14F);
            groupBox1.ForeColor = SystemColors.ControlText;
            groupBox1.Location = new Point(612, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(525, 172);
            groupBox1.TabIndex = 7;
            groupBox1.TabStop = false;
            groupBox1.Text = "Admin";
            // 
            // btnToggleStatus
            // 
            btnToggleStatus.Font = new Font("Sylfaen", 14F);
            btnToggleStatus.Location = new Point(285, 110);
            btnToggleStatus.Name = "btnToggleStatus";
            btnToggleStatus.Size = new Size(235, 46);
            btnToggleStatus.TabIndex = 7;
            btnToggleStatus.Text = "Checkout/Return";
            btnToggleStatus.UseVisualStyleBackColor = true;
            btnToggleStatus.Click += btnToggleStatus_Click;
            // 
            // pbCover
            // 
            pbCover.Location = new Point(1171, 190);
            pbCover.Name = "pbCover";
            pbCover.Size = new Size(402, 558);
            pbCover.SizeMode = PictureBoxSizeMode.Zoom;
            pbCover.TabIndex = 8;
            pbCover.TabStop = false;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Monotype Corsiva", 28F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label3.Location = new Point(1171, 34);
            label3.Name = "label3";
            label3.Size = new Size(409, 67);
            label3.TabIndex = 9;
            label3.Text = "Librarian's Dream";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Monotype Corsiva", 18F, FontStyle.Italic, GraphicsUnit.Point, 0);
            label1.Location = new Point(1164, 101);
            label1.Name = "label1";
            label1.Size = new Size(427, 44);
            label1.TabIndex = 10;
            label1.Text = "Books for kids and adults alike!";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1604, 830);
            Controls.Add(label1);
            Controls.Add(label3);
            Controls.Add(pbCover);
            Controls.Add(groupBox1);
            Controls.Add(lblStatus);
            Controls.Add(gbSearch);
            Controls.Add(dgvBooks);
            Name = "MainForm";
            Text = "Librarian's Dream";
            ((System.ComponentModel.ISupportInitialize)dgvBooks).EndInit();
            gbSearch.ResumeLayout(false);
            gbSearch.PerformLayout();
            groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pbCover).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvBooks;
        private GroupBox gbSearch;
        private Button btnSearch;
        private ComboBox cmbSearchType;
        private TextBox txtSearchQuery;
        private Button btnReset;
        private Label lblStatus;
        private Button btnAddBook;
        private Button btnEditBook;
        private Button btnDeleteBook;
        private GroupBox groupBox1;
        private Button btnToggleStatus;
        private PictureBox pbCover;
        private Label label3;
        private Label label1;
    }
}