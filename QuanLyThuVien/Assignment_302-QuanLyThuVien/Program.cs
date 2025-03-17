namespace Assignment_302_QuanLyThuVien
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            // Khởi tạo các đối tượng quản lý
            BookManager bookManager = new();
            ReaderManager readerManager = new();
            BorrowReturnManager borrowReturnManager = new();

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\n===== QUAN LY THU VIEN =====");
                Console.WriteLine("1. Quan ly sach");
                Console.WriteLine("2. Quan ly doc gia");
                Console.WriteLine("3. Quan ly muon/tra");
                Console.WriteLine("0. Thoat");

                switch (InputCheck.Int("Chon chuc nang: "))
                {
                    case 1:
                        BookManagerMenu(bookManager);
                        break;
                    case 2:
                        ReaderManagerMenu(readerManager);
                        break;
                    case 3:
                        BorrowReturnManagerMenu(borrowReturnManager);
                        break;
                    case 0:
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Lua chon khong hop le!");
                        break;
                }
            }
        }

        private static void BookManagerMenu(BookManager bookManager)
        {
            bool back = false;
            while (!back)
            {
                Console.WriteLine("\n===== QUAN LY SACH =====");
                Console.WriteLine("1. Them sach");
                Console.WriteLine("2. Cap nhat thong tin sach");
                Console.WriteLine("3. Xoa sach");
                Console.WriteLine("4. Hien thi danh sach sach");
                Console.WriteLine("5. Tim kiem sach");
                Console.WriteLine("6. Luu danh sach sach vao file");
                Console.WriteLine("7. Doc danh sach sach tu file");
                Console.WriteLine("0. Quay lai");

                switch (InputCheck.Int("Chon chuc nang: "))
                {
                    case 1:
                        bookManager.AddBook();
                        break;
                    case 2:
                        bookManager.UpdateBook();
                        break;
                    case 3:
                        bookManager.RemoveBook();
                        break;
                    case 4:
                        bookManager.ShowAllBooks();
                        break;
                    case 5:
                        SearchBookMenu(bookManager);
                        break;
                    case 6:
                        bookManager.SaveToFile();
                        break;
                    case 7:
                        bookManager.LoadFromFile();
                        break;
                    case 0:
                        back = true;
                        break;
                    default:
                        Console.WriteLine("Lua chon khong hop le!");
                        break;
                }
            }
        }

        private static void SearchBookMenu(BookManager bookManager)
        {
            Console.WriteLine("\n===== TIM KIEM SACH =====");
            Console.WriteLine("1. Tim theo ten sach");
            Console.WriteLine("2. Tim theo tac gia");
            Console.WriteLine("3. Tim theo ma sach");

            switch (InputCheck.Int("Chon cach tim kiem: "))
            {
                case 1:
                    bookManager.SearchByBookName();
                    break;
                case 2:
                    bookManager.SearchByAuthorName();
                    break;
                case 3:
                    Book? book = bookManager.SearchByID();
                    if (book != null)
                    {
                        bookManager.ShowBook(book);
                    }
                    break;
                default:
                    Console.WriteLine("Lua chon khong hop le!");
                    break;
            }
        }

        private static void ReaderManagerMenu(ReaderManager readerManager)
        {
            bool back = false;
            while (!back)
            {
                Console.WriteLine("\n===== QUAN LY DOC GIA =====");
                Console.WriteLine("1. Them doc gia");
                Console.WriteLine("2. Cap nhat thong tin doc gia");
                Console.WriteLine("3. Xoa doc gia");
                Console.WriteLine("4. Hien thi danh sach doc gia");
                Console.WriteLine("5. Tim kiem doc gia");
                Console.WriteLine("6. Luu danh sach doc gia vao file");
                Console.WriteLine("7. Doc danh sach doc gia tu file");
                Console.WriteLine("0. Quay lai");

                switch (InputCheck.Int("Chon chuc nang: "))
                {
                    case 1:
                        readerManager.AddReader();
                        break;
                    case 2:
                        readerManager.UpdateReader();
                        break;
                    case 3:
                        readerManager.RemoveReader();
                        break;
                    case 4:
                        readerManager.ShowAllReader();
                        break;
                    case 5:
                        SearchReaderMenu(readerManager);
                        break;
                    case 6:
                        readerManager.SaveToFile();
                        break;
                    case 7:
                        readerManager.LoadFromFile();
                        break;
                    case 0:
                        back = true;
                        break;
                    default:
                        Console.WriteLine("Lua chon khong hop le!");
                        break;
                }
            }
        }

        private static void SearchReaderMenu(ReaderManager readerManager)
        {
            Console.WriteLine("\n===== TIM KIEM DOC GIA =====");
            Console.WriteLine("1. Tim theo ma doc gia");
            Console.WriteLine("2. Tim theo ten doc gia");
            Console.WriteLine("0. Quay lai");

            switch (InputCheck.Int("Chon cach tim kiem: "))
            {
                case 1:
                    Reader? reader = readerManager.SearchByID();
                    if (reader != null)
                    {
                        readerManager.ShowReader(reader);
                    }
                    break;
                case 2:
                    List<Reader> readers = readerManager.SearchByName();
                    if (readers.Count > 0)
                    {
                        readerManager.ShowReader(readers);
                    }
                    break;
                case 0:
                    return;
                default:
                    Console.WriteLine("Lua chon khong hop le!");
                    break;
            }
        }

        private static void BorrowReturnManagerMenu(BorrowReturnManager borrowReturnManager)
        {
            bool back = false;
            while (!back)
            {
                Console.WriteLine("\n===== QUAN LY MUON/TRA =====");
                Console.WriteLine("1. Them thong tin muon/tra");
                Console.WriteLine("2. Cap nhat thong tin muon/tra");
                Console.WriteLine("3. Xoa thong tin muon/tra");
                Console.WriteLine("4. Hien thi danh sach muon/tra");
                Console.WriteLine("5. Tim kiem thong tin muon/tra");
                Console.WriteLine("6. Luu thong tin muon/tra vao file");
                Console.WriteLine("7. Doc thong tin muon/tra tu file");
                Console.WriteLine("0. Quay lai");

                switch (InputCheck.Int("Chon chuc nang: "))
                {
                    case 1:
                        borrowReturnManager.AddRecord();
                        break;
                    case 2:
                        borrowReturnManager.UpdateRecord();
                        break;
                    case 3:
                        borrowReturnManager.RemoveRecord();
                        break;
                    case 4:
                        borrowReturnManager.ShowAllRecord();
                        break;
                    case 5:
                        Reader? reader = borrowReturnManager.SearchRecord();
                        if (reader != null)
                        {
                            borrowReturnManager.ShowRecord(reader);
                        }
                        break;
                    case 6:
                        borrowReturnManager.SaveToFile();
                        break;
                    case 7:
                        borrowReturnManager.LoadFromFile();
                        break;
                    case 0:
                        back = true;
                        break;
                    default:
                        Console.WriteLine("Lua chon khong hop le!");
                        break;
                }
            }
        }
    }
}