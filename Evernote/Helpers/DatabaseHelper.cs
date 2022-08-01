namespace Evernote.Helpers;

public class DatabaseHelper
{
    private static string dbFile = Path.Combine(Environment.CurrentDirectory, "notesDB.db3");

    public static bool Insert<T>(T item)
    {
        bool result = false;

        using (SQLiteConnection conn = new SQLiteConnection(dbFile))
        {
            conn.CreateTable<T>();

            if (conn.Insert(item) > 0)
                result = true;
        }

        return result;
    }

    public static bool Update<T>(T item)
    {
        bool result = false;

        using (SQLiteConnection conn = new SQLiteConnection(dbFile))
        {
            conn.CreateTable<T>();

            if (conn.Update(item) > 0)
                result = true;
        }

        return result;
    }

    public static bool Delete<T>(T item)
    {
        bool result = false;

        using (SQLiteConnection conn = new SQLiteConnection(dbFile))
        {
            conn.CreateTable<T>();

            if (conn.Delete(item) > 0)
                result = true;
        }

        return result;
    }

    public static List<T> Read<T>() where T : new()
    {
        List<T> items;

        using (SQLiteConnection conn = new SQLiteConnection(dbFile))
        {
            conn.CreateTable<T>();

            items = conn.Table<T>().ToList();
        }

        return items;
    }
}
