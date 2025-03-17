namespace Assignment_302_QuanLyThuVien
{
    public class ReaderManager
    {
        List<Reader> readerList = new();

        public List<Reader> ReaderList { get => readerList; }

        public void AddReader()
        {
            Console.WriteLine("Them thong tin nguoi doc");
            Console.WriteLine("Nhap thong tin nguoi muon");
            Reader r = new(
                InputCheck.String("Ma so: "),
                InputCheck.String("Ho va ten: "),
                InputCheck.String("Dia chi: "),
                InputCheck.Int("So dien thoai: ")
            );
            if (readerList.Any(rd => rd.ReaderID == r.ReaderID))
            {
                Console.WriteLine("Nguoi doc da ton tai");
                return;
            }
            readerList.Add(r);
        }

        public void UpdateReader()
        {
            Console.WriteLine("Cap nhat thong tin nguoi doc");
            Reader? r = SearchByID();
            if (r == null)
            {
                return;
            }
            bool isDone = false;
            while (!isDone)
            {
                ShowReader(r);
                switch (InputCheck.Int("Chon thong tin can chinh sua \n1. Ho va ten | 2. Dia chi | 3. So dien thoai | 0. Thoat"))
                {
                    case 1:
                        Console.WriteLine("Thay doi ho va ten");
                        r.Name = InputCheck.String("Nhap ho va ten moi");
                        Console.WriteLine("Ho va ten da duoc cap nhat");
                        ShowReader(r);
                        break;

                    case 2:
                        Console.WriteLine("Thay doi dai chi");
                        r.Address = InputCheck.String("Nhap dia chi moi");
                        Console.WriteLine("Dia chi da duoc cap nhat");
                        ShowReader(r);
                        break;

                    case 3:
                        Console.WriteLine("Thay doi so dien thoai");
                        r.PhoneNumber = InputCheck.Int("Nhap so dien thoai moi");
                        Console.WriteLine("So dien thoai da duoc cap nhat");
                        ShowReader(r);
                        break;

                    case 0:
                        isDone = true;
                        break;

                    default:
                        Console.WriteLine("Nhap khong hop le");
                        break;
                }
            }
        }

        public void RemoveReader()
        {
            Console.WriteLine("Xoa nguoi doc");
            Reader? r = SearchByID();
            if (r == null)
            {
                return;
            }
            switch (InputCheck.Int("Ban co muon xoa nguoi nay?\n1. Xac nhan | 2. Tu choi"))
            {
                case 1:
                readerList.Remove(r);
                    Console.WriteLine("Nguoi doc da duoc xoa");
                    break;
                default:
                    Console.WriteLine("Nguoi doc khong duoc xoa");
                    break;
            }
        }

        public void ShowAllReader()
        {
            ShowReader(readerList);
        }

        public void ShowReader(Reader r)
        {
            Console.WriteLine($"{"ReaderID",-10} | {"Name",-30} | {"Address",-50} | {"PhoneNumber",-20}");
            Console.WriteLine(new string('-', 110));
            Console.WriteLine($"{r.ReaderID,-10} | {r.Name,-30} | {r.Address,-50} | {r.PhoneNumber,-20}");
            Console.WriteLine(new string('-', 110) + "\n");
        }
        public void ShowReader(List<Reader> list)
        {
            Console.WriteLine($"{"ReaderID",-10} | {"Name",-30} | {"Address",-50} | {"PhoneNumber",-20}");
            Console.WriteLine(new string('-', 110));
            foreach (Reader r in list)
            {
                Console.WriteLine($"{r.ReaderID,-10} | {r.Name,-30} | {r.Address,-50} | {r.PhoneNumber,-20}");
            }
            Console.WriteLine(new string('-', 110) + "\n");
        }

        public Reader? SearchByID()
        {
            Console.WriteLine("Tim theo ma nguoi doc");
            Reader? find = readerList.Find(r => r.ReaderID == InputCheck.String("Nhap ma nguoi doc can tim: "));
            if (find == null)
            {
                Console.WriteLine("khong tim thay nguoi doc");
                return null;
            }
            return find;
        }

        public Reader? SearchByID(string id)
        {
            Reader? find = readerList.Find(r => r.ReaderID == id);
            if (find == null)
            {
                Console.WriteLine("khong tim thay nguoi doc");
                return null;
            }
            return find;
        }

        public List<Reader> SearchByName()
        {
            Console.WriteLine("Tim kiem theo ten nguoi doc");
            string searchName = InputCheck.String("Nhap ten nguoi doc can tim: ");
            
            // Tim tat ca doc gia co ten chua chuoi tim kiem (khong phan biet hoa thuong)
            List<Reader> foundReaders = readerList.FindAll(r => 
                r.Name.ToLower().Contains(searchName.ToLower()));
                
            if (foundReaders.Count == 0)
            {
                Console.WriteLine("Khong tim thay nguoi doc nao co ten chua: " + searchName);
            }
            else
            {
                Console.WriteLine($"Da tim thay {foundReaders.Count} nguoi doc co ten chua: {searchName}");
            }
            
            return foundReaders;
        }

        #region file operations
        public void SaveToFile(string filePath = "readers.txt")
        {
            try
            {
                using StreamWriter writer = new(filePath);
                foreach (Reader reader in readerList)
                {
                    writer.WriteLine($"{reader.ReaderID}|{reader.Name}|{reader.Address}|{reader.PhoneNumber}");
                }
                Console.WriteLine($"Danh sach doc gia da duoc luu vao file: {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Loi khi luu file: {ex.Message}");
            }
        }

        public void LoadFromFile(string filePath = "readers.txt")
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    Console.WriteLine($"File {filePath} khong ton tai.");
                    return;
                }

                readerList.Clear();
                using StreamReader reader = new(filePath);
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split('|');
                    if (parts.Length == 4 && int.TryParse(parts[3], out int phoneNumber))
                    {
                        Reader r = new(
                            parts[0],   // ReaderID
                            parts[1],   // Name
                            parts[2],   // Address
                            phoneNumber // PhoneNumber
                        );
                        readerList.Add(r);
                    }
                }
                Console.WriteLine($"Da doc {readerList.Count} doc gia tu file: {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Loi khi doc file: {ex.Message}");
            }
        }
        #endregion
    }
}