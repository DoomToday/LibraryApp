using LibraryCatalog.Models;
using System.Text.Json;

namespace LibraryCatalog.Repositories
{
    public class LibraryRepository
    {
        private List<Book> _books;
        private int _nextId;

        // This is where your data will live in your project's output bin/Debug folder
        private readonly string _filePath = "library_data.json";

        public LibraryRepository()
        {
            LoadData();
        }

        // --- FILE I/O OPERATIONS ---

        private void LoadData()
        {
            if (File.Exists(_filePath))
            {
                // Read the file and deserialize it back into our list of objects
                string json = File.ReadAllText(_filePath);
                _books = JsonSerializer.Deserialize<List<Book>>(json) ?? new List<Book>();

                _nextId = _books.Any() ? _books.Max(b => b.Id) + 1 : 1;
            }
            else
            {
                _books = new List<Book>();
                _nextId = 1;

                AddBook("The C++ Programming Language", "Bjarne Stroustrup", new List<string> { "programming", "cpp", "guide" }, null);
                AddBook("C# in Depth", "Jon Skeet", new List<string> { "programming", "csharp", "advanced" }, null);
            }
        }

        private void SaveData()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(_books, options);
            File.WriteAllText(_filePath, json);
        }

        // --- GUEST SCENARIOS (Search) ---

        public List<Book> GetAllBooks() => _books.ToList();

        public List<Book> SearchByTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title)) return GetAllBooks();
            return _books.Where(b => b.Title.IndexOf(title, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
        }

        public List<Book> SearchByAuthor(string author)
        {
            if (string.IsNullOrWhiteSpace(author)) return GetAllBooks();
            return _books.Where(b => b.Author.IndexOf(author, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
        }

        public List<Book> SearchByKeyword(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword)) return GetAllBooks();
            return _books.Where(b => b.Keywords.Any(k => k.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0)).ToList();
        }

        // --- ADMIN SCENARIOS (CRUD) ---

        public void AddBook(string title, string author, List<string> keywords, string coverImagePath)
        {
            var newBook = new Book(_nextId++, title, author, keywords, true, coverImagePath );
            _books.Add(newBook);
            SaveData(); // Save changes!
        }

        public bool UpdateBook(int id, string title, string author, List<string> keywords, string coverImagePath)
        {
            var book = _books.FirstOrDefault(b => b.Id == id);
            if (book != null)
            {
                book.UpdateDetails(title, author, keywords, coverImagePath);
                SaveData(); // Save changes!
                return true;
            }
            return false;
        }

        public bool DeleteBook(int id)
        {
            var book = _books.FirstOrDefault(b => b.Id == id);
            if (book != null)
            {
                _books.Remove(book);
                SaveData(); // Save changes!
                return true;
            }
            return false;
        }
        public bool ToggleBookStatus(int id)
        {
            var book = _books.FirstOrDefault(b => b.Id == id);
            if (book != null)
            {
                if (book.IsAvailable)
                {
                    book.BorrowBook();
                }
                else
                {
                    book.ReturnBook();
                }

                SaveData(); // Don't forget to save to JSON!
                return true;
            }
            return false;
        }
    }
}