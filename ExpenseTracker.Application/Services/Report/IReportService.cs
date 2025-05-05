using ExpenseTracker.Domain.Enums;

namespace ExpenseTracker.Application.Services.Report
{
    public interface IReportService
    {
        // 1- Personel kendi masraf detaylarini gordugu rapor
        // 2- Sirket gunluk,haftalik,aylik rapor
        // 3- Sirket personel bazli gunluk haftalik aylik rapor
        // 4- Sirket gunluk haftalik aylik onaylanan reddedilen rapor 


        // 1. Rapor modeli :
        // public DateTime CreatedDate { get; set; }
        //public decimal Amount { get; set; } // Expense miktari
        //public Currency Currency { get; set; } = Currency.TRY;
        //public ExpenseStatus ExpenseStatus { get; set; }
        //public string? RejectionReason { get; set; }
        //public string ExpenseCategoryName { get; set; }



        // 2. Rapor modeli;
        //public decimal Amount { get; set; }
        //public DateTime PaidDate { get; set; }
        //public string TransactionStatus { get; set; }


        // 3. Rapor modeli;
        //public decimal UserName { get; set; }
        //public decimal UserSurname { get; set; }
        //public decimal Amount { get; set; }
        //public DateTime PaidDate { get; set; }
        //public string TransactionStatus { get; set; }
        //public DateTime PaymentDate { get; set; }

        // 4. Rapor modeli;
        //public decimal Amount { get; set; }
        //public DateTime PaidDate { get; set; }
        //public string TransactionStatus { get; set; }

    }
}
