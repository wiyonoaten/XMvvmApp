namespace XMvvmApp.Mvvm
{
    public class Binding
    {
        private object _connection;

        public Binding(object connection)
        {
            _connection = connection;
        }

        public void Detach()
        {
            _connection = null;
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
