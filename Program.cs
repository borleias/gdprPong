// This is a sample implementation of the game PONG in C#.

// this code is heavily based this source:
// https://github.com/SoftUni/SoftUni-Project-Tutorials---Pong-Game-C-Sharp


//Field
const int fieldLength = 50, fieldWidth = 15;
const char fieldTile = '#';
string line = string.Concat(Enumerable.Repeat(fieldTile, fieldLength));

//Rackets 
const int racketLength = fieldWidth / 4;
const char racketTile = '|';

int leftRacketHeight = 0;
int rightRacketHeight = 0;

//Ball
int ballX = fieldLength / 2;
int ballY = fieldWidth / 2;
const char ballTile = 'O';

bool isBallGoingDown = true;
bool isBallGoingRight = true;

//Points
int leftPlayerPoints = 0;
int rightPlayerPoints = 0;

//Scoreboard
int scoreboardX = fieldLength / 2 - 2;
int scoreboardY = fieldWidth + 1;

Console.Clear();

// game loop
while (true)
{
    //Print the borders
    Console.SetCursorPosition(0, 0);
    Console.WriteLine(line);

    Console.SetCursorPosition(0, fieldWidth);
    Console.WriteLine(line);

    //Print the rackets
    for (int i = 0; i < racketLength; i++)
    {
        Console.SetCursorPosition(0, i + 1 + leftRacketHeight);
        Console.WriteLine(racketTile);
        Console.SetCursorPosition(fieldLength - 1, i + 1 + rightRacketHeight);
        Console.WriteLine(racketTile);
    }

    bool gameFinished = false;
    //Do until a key is pressed
    while (!Console.KeyAvailable && !gameFinished)
    {
        Console.SetCursorPosition(ballX, ballY);
        Console.WriteLine(ballTile);
        Thread.Sleep(150); //Adds a timer so that the players have time to react

        Console.SetCursorPosition(ballX, ballY);
        Console.WriteLine(" "); //Clears the previous position of the ball

        //Update position of the ball
        if (isBallGoingDown)
        {
            ballY++;
        }
        else
        {
            ballY--;
        }

        if (isBallGoingRight)
        {
            ballX++;
        }
        else
        {
            ballX--;
        }

        if (ballY == 1 || ballY == fieldWidth - 1)
        {
            isBallGoingDown = !isBallGoingDown; //Change direction
        }

        if (ballX == 1)
        {
            if (ballY >= leftRacketHeight + 1 && ballY <= leftRacketHeight + racketLength) //Left racket hits the ball and it bounces
            {
                isBallGoingRight = !isBallGoingRight;
            }
            else //Ball goes out of the field; Right player scores
            {
                rightPlayerPoints++;
                ballY = fieldWidth / 2;
                ballX = fieldLength / 2;
                if (rightPlayerPoints == 10)
                {
                    gameFinished = true;
                }
            }
        }

        if (ballX == fieldLength - 2)
        {
            if (ballY >= rightRacketHeight + 1 && ballY <= rightRacketHeight + racketLength) //Right racket hits the ball and it bounces
            {
                isBallGoingRight = !isBallGoingRight;
            }
            else //Ball goes out of the field; Left player scores
            {
                leftPlayerPoints++;
                ballY = fieldWidth / 2;
                ballX = fieldLength / 2;
                if (leftPlayerPoints == 10)
                {
                    gameFinished = true;
                }
            }
        }

        Console.SetCursorPosition(scoreboardX, scoreboardY);
        Console.WriteLine($"{leftPlayerPoints} | {rightPlayerPoints}");
    }

    if (gameFinished)
    {
        break;
    }

    //Check which key has been pressed
    switch (Console.ReadKey().Key)
    {
        case ConsoleKey.UpArrow:
            if (rightRacketHeight > 0)
            {
                rightRacketHeight--;
            }
            break;

        case ConsoleKey.DownArrow:
            if (rightRacketHeight < fieldWidth - racketLength - 1)
            {
                rightRacketHeight++;
            }
            break;

        case ConsoleKey.W:
            if (leftRacketHeight > 0)
            {
                leftRacketHeight--;
            }
            break;

        case ConsoleKey.S:
            if (leftRacketHeight < fieldWidth - racketLength - 1)
            {
                leftRacketHeight++;
            }
            break;
    }

    //Clear the rackets’ previous positions
    for (int i = 1; i < fieldWidth; i++)
    {
        Console.SetCursorPosition(0, i);
        Console.WriteLine(" ");
        Console.SetCursorPosition(fieldLength - 1, i);
        Console.WriteLine(" ");
    }
}

Console.Clear();
Console.SetCursorPosition(0, 0);

if (rightPlayerPoints == 10)
{
    Console.WriteLine("Right player won!");
}
else
{
    Console.WriteLine("Left player won!");
}
