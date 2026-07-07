using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeterinaryClinic.Business.Abstract;
using VeterinaryClinic.DataAccess.Abstract;
using VeterinaryClinic.Entities.Models;
using VeterinaryClinic.Entities.Status;

namespace VeterinaryClinic.Business.Concrete
{
    public class ReportManager : IReportService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReportManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<DashboardReportDto> GetDashboardReportAsync()
        {
            var today = DateOnly.FromDateTime(DateTime.Today);
            var currentMonth = DateTime.Now.Month;
            var currentYear = DateTime.Now.Year;

            var appointments = await _unitOfWork.Appointments.ListAsync();
            var payments = await _unitOfWork.Payments.ListAsync();
            var animals = await _unitOfWork.Animals.ListAsync();

            var dto = new DashboardReportDto
            {
                DailyAppointmentsCount = appointments.Count(a => a.Date == today),
                DailyRevenue = payments.Where(p => p.PaymentDate.Date == DateTime.Today)
                                        .Sum(p => p.AmountPaid),

                MonthlyAppointmentCount = appointments.Count(a => a.Date.Month == currentMonth),
                MonthlyRevenue = payments.Where(p => p.PaymentDate.Month == currentMonth && p.PaymentDate.Year == currentYear)
                                            .Sum(p => p.AmountPaid),

                TotalAnimalsCount = animals.Count,

                TotalCreditCardCount = payments.Count(p => p.PaymentMethod == PaymentStatus.CreditCard),
                TotalBankTransferCount = payments.Count(p => p.PaymentMethod == PaymentStatus.BankTransfer),
                TotalCashCount = payments.Count(p => p.PaymentMethod == PaymentStatus.Cash),

                
                TotalAmountCreditCard = payments.Where(p => p.PaymentMethod == PaymentStatus.CreditCard).Sum(p => p.AmountPaid),
                TotalAmountBankTransfer = payments.Where(p => p.PaymentMethod == PaymentStatus.BankTransfer).Sum(p => p.AmountPaid),
                TotalAmountCash = payments.Where(p => p.PaymentMethod == PaymentStatus.Cash).Sum(p => p.AmountPaid),


            };

            return dto;

        }
    }
}
