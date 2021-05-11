using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Todo.App.Services
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
