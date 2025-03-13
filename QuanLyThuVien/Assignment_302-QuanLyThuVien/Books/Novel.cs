namespace Assignment_302_QuanLyThuVien
{
    public class Novel : Book
    {
        string genre;

        public string Genre { get => genre; set => genre = value; }
        
        public Novel(string bookID, string title, string author, string genre, int year) : base(bookID, title, author, year)
        {
            this.genre = genre;
        }

    }
}