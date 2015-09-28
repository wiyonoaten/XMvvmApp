namespace XMvvmApp.Mvvm
{
    public class Binding
    {
        protected object Connection { get; private set; }

        public Binding(object connection)
        {
            this.Connection = connection;
        }

        public virtual void Detach()
        {
            this.Connection = null;
        }
    }

    public class Binding<T> : Binding
        where T : class
    {
        public Binding(T connection)
            : base(connection)
        {
        }
    }
}
