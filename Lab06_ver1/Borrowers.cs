/****************
Ma sinh vien: 22ITxxx
Ho ten: Nguyen Van A
****************/
// Nguoi muon co 2 loai: giang vien va tro giang

public interface IBorrower
{
    string BorrowerId { get; }
    string FullName { get; }
    int GetMaximumDevices(); // so thiet bi toi da trong 1 phieu
}

// Giang vien duoc muon toi da 3 thiet bi
public class Lecturer : IBorrower
{
    public string BorrowerId { get; set; }
    public string FullName { get; set; }

    public Lecturer(string id, string name)
    {
        BorrowerId = id;
        FullName = name;
    }

    public int GetMaximumDevices()
    {
        return 3;
    }
}

// Tro giang duoc muon toi da 2 thiet bi
public class TeachingAssistant : IBorrower
{
    public string BorrowerId { get; set; }
    public string FullName { get; set; }

    public TeachingAssistant(string id, string name)
    {
        BorrowerId = id;
        FullName = name;
    }

    public int GetMaximumDevices()
    {
        return 2;
    }
}
