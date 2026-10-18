namespace SRSInventory.Consts
{
    internal class DatabaseConsts
    {
        public const string DatabaseFilename = "srs_inventory.sqlite";

        public const SQLite.SQLiteOpenFlags Flags = SQLite.SQLiteOpenFlags.ReadWrite | SQLite.SQLiteOpenFlags.SharedCache;

        public static string DatabasePath => Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);
    }
}
