namespace Assignment_302_QuanLyThuVien
{
    using System.Linq;
    public class BookManager
    {
        List<Book> books = new();

        public List<Book> Books { get => books; }

        public void AddBook()
        {
            Book book;
            while (true)
            {
                Console.WriteLine("Chon loai sach muon them");
                Console.WriteLine("1. sach giao trinh | 2. tieu thuyet |0. thoat");
                switch (InputCheck.Int("Chon: "))
                {
                    case 1:
                        Console.WriteLine("Them sach giao trinh");
                        book = new TextBook(
                            InputCheck.String("Nhap ma sach: "),
                            InputCheck.String("Nhap ten sach: "),
                            InputCheck.String("Nhap ten tac gia: "),
                            InputCheck.String("Nhap mon hoc: "),
                            InputCheck.Int("Nhap nam xuat ban: ")
                        );

                        //kiem tra xem sach da ton tai chua
                        if (books.Any(b => b.BookID == book.BookID))
                        {
                            Console.WriteLine("Ma sach da ton tai");
                            return;
                        }
                        books.Add(book);
                        Console.WriteLine("Sach da duoc them!");
                        return;

                    case 2:
                        Console.WriteLine("Them sach tieu thuyet");
                        book = new Novel(
                            InputCheck.String("Nhap ma sach: "),
                            InputCheck.String("Nhap ten sach: "),
                            InputCheck.String("Nhap ten tac gia: "),
                            InputCheck.String("Nhap the loai: "),
                            InputCheck.Int("Nhap nam xuat ban: ")
                        );

                        //kiem tra xem sach da ton tai chua
                        if (books.Any(b => b.BookID == book.BookID))
                        {
                            Console.WriteLine("Ma sach da ton tai");
                            return;
                        }
                        books.Add(book);
                        Console.WriteLine("Sach da duoc them!");
                        return;

                    case 0:
                        return;
                    default:
                        Console.WriteLine("Lua chon khong hop le");
                        break;
                }
            }
        }
        
        public void UpdateBook()
        {
            Console.WriteLine("Cap nhat thong tin");
            Book? b = SearchByID();
            if (b == null)
            {
                return;
            }

            bool isDone = false;
            while (isDone)
            {
                Console.WriteLine("Nhap thong tin can sua");
                Console.WriteLine("1. ten sach | 2. tac gia | 3. nam xuat ban | 0. thoat");
                switch (InputCheck.Int("Chon: "))
                {
                    case 1:
                        Console.WriteLine("Sua ten sach");
                        foreach (Book book in books)
                        {
                            if (book.BookID == b.BookID)
                            {
                                Console.WriteLine($"Ten sach cu: {book.Title}");
                                book.BookID = InputCheck.String("Nhap ten sach moi");
                                Console.WriteLine("Ten sach da duoc cap nhat");
                            }
                        }
                        break;

                    case 2:
                        Console.WriteLine("Sua ten tac gia");
                        foreach (Book book in books)
                        {
                            if (book.BookID == b.BookID)
                            {
                                Console.WriteLine($"Ten tac gia cu: {book.Author}");
                                book.Author = InputCheck.String("Nhap ten tac gia moi");
                                Console.WriteLine("Ten tac gia da duoc cap nhat");
                            }
                        }
                        break;

                    case 3:
                        Console.WriteLine("Sua nam xuat ban");
                        foreach (Book book in books)
                        {
                            if (book.BookID == b.BookID)
                            {
                                Console.WriteLine($"Nam xuat ban cu: {book.Year}");
                                book.Year = InputCheck.Int("Nhap nam xuat ban moi");
                                Console.WriteLine("Nam xuat ban da duoc cap nhat");
                            }
                        }
                        break;

                    case 0:
                        isDone = true;
                        break;
                    default:
                        Console.WriteLine("Nhap khong hop le");
                        break;
                }
            }
            ShowBook(b);
        }

        public void RemoveBook()
        {
            Console.WriteLine("Xoa sach ra khoi thu vien");
            Book? b = SearchByID();
            if (b == null)
            {
                return;
            }

            ShowBook(b);
            switch (InputCheck.Int("Ban co muon xoa cuon sach nay?\n1. xac nhan | 2. tu choi"))
            {
                case 1:
                    books.Remove(b);
                    Console.WriteLine("Sach da duoc xoa");
                    break;
                default:
                    Console.WriteLine("Sach khong duoc xoa");
                    break;
            }
        }

        #region show methods
        public void ShowAllBooks()
        {
            ShowBook(books);
        }

        public void ShowBook(List<Book> list)
        {
            Console.WriteLine($"{"BookID",-10} | {"Title",-50} | {"Author",-40} | {"Year",-10} | {"Genre",-20} | {"Type",-20}");
            Console.WriteLine(new string('-', 150));
            foreach (Book book in list)
            {
                Console.WriteLine($"{book.BookID,-10} | {book.Title,-50} | {book.Author,-40} | {book.Year,-10} | {GetBookGerne(book),-20} | {book.GetType().Name,-20}");
            }
            Console.WriteLine(new string('-', 150) + "\n");
        }

        public void ShowBook(Book book)
        {
            Console.WriteLine($"{"BookID",-10} | {"Title",-50} | {"Author",-40} | {"Year",-10} | {"Genre",-20} | {"Type",-20}");
            Console.WriteLine(new string('-', 150));
            Console.WriteLine($"{book.BookID,-10} | {book.Title,-50} | {book.Author,-40} | {book.Year,-10} | {GetBookGerne(book),-20} | {book.GetType().Name,-20}");
            Console.WriteLine(new string('-', 150) + "\n");
        }

        string? GetBookGerne(Book b)
        {
            if (b is Novel)
            {
                return (b as Novel)?.Genre;
            }
            else
            {
                return (b as TextBook)?.Subject;
            }
        }
        #endregion

        #region search methods
        public void SearchByAuthorName()
        {
            Console.WriteLine("Tim kiem theo ten tac gia");
            List<Book> find = books.FindAll(b => b.Author == InputCheck.String("Nhap ten tac gia"));
            if (find.Count == 0)
            {
                Console.WriteLine("Khong tim thay sach");
            }
            ShowBook(find);
        }

        public void SearchByBookName()
        {
            Console.WriteLine("Tim kiem theo ten sach");
            List<Book> find = books.FindAll(b => b.Title == InputCheck.String("Nhap ten sach"));
            if (find.Count == 0)
            {
                Console.WriteLine("Khong tim thay sach");
            }
            ShowBook(find);
        }

        public Book? SearchByID()
        {
            Console.WriteLine("Tim kiem theo ma sach");
            Book? find = books.Find(b => b.BookID == InputCheck.String("Nhap ma sach"));
            if (find != null)
            {
                Console.WriteLine("Khong tim thay sach");
            }
            return find;
        }
        #endregion

        #region sort methods
        public void SortByName()
        {
            books.Sort((b1, b2) => b1.Title.CompareTo(b2.Title));
            ShowAllBooks();
        }

        public void SortByID()
        {
            books.Sort((b1, b2) => b1.BookID.CompareTo(b2.BookID));
            ShowAllBooks();
        }
        #endregion
    }
}