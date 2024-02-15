using MathOrWords.Models;

namespace MathOrWords
{
    public partial class MainPage : ContentPage
    {
        public static List<Game> Games { get; set; }

        public MainPage()
        {
            InitializeComponent();
            Games = new List<Game>();
        }

        private void OnMathGameChosen(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MathGamePage()); // Asynchronous execution
        }

        private void OnWordsGameChosen(object sender, EventArgs e)
        {
            Navigation.PushAsync(new WordsGamePage());
        }

        private void OnScoresChosen(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ScoresPage(Games));
        }
    }
}
