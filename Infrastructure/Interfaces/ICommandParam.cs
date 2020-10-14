using Infrastructure.Entity;

namespace Infrastructure.Interfaces
{
    /// <summary>
    /// To locate command parameters
    /// </summary>
    /// <createdby>
    /// jebril mohammad
    /// </createdby>
    public interface ICommandParam<T> where T : class, IEntity, new()
    {
    }
}