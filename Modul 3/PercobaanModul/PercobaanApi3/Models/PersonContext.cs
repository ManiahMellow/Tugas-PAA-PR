using Npgsql;
using PercobaanApi3.Helpers;

namespace PercobaanApi3.Models
{
    public class PersonContext
    {
        private string __constr;
        private string __ErrorMsg;
        public PersonContext(string pConstr) 
        { 
            __constr = pConstr;
        }  

        public List<Person> ListPerson()
        {
            List<Person> list1 = new List<Person>();
            string query = string.Format(@"SELECT id_person, nama, alamat, email, username, password FROM users.person");
            sqlDBHelpers db = new sqlDBHelpers(this.__constr);

            try
            {
                NpgsqlCommand cmd = db.getNpgsqlCommand(query);
                NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list1.Add(new Person() 
                    {
                        id_person = int.Parse(reader["id_person"].ToString()),
                        nama = reader["nama"].ToString(),
                        alamat = reader["alamat"].ToString(),
                        email = reader["email"].ToString(),
                        username = reader["username"].ToString(),
                        password = reader["password"].ToString()
                    });
                }
                cmd.Dispose();
                db.closeConnection();
            }
            catch (Exception ex)
            {
                __ErrorMsg = ex.Message;
            }

            return list1;
        }
    }
}
