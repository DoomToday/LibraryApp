using LibraryCatalog.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibraryCatalog.Forms
{
    public partial class BookDetailsForm : Form
    {
        public string BookTitle { get; private set; }
        public string BookAuthor { get; private set; }
        public List<string> BookKeywords { get; private set; }
        public string CoverImagePath { get; private set; }
        private string _tempImagePath;

        public BookDetailsForm(Book existingBook = null)
        {
            InitializeComponent();

            if (existingBook != null)
            {
                this.Text = "Edit Book";
                txtTitle.Text = existingBook.Title;
                txtAuthor.Text = existingBook.Author;
                txtKeywords.Text = string.Join(", ", existingBook.Keywords);

                CoverImagePath = existingBook.CoverImagePath;
                _tempImagePath = existingBook.CoverImagePath;

                if (!string.IsNullOrEmpty(_tempImagePath) && File.Exists(_tempImagePath))
                {
                    using (var fs = new FileStream(_tempImagePath, FileMode.Open, FileAccess.Read))
                    {
                        pbCoverPreview.Image = System.Drawing.Image.FromStream(fs);
                    }
                }
            }
            else
            {
                this.Text = "Add New Book";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text) || string.IsNullOrWhiteSpace(txtAuthor.Text))
            {
                MessageBox.Show("Title and Author are required!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            BookTitle = txtTitle.Text.Trim();
            BookAuthor = txtAuthor.Text.Trim();
            BookKeywords = txtKeywords.Text.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                           .Select(k => k.Trim()).ToList();

            // Handle the image saving magic!
            if (_tempImagePath != CoverImagePath) // Only copy if they picked a NEW image
            {
                if (!string.IsNullOrEmpty(_tempImagePath))
                {
                    if (!Directory.Exists("Covers")) Directory.CreateDirectory("Covers");

                    string ext = Path.GetExtension(_tempImagePath);
                    string finalPath = $"Covers/{Guid.NewGuid()}{ext}"; // Unique name so files don't overwrite!

                    File.Copy(_tempImagePath, finalPath, true);
                    CoverImagePath = finalPath;
                }
                else
                {
                    CoverImagePath = null; // They cleared the image!
                }
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnBrowseCover_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    _tempImagePath = ofd.FileName;

                    // Show a preview without locking the file
                    pbCoverPreview.Image?.Dispose();
                    using (var fs = new FileStream(_tempImagePath, FileMode.Open, FileAccess.Read))
                    {
                        pbCoverPreview.Image = System.Drawing.Image.FromStream(fs);
                    }
                }
            }
        }

        private void btnClearCover_Click(object sender, EventArgs e)
        {
            _tempImagePath = null;
            pbCoverPreview.Image?.Dispose();
            pbCoverPreview.Image = null;
        }
    }
}
