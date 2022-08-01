namespace Evernote.Model;

public class Notebook
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    [Indexed]
    public int UserID { get; set; }
    [MaxLength(50)]
    public string Name { get; set; }
}
