namespace GunPrototype.Common
{
    public interface IFactory<T>
    {
        public T Create();
    }
}