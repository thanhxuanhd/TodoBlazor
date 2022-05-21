namespace Todo.App.Services.Base
{
    public class BaseDataService
    {
        protected IClient _client;

        public BaseDataService(IClient client)
        {
            _client = client;
        }
    }
}