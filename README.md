# **Math or Words**

This is "Math or Words", my first project made with .NET MAUI. It's a desktop application for Windows that lets you play a game to test your skills with either math or words. You can download a sideloading version of the app (Windows build) [here](https://www.dropbox.com/scl/fi/8oi39g1mn134dh0fb34zm/MathOrWords.zip?rlkey=vm8q9gw4qjzs3nwuyb47i9xr3&st=azg4kz26&dl=0).

![Menu sample](MathOrWords/Resources/Images/main_menu.PNG)

## **Technical side of things**

The application is written with C# and XAML, using the .NET MAUI framework and SQLite for database management. It also consumes the Wikipedia API, making use of the Json.NET framework.

## **Features**

#### Math mode

Math mode consists in solving randomly generated equations (addition, subtraction, multiplication, division).

![Math sample](MathOrWords/Resources/Images/math_game.PNG)

#### Words mode

Words mode consists in forming words within the given constraints (inclusion/exclusion of letters, specific letter at the beginning/at the end of the word) that fit into a specified category (e.g. plants, tools, foods, etc.).

![Words sample](MathOrWords/Resources/Images/words_game.PNG)

In addition I also implemented:

- score and attempts counter
- score saving and browsing
- option to delete selected scores

## **How to play?**

Choose game mode (math or words) and game variant (in the case of math). Then try to solve the given equation (math) or find a word that matches the given constraints (words). You have 3 attempts each time you start a new game. Head to the scores page in order to browse your previous scores.

## **Main challenges**

When designing and implementing the project I stumbled upon several challenges:

- _MAUI framework_. It was my first encounter with the .NET MAUI framework and XAML, so it took me a while to get a grasp of the project structure and how the pages work with the code-behind pattern.

- _Database_. Previously I dealt mostly with raw SQL, so carrying out queries purely with C# and mapping a table to a class was something new to me. I also struggled a bit with finding a proper way to indicate the target folder to store the database.

- _Timed events_. I wanted to implement a slight "suspension" when the game was over and first thought of a simple timer. But then I found out about async methods and tasks, so I finally opted for a Delay() method without stopping the whole app, which was a good solution to the problem.

- _Consuming the Wikipedia API_. Due to the impredictible nature of player's answers in the Words mode, I struggled with the appropriate validation mechanism. Words are - of course - checked for spelling according to the instructions. But in order to check their actual meaning, I had to use an external validator. I opted for the Wikipedia API, making a Wikipedia query with the player's answer. Search results are parsed as a JSON object using the Json.NET framework and then each JToken is compared with categories that the answer should fit into. The first category to check is the main category used in the question. Since the Wikipedia search results might not include this one particular word, yet the answer can still be correct, I included addtitional, "helper" categories to validate the answer against. For example, if the question contains the category "plant", the Wikipedia query results are searched for "plant" and then also for "flora", "vegetation" and "cultivate". This approach can still produce incorrect results (false positives/false negatives), but it works well most of the time.

- _App deployment_. I have some previous experience with WinForms, which allows to build and deploy straightaway from Visual Studio. In MAUI, however, it didn't work this way and I had to manually disable default packaging to be able to deploy and run the final app on my machine without any additional actions.

## **Credits**

Created by Wojciech Grodzicki.

- Initial project template by [The C# Academy](https://www.youtube.com/watch?v=o81wpRuOGjE&list=PL4G0MUH8YWiAMypwveH2LlLK_o8Jto9CE)
- Fredoka font by Milena Brandão, Hafontia via [Google Fonts](https://fonts.google.com/specimen/Fredoka)
- Indie Flower font by Kimberly Geswein via [Google Fonts](https://fonts.google.com/specimen/Indie+Flower)
- Background by stux via [Pixabay](https://pixabay.com/photos/black-board-traces-of-chalk-school-1072366/)
- Trash bin icon by cybertotte via [Pixabay](https://pixabay.com/vectors/dust-bin-icon-the-trash-can-debris-4875414/)
