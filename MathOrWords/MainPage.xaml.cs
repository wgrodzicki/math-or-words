namespace MathOrWords
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
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
            Navigation.PushAsync(new ScoresPage());
        }
    }
}
