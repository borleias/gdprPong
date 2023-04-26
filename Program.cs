// This is a sample implementation of the game PONG in C#.

// this code is heavily based this source:
// https://github.com/SoftUni/SoftUni-Project-Tutorials---Pong-Game-C-Sharp


//Field
using System.Text;

const int fieldLength = 50, fieldWidth = 15;

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

//Print the borders
DrawBoard(fieldLength, fieldWidth);

// game loop
while (true)
{
    DrawRackets(leftRacketHeight, rightRacketHeight);

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

        // touching top or bottom
        if (ballY == 2 || ballY == fieldWidth - 2)
        {
            isBallGoingDown = !isBallGoingDown; //Change direction
        }

        // reaching left side
        if (ballX == 2)
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

        // reaching right side
        if (ballX == fieldLength - 3)
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

    ClearRackets(leftRacketHeight, rightRacketHeight);

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

    //ClearRackets(fieldLength, fieldWidth);
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


void DrawBoard(int fieldLength, int fieldWidth)
{
    const char topLeft = (char)0x2554;
    const char topRight = (char)0x2557;
    const char bottomLeft = (char)0x255A;
    const char bottomRight = (char)0x255D;
    const char leftCross = (char)0x255F;
    const char rightCross = (char)0x2562;
    const char hLineDouble = (char)0x2550;
    const char hLineSingle = (char)0x2500;
    const char vLine = (char)0x2551;
    string horizontalLineDouble = string.Concat(Enumerable.Repeat(hLineDouble, fieldLength - 2));
    string horizontalLineSingle = string.Concat(Enumerable.Repeat(hLineSingle, fieldLength - 2));

    Console.OutputEncoding = Encoding.Unicode;
    Console.Clear();

    // Top
    DrawSymbol(0, 0, topLeft);
    DrawText(1, 0, horizontalLineDouble);
    DrawSymbol(fieldLength - 1, 0, topRight);

    // Sides
    for (int i = 1; i < fieldWidth - 3; i++)
    {
        DrawSymbol(0, i, vLine);
        DrawSymbol(fieldLength - 1, i, vLine);
    }

    // Scoreboard
    DrawSymbol(0, fieldWidth - 3, leftCross);
    DrawText(1, fieldWidth - 3, horizontalLineSingle);
    DrawSymbol(fieldLength - 1, fieldWidth - 3, rightCross);
    DrawSymbol(0, fieldWidth - 2, vLine);
    DrawSymbol(fieldLength - 1, fieldWidth - 2, vLine);

    // Bottom
    DrawSymbol(0, fieldWidth - 1, bottomLeft);
    DrawText(1, fieldWidth - 1, horizontalLineDouble);
    DrawSymbol(fieldLength - 1, fieldWidth - 1, bottomRight);
}

void DrawSymbol(int x, int y, char symbol)
{
    Console.SetCursorPosition(x, y);
    Console.Write(symbol);
}

void DrawText(int x, int y, string text)
{
    Console.SetCursorPosition(x, y);
    Console.Write(text);
}

void DrawRackets(int leftRacketHeight, int rightRacketHeight)
{
    DrawRacketSymbols(racketTile, leftRacketHeight, rightRacketHeight);
}

void ClearRackets(int leftRacketHeight, int rightRacketHeight)
{
    DrawRacketSymbols(' ', leftRacketHeight, rightRacketHeight);
}

void DrawRacketSymbols(char racketTile, int leftRacketHeight, int rightRacketHeight)
{
    //Print the rackets
    for (int i = 0; i < racketLength; i++)
    {
        DrawSymbol(1, i + 1 + leftRacketHeight, racketTile);
        DrawSymbol(fieldLength - 2, i + 1 + rightRacketHeight, racketTile);
    }
}


//void ClearRackets(int fieldLength, int fieldWidth)
//{
//    //Clear the rackets’ previous positions
//    for (int i = 1; i < fieldWidth; i++)
//    {
//        Console.SetCursorPosition(0, i);
//        Console.WriteLine(" ");
//        Console.SetCursorPosition(fieldLength - 1, i);
//        Console.WriteLine(" ");

//        DrawSymbol(0, i + 1 + leftRacketHeight, racketTile);
//        DrawSymbol(fieldLength - 1, i + 1 + rightRacketHeight, racketTile);
//    }
//}