using System;
using System.Collections.Generic;

namespace LibraryCatalog.Models
{
    public class Book
    {
        public int Id { get; private set; }
        public string Title { get; private set; }
        public string Author { get; private set; }
        public List<string> Keywords { get; private set; }

        public string CoverImagePath { get; private set; }
        public bool IsAvailable { get; private set; }

        public Book(int id, string title, string author, List<string> keywords, bool isAvailable = true, string coverImagePath = null)
        {
            Id = id;
            IsAvailable = isAvailable;
            UpdateDetails(title, author, keywords, coverImagePath);
        }

        public void UpdateDetails(string newTitle, string newAuthor, List<string> newKeywords, string newCoverImagePath)
        {
            if (string.IsNullOrWhiteSpace(newTitle))
                throw new ArgumentException("Title cannot be empty.");

            if (string.IsNullOrWhiteSpace(newAuthor))
                throw new ArgumentException("Author cannot be empty.");

            Title = newTitle;
            Author = newAuthor;
            Keywords = newKeywords ?? new List<string>();
            CoverImagePath = newCoverImagePath;
        }

        // --- NEW ENCAPSULATED METHODS ---

        public void BorrowBook()
        {
            if (!IsAvailable)
                throw new InvalidOperationException("Book is already checked out!");
            IsAvailable = false;
        }

        public void ReturnBook()
        {
            if (IsAvailable)
                throw new InvalidOperationException("Book is already in the library!");
            IsAvailable = true;
        }

        public string KeywordsDisplay => string.Join(", ", Keywords);

        // This makes it look nice in the DataGridView instead of just saying "True" or "False"
        public string StatusDisplay => IsAvailable ? "Available" : "Borrowed";
    }
}