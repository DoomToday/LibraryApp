using LibraryCatalog.Models;
using LibraryCatalog.Repositories;  

namespace LibraryCatalog.Forms
{
    public partial class MainForm : Form
    {
        private User _currentUser; // Store the whole user object
        private UserRole _currentUserRole;
        private LibraryRepository _libraryRepository;
        private UserRepository _userRepository;

        public MainForm(User user, UserRepository userRepository)
        {
            InitializeComponent();
            _currentUser = user;
            // Fallback to Guest if a null user is somehow passed
            _currentUserRole = user != null ? user.Role : UserRole.Guest;

            _libraryRepository = new LibraryRepository();
            _userRepository = userRepository;

            ApplyPermissions();

            // Populate the search combo box so it's not empty
            cmbSearchType.Items.Add("Title");
            cmbSearchType.Items.Add("Author");
            cmbSearchType.Items.Add("Keyword");
            cmbSearchType.SelectedIndex = 0; // Default to Title

            RefreshGrid(_libraryRepository.GetAllBooks());
        }

        private void ApplyPermissions()
        {
            btnAddBook.Visible = false;
            btnEditBook.Visible = false;
            btnDeleteBook.Visible = false;
            btnToggleStatus.Visible = false;
            btnAddUser.Visible = false;

            if (_currentUserRole == UserRole.Guest)
            {
                btnToggleFavorite.Visible = false;
                chkShowFavorites.Visible = false;
            }
            else
            {
                btnToggleFavorite.Visible = true;
                chkShowFavorites.Visible = true;
            }

            if (_currentUserRole == UserRole.Admin)
            {
                btnAddBook.Visible = true;
                btnEditBook.Visible = true;
                btnDeleteBook.Visible = true;
                btnToggleStatus.Visible = true;
                btnAddUser.Visible = true;
            }
        }
        private void RefreshGrid(List<Book> books)
        {
            if (chkShowFavorites.Checked && _currentUserRole != UserRole.Guest)
            {
                var favIds = _currentUser.FavoriteBookIds ?? new List<int>();
                books = books.Where(b => favIds.Contains(b.Id)).ToList();
            }

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
                results = _libraryRepository.SearchByTitle(query);
            else if (searchType == "Author")
                results = _libraryRepository.SearchByAuthor(query);
            else if (searchType == "Keyword")
                results = _libraryRepository.SearchByKeyword(query);

            RefreshGrid(results);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtSearchQuery.Clear();
            RefreshGrid(_libraryRepository.GetAllBooks());
        }

        // --- ADMIN CONTROLS ---

        private void btnToggleStatus_Click(object sender, EventArgs e)
        {
            if (dgvBooks.SelectedRows.Count == 0) return;

            // Grab the ID from the selected row
            int bookId = (int)dgvBooks.SelectedRows[0].Cells["Id"].Value;

            if (_libraryRepository.ToggleBookStatus(bookId))
            {
                RefreshGrid(_libraryRepository.GetAllBooks());
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
                _libraryRepository.DeleteBook(bookId);
                RefreshGrid(_libraryRepository.GetAllBooks());
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
                    _libraryRepository.AddBook(detailsForm.BookTitle, detailsForm.BookAuthor, detailsForm.BookKeywords, detailsForm.CoverImagePath);
                    RefreshGrid(_libraryRepository.GetAllBooks());
                }
            }
        }

        private void btnEditBook_Click(object sender, EventArgs e)
        {
            if (dgvBooks.SelectedRows.Count == 0) return;

            int bookId = (int)dgvBooks.SelectedRows[0].Cells["Id"].Value;
            var bookToEdit = _libraryRepository.GetAllBooks().FirstOrDefault(b => b.Id == bookId);

            if (bookToEdit != null)
            {
                using (var detailsForm = new BookDetailsForm(bookToEdit))
                {
                    if (detailsForm.ShowDialog() == DialogResult.OK)
                    {
                        // Update it in the repository!
                        _libraryRepository.UpdateBook(bookId, detailsForm.BookTitle, detailsForm.BookAuthor, detailsForm.BookKeywords, detailsForm.CoverImagePath);
                        RefreshGrid(_libraryRepository.GetAllBooks());
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
                var selectedBook = _libraryRepository.GetAllBooks().FirstOrDefault(b => b.Id == bookId);

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

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            // Extra safety check just in case the button was somehow clicked
            if (_currentUserRole != UserRole.Admin)
            {
                MessageBox.Show("You do not have permission to perform this action.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (var userForm = new AddUserForm(_userRepository))
            {
                userForm.ShowDialog();
            }
        }

        private void btnToggleFavorite_Click(object sender, EventArgs e)
        {
            if (dgvBooks.SelectedRows.Count == 0) return;

            int bookId = (int)dgvBooks.SelectedRows[0].Cells["Id"].Value;

            // Update the JSON database
            _userRepository.ToggleFavorite(_currentUser.Id, bookId);

            // We need to refresh our local _currentUser object so it has the latest data
            // Otherwise the grid won't update properly if the checkbox is checked!
            _currentUser = _userRepository.GetUserByUsername(_currentUser.Username);

            // Redraw the grid
            RefreshGrid(_libraryRepository.GetAllBooks());
        }

        private void dgvBooks_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (_currentUserRole == UserRole.Guest || _currentUser == null || _currentUser.FavoriteBookIds == null)
                return;

            foreach (DataGridViewRow row in dgvBooks.Rows)
            {
                int bookId = (int)row.Cells["Id"].Value;

                if (_currentUser.FavoriteBookIds.Contains(bookId))
                {

                    row.DefaultCellStyle.BackColor = Color.LightPink;


                    row.DefaultCellStyle.Font = new Font(dgvBooks.Font, FontStyle.Bold);
                }
                else
                {
                    row.DefaultCellStyle.BackColor = Color.White;
                }
            }
        }

        private void chkShowFavorites_CheckedChanged(object sender, EventArgs e)
        {
            RefreshGrid(_libraryRepository.GetAllBooks());
        }
    }
}
