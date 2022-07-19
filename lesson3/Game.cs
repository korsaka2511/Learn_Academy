using System;

namespace Krestiki
{
    class Game
    {
        private char[,] _field = new char[3, 3] {
                { ' ', ' ', ' ' },
                { ' ', ' ', ' ' },
                { ' ', ' ', ' ' },
            };

        private bool _isXMove = true;

        private readonly string _coordErrorMessage = "Координаты должны быть: [0, 2]";

        public Game()
        {
            Draw();

            int row, col;
            do
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("\n Введите номер ряда  0- первый  2- последний: [0, 2]\n (или -1 для выхода): ");
                Console.ForegroundColor = ConsoleColor.White;
                if (!int.TryParse(Console.ReadLine(), out row))
                {
                    ShowError(_coordErrorMessage);
                    continue;
                }
                if (row == -1) break;

                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("\n Введите номер столбца 0- первый  2- последний: [0, 2]\n (или -1 для выхода): ");
                Console.ForegroundColor = ConsoleColor.White;
                if (!int.TryParse(Console.ReadLine(), out col))
                {
                    ShowError(_coordErrorMessage);
                    continue;
                }
                if (col == -1) break;

                Update(row, col);
                Draw();
            } while (true);

        }

        private void Update(int row, int col)
        {
            if (row == -1 || col == -1)
            {
                return;
            }

            if (0 <= row && row <= 2 &&
                0 <= col && col <= 2)
            {
                if (_field[row, col] == ' ')
                {
                    _field[row, col] = _isXMove ? 'X' : 'O';

                    if (IsWinner('X'))
                    {
                        Draw();
                        EndGame("Крестики");
                    }
                    else if (IsWinner('O'))
                    {
                        Draw();
                        EndGame("Нолики");
                    }

                    _isXMove = !_isXMove;
                }
                else
                {
                    ShowError("Тут уже есть значение");
                }
            }
            else
            {
                ShowError(_coordErrorMessage);
                return;
            }
        }

        private void Draw()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n Крестики - Нолики\n");
            Console.ForegroundColor = ConsoleColor.Magenta;

            ShowField();

            // Устанавливаем цвет рисования белым
            Console.ForegroundColor = ConsoleColor.White;
        }

        private void ShowField()
        {
            Console.WriteLine(string.Format("  {0} | {1} | {2}", _field[0, 0], _field[0, 1], _field[0, 2]));
            Console.WriteLine(" ---+---+---");
            Console.WriteLine(string.Format("  {0} | {1} | {2}", _field[1, 0], _field[1, 1], _field[1, 2]));
            Console.WriteLine(" ---+---+---");
            Console.WriteLine(string.Format("  {0} | {1} | {2}", _field[2, 0], _field[2, 1], _field[2, 2]));
        }

        private void ShowError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n " + message);
            Console.WriteLine(" Нажмите любую клавишу");

            Console.ReadKey();
        }

        private bool IsWinner(char player)
        {
            return (
                // Rows
                (_field[0, 0] == player && _field[0, 1] == player && _field[0, 2] == player) ||
                (_field[1, 0] == player && _field[1, 1] == player && _field[1, 2] == player) ||
                (_field[2, 0] == player && _field[2, 1] == player && _field[2, 2] == player) ||
                // Columns
                (_field[0, 0] == player && _field[1, 0] == player && _field[2, 0] == player) ||
                (_field[0, 1] == player && _field[1, 1] == player && _field[2, 1] == player) ||
                (_field[0, 2] == player && _field[1, 2] == player && _field[2, 2] == player) ||
                // Diagonals
                (_field[0, 0] == player && _field[1, 1] == player && _field[2, 2] == player) ||
                (_field[0, 2] == player && _field[1, 1] == player && _field[2, 0] == player)
            );
        }

        private void ClearField()
        {
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    _field[row, col] = ' ';
                }
            }
        }

        private void EndGame(string player)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(string.Format("\n Победили {0}!", player));
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n Нажмите любую клавишу для продолжения");
            Console.ReadKey();
            ClearField();
        }
    }
}