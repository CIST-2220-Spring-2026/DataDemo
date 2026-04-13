namespace DataDemo.Data
{
    using Microsoft.Data.Sqlite;
    public class DatabaseService
    {
        public string ConnectionString { get; }
        public DatabaseService()
        {
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, AppSettings.DatabaseFileName);
            ConnectionString = $"Data Source={dbPath}"; 
            InitializeDatabase();
            SeedSampleData();
        }
        private void InitializeDatabase()
        {
            using var conn = new SqliteConnection(ConnectionString);
            conn.Open();
            string sql = @"
                CREATE TABLE IF NOT EXISTS Campus (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT
                )
                ;CREATE TABLE IF NOT EXISTS Course (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL
                )
                ;CREATE TABLE IF NOT EXISTS Major (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Title TEXT
                )
                ;CREATE TABLE IF NOT EXISTS Student (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    FirstName TEXT NULL,
                    LastName TEXT NULL,
                    StudentNumber TEXT NULL,
                    MajorId INTEGER NULL,
                    FOREIGN KEY (MajorId) REFERENCES Major(Id) ON DELETE SET NULL
                );";
            using var cmd = new SqliteCommand(sql, conn);
            cmd.ExecuteNonQuery();
        }
        private void SeedSampleData()
        {
            using var conn = new SqliteConnection(ConnectionString);
            conn.Open(); 
            string sql = @"
                INSERT INTO Campus (Name)
                SELECT 'Main Campus'
                WHERE NOT EXISTS (SELECT 1 FROM Campus WHERE Name = 'Main Campus');

                INSERT INTO Course (Name)
                SELECT 'C# Programming'
                WHERE NOT EXISTS (SELECT 1 FROM Course WHERE Name = 'C# Programming');

                INSERT INTO Major (Title)
                SELECT 'Computer Science'
                WHERE NOT EXISTS (SELECT 1 FROM Major WHERE Title = 'Computer Science');
                "; 

            using var cmd = new SqliteCommand(sql, conn);
            cmd.ExecuteNonQuery();
        }
    }
}
