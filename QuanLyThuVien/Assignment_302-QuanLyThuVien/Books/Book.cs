namespace Assignment_302_QuanLyThuVien
{
    public abstract class Book
    {
        string bookID;
        string title;
        string author;
        int year;

        public string BookID { get => bookID; set => bookID = value; }
        public string Title { get => title; set => title = value; }
        public string Author { get => author; set => author = value; }
        public int Year { get => year; set => year = value; }

        public Book(string bookID, string title, string author, int year)
        {
            this.bookID = bookID;
            this.title = title;
            this.author = author;
            this.year = year;
        }
    }
}