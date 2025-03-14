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
    }
}