
using System;

class ConnectFour
{
    private char[][] board;
    private char currentPlayer;

    public ConnectFour()
    {
        board = new char[6][];
        for (int row = 0; row < 6; row++)
        {
            board[row] = new char[7];
            for (int col = 0; col < 7; col++)
            {
                board[row][col] = ' ';
            }
        }

        currentPlayer = 'X';
    }

    public void DisplayBoard()
    {
        Console.Clear(); // Clear the console before displaying the updated board

        Console.WriteLine("Welcome to Connect Four!\n");

        for (int row = 0; row < 6; row++)
        {
            Console.Write("| ");
            for (int col = 0; col < 7; col++)
            {
                Console.Write(board[row][col] + " | ");
            }
            Console.WriteLine();
        }

        Console.WriteLine("-----------------------------");
        Console.WriteLine("  1   2   3   4   5   6   7  ");
    }

    public bool MakeMove(int column)
    {
        if (!IsValidMove(column))
        {
            Console.WriteLine("Invalid move. Please try again.");
            return false;
        }

        int row = GetNextEmptyRow(column);
        board[row][column] = currentPlayer;

        if (IsWinningMove(row, column))
        {
            DisplayBoard();
            Console.WriteLine($"\nPlayer {currentPlayer} wins!");
            return true;
        }

        if (IsBoardFull())
        {
            DisplayBoard();
            Console.WriteLine("\nIt's a draw!");
            return true;
        }

        SwitchPlayers();
        return false;
    }

    public bool IsValidMove(int column)
    {
        if (column < 0 || column >= 7)
        {
            return false;
        }

        return board[0][column] == ' ';
    }

    public int GetNextEmptyRow(int column)
    {
        for (int row = 5; row >= 0; row--)
        {
            if (board[row][column] == ' ')
            {
                return row;
            }
        }

        return -1; // Column is full, should not happen if IsValidMove is called before.
    }

    public bool IsWinningMove(int row, int column)
    {
        char player = currentPlayer;

        // Check horizontally
        int count = 0;
        for (int c = column - 3; c <= column + 3; c++)
        {
            if (c >= 0 && c <= 6 && board[row][c] == player)
            {
                count++;
                if (count == 4)
                {
                    return true;
                }
            }
            else
            {
                count = 0;
            }
        }

        // Check vertically
        count = 0;
        for (int r = row - 3; r <= row + 3; r++)
        {
            if (r >= 0 && r <= 5 && board[r][column] == player)
            {
                count++;
                if (count == 4)
                {
                    return true;
                }
            }
            else
            {
                count = 0;
            }
        }

        // Check diagonals
        count = 0;
        for (int d = -3; d <= 3; d++)
        {
            int r = row + d;
            int c = column + d;
            if (r >= 0 && r <= 5 && c >= 0 && c <= 6 && board[r][c] == player)
            {
                count++;
                if (count == 4)
                {
                    return true;
                }
            }
            else
            {
                count = 0;
            }
        }

        count = 0;
        for (int d = -3; d <= 3; d++)
        {
            int r = row - d;
            int c = column + d;
            if (r >= 0 && r <= 5 && c >= 0 && c <= 6 && board[r][c] == player)
            {
                count++;
                if (count == 4)
                {
                    return true;
                }
            }
            else
            {
                count = 0;
            }
        }

        return false;
    }

    public bool IsBoardFull()
    {
        for (int row = 0; row < 6; row++)
        {
            for (int col = 0; col < 7; col++)
            {
                if (board[row][col] == ' ')
                {
                    return false;
                }
            }
        }

        return true;
    }

    public void SwitchPlayers()
    {
        currentPlayer = (currentPlayer == 'X') ? 'O' : 'X';
    }

    public void Play()
    {
        Console.WriteLine("Welcome to Connect Four!");

        while (true)
        {
            DisplayBoard();
            Console.Write($"\nPlayer {currentPlayer}, choose a column (1-7): ");
            int column;
            if (int.TryParse(Console.ReadLine(), out column))
            {
                column--; // Adjust for 0-based indexing
                if (MakeMove(column))
                {
                    if (PlayAgain())
                    {
                        ResetBoard();
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a number.");
            }
        }
    }

    public bool PlayAgain()
    {
        Console.Write("Do you want to play again? (Y/N): ");
        string input = Console.ReadLine().Trim().ToUpper();
        return input == "Y";
    }

    public void ResetBoard()
    {
        for (int row = 0; row < 6; row++)
        {
            for (int col = 0; col < 7; col++)
            {
                board[row][col] = ' ';
            }
        }

        currentPlayer = 'X';
    }
}

class Program
{
    static void Main(string[] args)
    {
        ConnectFour game = new ConnectFour();
        game.Play();
    }
}
