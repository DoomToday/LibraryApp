using LibraryCatalog.Models;
using LibraryCatalog.Services;
namespace LibraryCatalog.Forms
{
    public partial class MainForm : Form
    {
        private UserRole _currentUserRole;
        private LibraryRepository _repository;

        public MainForm(UserRole role)
        {
            InitializeComponent();
            _currentUserRole = role;
            _repository = new LibraryRepository(); // Initialize our backend!

            ApplyPermissions();

            // Populate the search combo box so it's not empty
            cmbSearchType.Items.Add("Title");
            cmbSearchType.Items.Add("Author");
            cmbSearchType.Items.Add("Keyword");
            cmbSearchType.SelectedIndex = 0; // Default to Title

            RefreshGrid(_repository.GetAllBooks());
        }

        private void ApplyPermissions()
        {
            if (_currentUserRole == UserRole.Guest)
            {
                btnAddBook.Visible = false;
                btnEditBook.Visible = false;
                btnDeleteBook.Visible = false;
                btnToggleStatus.Visible = false;
            }
            else
            {
                btnAddBook.Visible = true;
                btnEditBook.Visible = true;
                btnDeleteBook.Visible = true;
                btnToggleStatus.Visible = true;
            }
        }
        private void RefreshGrid(List<Book> books)
        {
            dgvBooks.DataSource = null;
            dgvBooks.DataSource = books;

            dgvBooks.AllowUserToAddRows = false;

            if (dgvBooks.Columns["Keywords"] != null) dgvBooks.Columns["Keywords"].Visible = false;
            if (dgvBooks.Columns["CoverImagePath"] != null) dgvBooks.Columns["CoverImagePath"].Visible = false;
            if (dgvBooks.Columns["IsAvailable"] != null) dgvBooks.Columns["IsAvailable"].Visible = false;

            if (dgvBooks.Columns["StatusDisplay"] != null) dgvBooks.Columns["StatusDisplay"].HeaderText = "Status";
            if (dgvBooks.Columns["KeywordsDisplay"] != null) dgvBooks.Columns["KeywordsDisplay"].HeaderText = "Keywords";

            if (dgvBooks.Columns["Title"] != null) dgvBooks.Columns["Title"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            if (dgvBooks.Columns["Author"] != null) dgvBooks.Columns["Author"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            if (dgvBooks.Columns["StatusDisplay"] != null) dgvBooks.Columns["StatusDisplay"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            if (dgvBooks.Columns["Id"] != null) dgvBooks.Columns["Id"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            lblStatus.Text = $"Role: {_currentUserRole} | Showing {books.Count} records.";
        }

        // --- SEARCH CONTROLS ---

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string query = txtSearchQuery.Text.Trim();
            string searchType = cmbSearchType.SelectedItem.ToString();
            List<Book> results = new List<Book>();

            if (searchType == "Title")
                results = _repository.SearchByTitle(query);
            else if (searchType == "Author")
                results = _repository.SearchByAuthor(query);
            else if (searchType == "Keyword")
                results = _repository.SearchByKeyword(query);

            RefreshGrid(results);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtSearchQuery.Clear();
            RefreshGrid(_repository.GetAllBooks());
        }

        // --- ADMIN CONTROLS ---

        private void btnToggleStatus_Click(object sender, EventArgs e)
        {
            if (dgvBooks.SelectedRows.Count == 0) return;

            // Grab the ID from the selected row
            int bookId = (int)dgvBooks.SelectedRows[0].Cells["Id"].Value;

            if (_repository.ToggleBookStatus(bookId))
            {
                RefreshGrid(_repository.GetAllBooks());
            }
        }

        private void btnDeleteBook_Click(object sender, EventArgs e)
        {
            if (dgvBooks.SelectedRows.Count == 0) return;

            int bookId = (int)dgvBooks.SelectedRows[0].Cells["Id"].Value;
            string bookTitle = dgvBooks.SelectedRows[0].Cells["Title"].Value.ToString();

            var confirmResult = MessageBox.Show($"Are you sure you want to delete '{bookTitle}'?",
                                     "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirmResult == DialogResult.Yes)
            {
                _repository.DeleteBook(bookId);
                RefreshGrid(_repository.GetAllBooks());
            }
        }

        // You still need to make the Add and Edit forms!
        private void btnAddBook_Click(object sender, EventArgs e)
        {
            using (var detailsForm = new BookDetailsForm())
            {
                if (detailsForm.ShowDialog() == DialogResult.OK)
                {
                    // Pass the new CoverImagePath to the repository!
                    _repository.AddBook(detailsForm.BookTitle, detailsForm.BookAuthor, detailsForm.BookKeywords, detailsForm.CoverImagePath);
                    RefreshGrid(_repository.GetAllBooks());
                }
            }
        }

        private void btnEditBook_Click(object sender, EventArgs e)
        {
            if (dgvBooks.SelectedRows.Count == 0) return;

            int bookId = (int)dgvBooks.SelectedRows[0].Cells["Id"].Value;
            var bookToEdit = _repository.GetAllBooks().FirstOrDefault(b => b.Id == bookId);

            if (bookToEdit != null)
            {
                using (var detailsForm = new BookDetailsForm(bookToEdit))
                {
                    if (detailsForm.ShowDialog() == DialogResult.OK)
                    {
                        // Update it in the repository!
                        _repository.UpdateBook(bookId, detailsForm.BookTitle, detailsForm.BookAuthor, detailsForm.BookKeywords, detailsForm.CoverImagePath);
                        RefreshGrid(_repository.GetAllBooks());
                    }
                }
            }
        }
        private void dgvBooks_SelectionChanged(object sender, EventArgs e)
        {
            // Clear the picture box first in case we select a book with no image
            pbCover.Image?.Dispose(); // Free up memory!
            pbCover.Image = null;

            if (dgvBooks.SelectedRows.Count > 0)
            {
                int bookId = (int)dgvBooks.SelectedRows[0].Cells["Id"].Value;
                var selectedBook = _repository.GetAllBooks().FirstOrDefault(b => b.Id == bookId);

                // Check if the book actually has an image path, and if that file still exists on the computer
                if (selectedBook != null && !string.IsNullOrEmpty(selectedBook.CoverImagePath) && System.IO.File.Exists(selectedBook.CoverImagePath))
                {
                    // I told you this before: DO NOT use Image.FromFile() or you will lock the file! 
                    // Use a FileStream so WinForms lets go of the file after loading it.
                    using (var fs = new System.IO.FileStream(selectedBook.CoverImagePath, System.IO.FileMode.Open, System.IO.FileAccess.Read))
                    {
                        pbCover.Image = System.Drawing.Image.FromStream(fs);
                    }
                }
            }
        }
    }
}
