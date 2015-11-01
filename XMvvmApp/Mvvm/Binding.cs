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
        protected new T Connection { get { return base.Connection as T; } }

        public Binding(T connection)
            : base(connection)
        {
        }
    }
}
