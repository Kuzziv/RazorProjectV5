namespace RazorProjectV5.Services.Interfaces
{
    public interface IJsonFileService<T>
    {
        Task SaveAsync(IEnumerable<T> objects);
        Task<IEnumerable<T>> GetAllAsync();
    }
}
