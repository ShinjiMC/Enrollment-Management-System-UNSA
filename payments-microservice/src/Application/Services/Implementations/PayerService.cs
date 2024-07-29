namespace PaymentsMicroservice.Application.Services.Implementations
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using PaymentsMicroservice.Application.Services.Interfaces;
    using PaymentsMicroservice.Domain.Entities;
    using PaymentsMicroservice.Domain.Repositories;

    public class PayerService : IPayerService
    {
        private readonly IPayerRepository _payerRepository;

        public PayerService(IPayerRepository payerRepository)
        {
            _payerRepository = payerRepository;
        }

        public async Task<List<Payer>> GetPayers()
        {
            return await _payerRepository.GetPayers();
        }

        public async Task<Payer> GetPayerById(string payerId)
        {
            var result =  await _payerRepository.GetPayerById(payerId);
            Console.WriteLine(result);
            return result;
        }

        public async Task<bool> SavePayer(Payer payer)
        {
            return await _payerRepository.SavePayer(payer);
        }
    }
}