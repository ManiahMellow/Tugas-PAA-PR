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
            string query = string.Format(@"SELECT id_murid, nama_murid, alamat_rumah, pelajaran_favorit, kelas FROM users.person");
            sqlDBHelper db = new sqlDBHelper(this.__constr);

            try
            {
                NpgsqlCommand cmd = db.getNpgsqlCommand(query);
                NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new Person()
                    {
                        id_murid = int.Parse(reader["id_murid"].ToString()),
                        nama_murid = reader["nama_murid"].ToString(),
                        alamat_rumah = reader["alamat_rumah"].ToString(),
                        pelajaran_favorit = reader["pelajaran_favorit"].ToString(),
                        kelas = int.Parse(reader["kelas"].ToString()),
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
            string query = @"SELECT id_murid, nama_murid, alamat_rumah, pelajaran_favorit, kelas FROM users.person WHERE id_murid = @id";

            NpgsqlConnection conn = new NpgsqlConnection(__constr);
            conn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@id", id);
            NpgsqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                person = new Person
                {
                    id_murid = int.Parse(reader["id_murid"].ToString()),
                    nama_murid = reader["nama_murid"].ToString(),
                    alamat_rumah = reader["alamat_rumah"].ToString(),
                    pelajaran_favorit = reader["pelajaran_favorit"].ToString(),
                    kelas = int.Parse(reader["kelas"].ToString()),
                };
            }

            reader.Close();
            cmd.Dispose();
            conn.Close();

            return person;
        }

        public void AddPerson(PersonInsert person)
        {
            string query = @"INSERT INTO users.person (nama_murid, alamat_rumah, pelajaran_favorit, kelas, username, password) VALUES (@nama_murid, @alamat_rumah, @pelajaran_favorit, @kelas, @username, @password)";

            NpgsqlConnection conn = new NpgsqlConnection(__constr);
            conn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@nama_murid", person.nama_murid);
            cmd.Parameters.AddWithValue("@alamat_rumah", person.alamat_rumah);
            cmd.Parameters.AddWithValue("@pelajaran_favorit", person.pelajaran_favorit);
            cmd.Parameters.AddWithValue("@kelas", person.kelas);
            cmd.Parameters.AddWithValue("@username", person.username);
            cmd.Parameters.AddWithValue("@password", person.password);
            cmd.ExecuteNonQuery();

            cmd.Dispose();
            conn.Close();
        }

        public void UpdatePerson(PersonInsert person,int iduser)
        {
            string query = @"UPDATE users.person SET nama_murid = @nama_murid, alamat_rumah = @alamat_rumah, pelajaran_favorit = @pelajaran_favorit, kelas = @kelas WHERE id_murid = @id";

            NpgsqlConnection conn = new NpgsqlConnection(__constr);
            conn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@id", iduser);
            cmd.Parameters.AddWithValue("@nama_murid", person.nama_murid);
            cmd.Parameters.AddWithValue("@alamat_rumah", person.alamat_rumah);
            cmd.Parameters.AddWithValue("@pelajaran_favorit", person.pelajaran_favorit);
            cmd.Parameters.AddWithValue("@kelas", person.kelas);
            cmd.ExecuteNonQuery();

            cmd.Dispose();
            conn.Close();
        }

        public void DeletePerson(int id)
        {
            string query = @"DELETE FROM users.person WHERE id_murid = @id";

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