namespace Assignment_302_QuanLyThuVien
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            BookManager bookManager = new();
            bookManager.AddBook();
            bookManager.ShowAllBooks();
        }
    }
}