using Infrastructure.Entity;

namespace Infrastructure.Interfaces
{
    /// <summary>
    /// Use ICommand to locate http requset data from the APIS as command all command should have
    /// Execute function
    /// </summary>
    /// <createdby>
    /// Jebril mohammad
    /// </createdby>
    internal interface ICommand<T>
    {
        public  T Execute(ICommandParam param );
    }

    internal interface ICommandExt<T>
    {
        public T Execute(ICommandParam param, dynamic paramExt);
    }
}