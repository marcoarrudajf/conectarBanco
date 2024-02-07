using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConectarBD
{
    public class UserRepository
    {
        private readonly IDatabaseConnection _databaseConnection;

        public UserRepository(IDatabaseConnection databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }

        public void GetAllUsers()
        {
            using (var connection = _databaseConnection.Connection)
            {
                string connectionString = "Server=localhost;Database=cadastropessoa;Uid=root;Pwd=mefaj1737;";
            }
        }
    }

}
