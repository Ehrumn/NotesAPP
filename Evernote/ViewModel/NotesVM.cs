using Evernote.Helpers;
using Evernote.Model;
using Evernote.ViewModel.Commands;

namespace Evernote.ViewModel;

public class NotesVM : INotifyPropertyChanged
{
    public ObservableCollection<Notebook> Notebooks { get; set; }

    private Notebook selectedNotebook;


    public Notebook SelectedNotebook
    {
        get { return selectedNotebook; }
        set
        {
            selectedNotebook = value;
            OnPropertyChanged("SelectedNotebook");
            GetNotes();
        }
    }

    public ObservableCollection<Note> Notes { get; set; }
    public NewNotebookCommand NewNotebookCommand { get; set; }
    public NewNoteCommand NewNoteCommand { get; set; }

    public event PropertyChangedEventHandler PropertyChanged;

    public NotesVM()
    {
        NewNotebookCommand = new NewNotebookCommand(this);
        NewNoteCommand = new NewNoteCommand(this);

        Notebooks = new ObservableCollection<Notebook>();
        Notes = new ObservableCollection<Note>();

        GetNotebooks();
    }

    public void CreateNotebook()
    {
        Notebook newNotebook = new Notebook()
        {
            Name = "New Notebook"
        };

        DatabaseHelper.Insert(newNotebook);

        GetNotebooks();
    }

    public void CreateNote(int notebookId)
    {
        Note newNote = new Note()
        {
            NotebookId = notebookId,
            CreateAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            Title = $"Note for {DateTime.Now.ToString("G")}"
        };

        DatabaseHelper.Insert(newNote);
        GetNotes();
    }

    private void GetNotebooks()
    {
        var notebooks = DatabaseHelper.Read<Notebook>();
        Notebooks.Clear();

        foreach (Notebook notebook in notebooks)
            Notebooks.Add(notebook);
    }

    private void GetNotes()
    {
        if (SelectedNotebook is not null)
        {
            IEnumerable<Note> notes = DatabaseHelper.Read<Note>().Where(n => n.NotebookId == SelectedNotebook.Id);
            Notes.Clear();

            foreach (Note note in notes)
                Notes.Add(note);
        }
    }
    private void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
