using DataDemo.Data.Models;
using Microsoft.Data.Sqlite;
namespace DataDemo.Data.Repositories;

public class MajorRepository
{
    private readonly DatabaseService db;

    public MajorRepository(DatabaseService db)
    {
        this.db = db;
    }

    public List<Major> GetAll()
    {
        List<Major> items = new List<Major>();
        using var conn = new SqliteConnection(db.ConnectionString);
        conn.Open();
        string sql = "SELECT Id, Title FROM Major ORDER BY Title;";
        using var cmd = new SqliteCommand(sql, conn);
        using var reader = cmd.ExecuteReader(); 
        while (reader.Read())
        {
            Major Major = new Major
            {
                Id = reader.GetInt32(0),
                Title = reader.GetString(1)
            }; 
            items.Add(Major);
        }
        return items;
    }

    public Major? GetById(int id)
    {
        using var conn = new SqliteConnection(db.ConnectionString);
        conn.Open();
        string sql = "SELECT Id, Title FROM Major WHERE Id = $id;";
        using var cmd = new SqliteCommand(sql, conn);
        cmd.Parameters.AddWithValue("$id", id);
        using var reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            return new Major
            {
                Id = reader.GetInt32(0),
                Title = reader.GetString(1)
            };
        }
        return null;
    }

    public void Add(Major Major)
    {
        using var conn = new SqliteConnection(db.ConnectionString);
        conn.Open();
        string sql = "INSERT INTO Major (Title) VALUES ($title);";
        using var cmd = new SqliteCommand(sql, conn);
        cmd.Parameters.AddWithValue("$title", Major.Title);
        cmd.ExecuteNonQuery();
    }
    public void Update(Major Major)
    {
        using var conn = new SqliteConnection(db.ConnectionString);
        conn.Open(); 
        string sql = "UPDATE Major SET Title = $title WHERE Id = $id;";
        using var cmd = new SqliteCommand(sql, conn);
        cmd.Parameters.AddWithValue("$title", Major.Title);
        cmd.Parameters.AddWithValue("$id", Major.Id);
        cmd.ExecuteNonQuery();
    }

    public void Delete(int id)
    {
        using var conn = new SqliteConnection(db.ConnectionString);
        conn.Open();
        string sql = "DELETE FROM Major WHERE Id = $id;";
        using var cmd = new SqliteCommand(sql, conn);
        cmd.Parameters.AddWithValue("$id", id);
        cmd.ExecuteNonQuery();
    }
}