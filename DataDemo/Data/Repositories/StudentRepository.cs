using DataDemo.Data.Models;
using Microsoft.Data.Sqlite;

namespace DataDemo.Data.Repositories
{
    public class StudentRepository
    {
        private readonly DatabaseService db;

        public StudentRepository(DatabaseService db)
        {
            this.db = db;
        }

        public List<Student> GetAll()
        {
            List<Student> items = new List<Student>();
            using var conn = new SqliteConnection(db.ConnectionString);
            conn.Open();

            string sql = "SELECT Id, FirstName, LastName, StudentNumber, MajorId FROM Student ORDER BY LastName;";
            using var cmd = new SqliteCommand(sql, conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Student student = new Student
                {
                    Id = reader.GetInt32(0),
                    FirstName = reader.GetString(1),
                    LastName = reader.GetString(2),
                    StudentNumber = reader.GetString(3),
                    MajorId = reader.GetInt32(4),
                };
                items.Add(student);
            }

            return items;
        }

        public Student? GetById(int id)
        {
            using var conn = new SqliteConnection(db.ConnectionString);
            conn.Open();

            string sql = "SELECT Id, FirstName, LastName, StudentNumber, MajorId FROM Student WHERE Id = $id;";
            using var cmd = new SqliteCommand(sql, conn);
            cmd.Parameters.AddWithValue("$id", id);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Student
                {
                    Id = reader.GetInt32(0),
                    FirstName = reader.GetString(1),
                    LastName = reader.GetString(2),
                    StudentNumber = reader.GetString(3),
                    MajorId = reader.GetInt32(4),
                };
            }

            return null;
        }

        public void Add(Student student)
        {
            using var conn = new SqliteConnection(db.ConnectionString);
            conn.Open();

            string sql = @"INSERT INTO Student
                (FirstName, LastName, StudentNumber, MajorId)
                VALUES
                ($FirstName, $LastName, $StudentNumber, $MajorId);";

            using var cmd = new SqliteCommand(sql, conn);
            cmd.Parameters.AddWithValue("$FirstName", (object?)student.FirstName ?? DBNull.Value);
            cmd.Parameters.AddWithValue("$LastName", (object?)student.LastName ?? DBNull.Value);
            cmd.Parameters.AddWithValue("$StudentNumber", (object?)student.StudentNumber ?? DBNull.Value);
            cmd.Parameters.AddWithValue("$MajorId", (object?)student.MajorId ?? DBNull.Value);

            cmd.ExecuteNonQuery();
        }

        public void Update(Student student)
        {
            using var conn = new SqliteConnection(db.ConnectionString);
            conn.Open();

            string sql = @"UPDATE Student
                SET FirstName = $FirstName,
                    LastName = $LastName,
                    StudentNumber = $StudentNumber,
                    MajorId = $MajorId
                WHERE Id = $id;";

            using var cmd = new SqliteCommand(sql, conn);
            cmd.Parameters.AddWithValue("$FirstName", (object?)student.FirstName ?? DBNull.Value);
            cmd.Parameters.AddWithValue("$LastName", (object?)student.LastName ?? DBNull.Value);
            cmd.Parameters.AddWithValue("$StudentNumber", (object?)student.StudentNumber ?? DBNull.Value);
            cmd.Parameters.AddWithValue("$MajorId", (object?)student.MajorId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("$id", student.Id);

            cmd.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            using var conn = new SqliteConnection(db.ConnectionString);
            conn.Open();

            string sql = "DELETE FROM Student WHERE Id = $id;";
            using var cmd = new SqliteCommand(sql, conn);
            cmd.Parameters.AddWithValue("$id", id);

            cmd.ExecuteNonQuery();
        }
    }
}