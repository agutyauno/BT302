namespace Assignment_302_QuanLyThuVien
{
    public class Reader
    {
        #region fields
            string readerID;
            string name;
            string address;
            int phoneNumber;
        #endregion

        #region properties
            public string ReaderID { get => readerID; set => readerID = value; }
            public string Name { get => name; set => name = value; }
            public string Address { get => address; set => address = value; }
            public int PhoneNumber { get => phoneNumber; set => phoneNumber = value; }
        #endregion

        public Reader(string readerID, string name, string address, int phoneNumber)
        {
            this.readerID = readerID;
            this.name = name;
            this.address = address;
            this.phoneNumber = phoneNumber;
        }
    }
}