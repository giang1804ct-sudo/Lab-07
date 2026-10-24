/***************
Ma sinh vien: 202418889
Ho ten: Dang Thi Huong Giang
Bai 04 - He thong tinh luong va thuong nhan su
***************/
using System;

namespace PayrollSystem
{
    public sealed class BonusRecord
    {
        public decimal Amount { get; }
        public string? Reason { get; }
        public string Method { get; }
        public DateTime CreatedAt { get; }

        public BonusRecord(decimal amount, string? reason, string method)
        {
            Amount = amount;
            Reason = reason;
            Method = method;
            CreatedAt = DateTime.Now;
        }

        public override string ToString()
            => $"{CreatedAt:yyyy-MM-dd HH:mm} | {Method} | +{Amount:N0} | {Reason ?? "(khong ly do)"}";
    }
}

