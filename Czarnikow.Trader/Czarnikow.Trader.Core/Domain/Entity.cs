namespace Czarnikow.Trader.Core.Domain
{
    public abstract class Entity<T>
    {
        protected ValidationErrors validationErrorCollection;

        public Entity()
        {
            this.validationErrorCollection = new ValidationErrors();
        }

        public T Id
        {
            get; protected set;
        }

        public virtual void Validate() { }
    }
}