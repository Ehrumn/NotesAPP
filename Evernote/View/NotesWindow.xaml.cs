using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using System.Windows.Documents;

namespace Evernote.View;
public partial class NotesWindow : Window
{
    public NotesWindow()
    {
        InitializeComponent();
    }

    private void MenuItem_Click(object sender, RoutedEventArgs e)
    {
        Application.Current.Shutdown();
    }

    private async void SpeechButton_Click(object sender, RoutedEventArgs e)
    {
        string region = "brazilsouth";
        string key = "3c365d8e9154412195972be0bb5ca981";

        var speechConfig = SpeechConfig.FromSubscription(key, region);

        using (var audioConfig = AudioConfig.FromDefaultMicrophoneInput())
        {
            using (var recognizer = new SpeechRecognizer(speechConfig, audioConfig))
            {
                var result = await recognizer.RecognizeOnceAsync();
                contentRichTextBox.Document.Blocks.Add(new Paragraph(new Run(result.Text)));
            }
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
