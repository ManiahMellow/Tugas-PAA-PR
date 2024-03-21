using Npgsql;
using PercobaanApi1.Helpers;


namespace PercobaanApi1.Models
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
            List<Person> list = new List<Person>();
            string query = string.Format(@"SELECT id_person, nama, alamat, email FROM users.person");
            sqlDBHelper db = new sqlDBHelper(this.__constr);

            try
            {
                NpgsqlCommand cmd = db.getNpgsqlCommand(query);
                NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new Person()
                    {
                        id_person = int.Parse(reader["id_person"].ToString()),
                        nama = reader["nama"].ToString(),
                        alamat = reader["alamat"].ToString(),
                        email = reader["email"].ToString(),
                    });
                }

                cmd.Dispose();
                db.closeConnection();
            }

            catch (Exception ex)
            {
                __ErrorMsg = ex.Message;
            }

            return list;
        }

        public Person GetPerson(int id)
        {
            Person person = null;
            string query = @"SELECT id_person, nama, alamat, email FROM users.person WHERE id_person = @id";

            NpgsqlConnection conn = new NpgsqlConnection(__constr);
            conn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@id", id);
            NpgsqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                person = new Person
                {
                    id_person = int.Parse(reader["id_person"].ToString()),
                    nama = reader["nama"].ToString(),
                    alamat = reader["alamat"].ToString(),
                    email = reader["email"].ToString(),
                };
            }

            reader.Close();
            cmd.Dispose();
            conn.Close();

            return person;
        }

        public void AddPerson(PersonInsert person)
        {
            string query = @"INSERT INTO users.person (nama, alamat, email) VALUES (@nama, @alamat, @email)";

            NpgsqlConnection conn = new NpgsqlConnection(__constr);
            conn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@nama", person.nama);
            cmd.Parameters.AddWithValue("@alamat", person.alamat);
            cmd.Parameters.AddWithValue("@email", person.email);
            cmd.ExecuteNonQuery();

            cmd.Dispose();
            conn.Close();
        }

        public void UpdatePerson(PersonInsert person,int iduser)
        {
            string query = @"UPDATE users.person SET nama = @nama, alamat = @alamat, email = @email WHERE id_person = @id";

            NpgsqlConnection conn = new NpgsqlConnection(__constr);
            conn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@id", iduser);
            cmd.Parameters.AddWithValue("@nama", person.nama);
            cmd.Parameters.AddWithValue("@alamat", person.alamat);
            cmd.Parameters.AddWithValue("@email", person.email);
            cmd.ExecuteNonQuery();

            cmd.Dispose();
            conn.Close();
        }

        public void DeletePerson(int id)
        {
            string query = @"DELETE FROM users.person WHERE id_person = @id";

            NpgsqlConnection conn = new NpgsqlConnection(__constr);
            conn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();

            cmd.Dispose();
            conn.Close();
        }

    }
}