using System.Speech.Recognition;
using System.Windows.Documents;

namespace Evernote.View;
public partial class NotesWindow : Window
{
    SpeechRecognitionEngine recoginzer;

    public NotesWindow()
    {
        InitializeComponent();

        var currentCulture = (from r in SpeechRecognitionEngine.InstalledRecognizers() 
                              where r.Culture.Equals(Thread.CurrentThread.CurrentCulture)
                              select r).FirstOrDefault();
        recoginzer = new SpeechRecognitionEngine(currentCulture);

        GrammarBuilder builder = new GrammarBuilder();
        builder.AppendDictation();
        Grammar grammar = new Grammar(builder);

        recoginzer.LoadGrammar(grammar);
        recoginzer.SetInputToDefaultAudioDevice();
        recoginzer.SpeechRecognized+=Recoginzer_SpeechRecognized;
    }

    private void Recoginzer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
    {
        string recoginzedText= e.Result.Text;

        contentRichTextBox.Document.Blocks.Add(new Paragraph(new Run(recoginzedText)));
    }

    private void MenuItem_Click(object sender, RoutedEventArgs e)
    {
        Application.Current.Shutdown();
    }

    bool isRecognizing = false;
    private async void SpeechButton_Click(object sender, RoutedEventArgs e)
    {
        if (!isRecognizing)
        {
            recoginzer.RecognizeAsync(RecognizeMode.Multiple);
            isRecognizing = true;
        }
        else
        {
            recoginzer.RecognizeAsyncStop();
            isRecognizing = false;
        }
    }

    private void contentRichTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
    {
        int ammountOfCharacters = (new TextRange(contentRichTextBox.Document.ContentStart, contentRichTextBox.Document.ContentEnd)).Text.Length;

        statusTextBlock.Text = $"Document length: {ammountOfCharacters} characters";
    }

    private void boldButton_Click(object sender, RoutedEventArgs e)
    {
        contentRichTextBox.Selection.ApplyPropertyValue(Inline.FontWeightProperty, FontWeights.Bold);
    }
}
