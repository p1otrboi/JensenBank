namespace JensenBank.Infrastructure.Interfaces
{
    public interface IDispositionRepo
    {
        public Task<int> AddAsync(int customerId, int accountId, string type);
    }
}
