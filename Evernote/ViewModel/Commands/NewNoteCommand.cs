using Evernote.Model;

namespace Evernote.ViewModel.Commands;

public class NewNoteCommand : ICommand
{
    public NotesVM VM { get; set; }
    public event EventHandler CanExecuteChanged;

    public NewNoteCommand(NotesVM vm)
    {
        VM = vm;
    }

    public bool CanExecute(object parameter)
    {
        Notebook selectedNotebook = (Notebook)parameter;
        if (selectedNotebook is not null)
            return true;
        return false;
    }

    public void Execute(object parameter)
    {
        Notebook selectedNotebook = (Notebook)parameter;
        VM.CreateNote(selectedNotebook.Id);
    }
}
