using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeterinaryClinic.Entities.Models
{
    public class DashboardReportDto
    {
        public int DailyAppointmentsCount { get; set; }
        public decimal DailyRevenue { get; set; }

        public int MonthlyAppointmentCount { get; set; }
        public decimal MonthlyRevenue { get; set; }

        public int TotalAnimalsCount { get; set; }

        public int TotalCreditCardCount { get; set; }
        public int TotalBankTransferCount { get; set; }
        public int TotalCashCount { get; set; }

        public decimal TotalAmountCreditCard { get; set; }
        public decimal TotalAmountBankTransfer { get; set; }
        public decimal TotalAmountCash { get; set; }

    }
}
