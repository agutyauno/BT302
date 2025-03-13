namespace Assignment_302_QuanLyThuVien
{
    public class TextBook : Book
    {
        string subject;
        
        public string Subject { get => subject; set => subject = value; }

        public TextBook(string bookID, string title, string author, string subject, int year) : base(bookID, title, author, year)
        {
            this.subject = subject;
        }

    }
}