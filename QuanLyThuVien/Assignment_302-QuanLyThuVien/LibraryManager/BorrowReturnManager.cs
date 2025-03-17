namespace Assignment_302_QuanLyThuVien
{
    struct BorrowDate
    {
        public DateOnly BorrowDay;
        public DateOnly ReturnDay;

        public BorrowDate(DateOnly borrowDay, DateOnly returnDay)
        {
            BorrowDay = borrowDay;
            ReturnDay = returnDay;
        }
    }
    public class BorrowReturnManager
    {
        Dictionary<Reader, BorrowDate> borrowRecords = new();
        ReaderManager readerManager = new();

        public void AddRecord()
        {
            Console.WriteLine("Them danh sach muon tra");
            string id = InputCheck.String("Nhap ma so cua nguoi muon sach");
            Reader? reader = readerManager.SearchByID(id);
            if (reader == null)
            {
                switch (InputCheck.Int("Ban co muon them nguoi doc moi khong?\n1. co | 2. khong"))
                {
                    case 1:
                        readerManager.AddReader();
                        reader = readerManager.SearchByID(id);
                        break;
                    default:
                        return;
                }
            }
            Console.WriteLine("Nhap ngay muon va tra sach: ");
            BorrowDate d = new(
                InputCheck.GetDateOnly("Ngay muon sach: "),
                InputCheck.GetDateOnly("Ngay tra sach: ")
            );

            if (reader != null)
            {
                borrowRecords.Add(reader, d);
            }
        }

        public void UpdateRecord()
        {
            Console.WriteLine("Thay doi ngay muon tra");
            Reader? r = SearchRecord();
            if (r == null)
            {
                return;
            }
            ShowRecord(r);
            Console.WriteLine("Thay doi ngay muon tra");
            BorrowDate d = new(
                InputCheck.GetDateOnly("Ngay muon sach: "),
                InputCheck.GetDateOnly("Ngay tra sach: ")
            );
            borrowRecords[r] = d;
            Console.WriteLine("ngay muon tra da duoc cap nhat");
        }

        public void RemoveRecord()
        {
            Console.WriteLine("Xoa thong tin muon tra");
            Reader? r = SearchRecord();
            if (r == null)
            {
                return;
            }
            switch (InputCheck.Int("Ban co muon xoa thong tin nay?\n1. Xac nhan | 2. Tu choi"))
            {
                case 1:
                    borrowRecords.Remove(r);
                    Console.WriteLine("Thong tin da duoc xoa");
                    break;
                default:
                    Console.WriteLine("Thong tin khong duoc xoa");
                    break;
            }
        }

        public Reader? SearchRecord()
        {
            string id = InputCheck.String("Nhap ma so nguoi muon: ");
            Reader? find = borrowRecords.Keys.FirstOrDefault(r => r.ReaderID == id);
            if (find == null)
            {
                Console.WriteLine("Khong tim thay nguoi muon");
                return null;
            }
            return find;
        }

        #region show methods
        public void ShowRecord(Reader r)
        {
            Console.WriteLine($"{"Ho ten nguoi muon",-30} | {"Ngay muon",-15} | {"Ngay tra",-15} | {"So tien phat",-30}");
            Console.WriteLine(new string('-', 90));
            Console.WriteLine($"{r.Name,-30} | {borrowRecords[r].BorrowDay,-15} | {borrowRecords[r].ReturnDay,-15} | {CaculateFine(r),-30}");
            Console.WriteLine(new string('-', 90) + "\n");
        }

        public void ShowAllRecord()
        {
            Console.WriteLine($"{"Ho ten nguoi muon",-30} | {"Ngay muon",-15} | {"Ngay tra",-15} | {"So tien phat",-30}");
            Console.WriteLine(new string('-', 60));
            foreach (Reader r in borrowRecords.Keys)
            {
                Console.WriteLine($"{r.Name,-30} | {borrowRecords[r].BorrowDay,-15} | {borrowRecords[r].ReturnDay,-15} | {CaculateFine(r),-30}");
            }
            Console.WriteLine(new string('-', 60) + "\n");
        }
        #endregion

        public double CaculateFine(Reader r)
        {

            int numberOfDayLate = (DateTime.Today - borrowRecords[r].ReturnDay.ToDateTime(TimeOnly.MinValue)).Days;
            if (numberOfDayLate > 0)
            {
                return 5000 * numberOfDayLate;
            }
            else
            {
                return 0;
            }

        }

        #region file operations
        public void SaveToFile(string filePath = "borrow_records.txt")
        {
            try
            {
                using StreamWriter writer = new(filePath);
                foreach (KeyValuePair<Reader, BorrowDate> record in borrowRecords)
                {
                    writer.WriteLine($"{record.Key.ReaderID}|{record.Value.BorrowDay}|{record.Value.ReturnDay}");
                }
                Console.WriteLine($"Danh sach muon tra da duoc luu vao file: {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Loi khi luu file: {ex.Message}");
            }
        }

        public void LoadFromFile(string filePath = "borrow_records.txt")
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    Console.WriteLine($"File {filePath} khong ton tai.");
                    return;
                }

                // Dam bao danh sach doc gia da duoc tai
                if (readerManager.ReaderList.Count == 0)
                {
                    Console.WriteLine("Can tai danh sach doc gia truoc.");
                    readerManager.LoadFromFile();
                }

                borrowRecords.Clear();
                using StreamReader reader = new(filePath);
                string? line;
                int successCount = 0;
                int errorCount = 0;

                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split('|');
                    if (parts.Length == 3 && 
                        DateOnly.TryParse(parts[1], out DateOnly borrowDay) && 
                        DateOnly.TryParse(parts[2], out DateOnly returnDay))
                    {
                        string readerID = parts[0];
                        Reader? r = readerManager.SearchByID(readerID);
                        
                        if (r != null)
                        {
                            BorrowDate borrowDate = new(borrowDay, returnDay);
                            borrowRecords.Add(r, borrowDate);
                            successCount++;
                        }
                        else
                        {
                            errorCount++;
                            Console.WriteLine($"Khong tim thay doc gia co ID: {readerID}");
                        }
                    }
                }
                
                Console.WriteLine($"Da doc {successCount} ban ghi muon tra tu file: {filePath}");
                if (errorCount > 0)
                {
                    Console.WriteLine($"Co {errorCount} ban ghi khong hop le.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Loi khi doc file: {ex.Message}");
            }
        }
        #endregion
    }
}