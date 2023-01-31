using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Intro;
using Projeto_Hub_de_Jogos.Service.Players;

namespace Projeto_Hub_de_Jogos.Service.Games.TicTacToe
{
    public class TicTacToeService
    {
        public Player ScorePlayer1;
        public Player ScorePlayer2;

        //public int GameScoreTicTacToePlayer1 { get; set; } = 0;
        //public int GameScoreTicTacToePlayer2 { get; set; } = 0;
        public static string winNamePlayer { get; set; } = "";
        public static string row { get; set; }
        public static int rowInt { get; set; }
        public int collun;
        public string oxSimbol;
        public int CountObjects = 0;

        //Constructor
        public TicTacToeService(Player player1, Player player2)
        {
            ScorePlayer1 = player1;
            ScorePlayer2 = player2;
        }

        //metodos
        public void Play(string name1, string name2)
        {
            for (int i = 0; i < 9; i++)
            {
                //verifica quem é o jogador da vez e atribui X ou O
                if (i % 2 == 0)
                {
                    if (VerifyCollAndRowInput(name1))
                    {
                        ConvertInputRow();
                        if (TicTacToeBoard.ticTacToe[rowInt, collun] == "O" || TicTacToeBoard.ticTacToe[rowInt, collun] == "X")
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine(" Parece que esta posição já esta preenchida");
                            Console.ResetColor();
                        }
                        else
                        {
                            oxSimbol = "X";
                        }
                    }
                }
                else
                {
                    if (VerifyCollAndRowInput(name2))
                    {
                        ConvertInputRow();
                        if (TicTacToeBoard.ticTacToe[rowInt, collun] == "O" || TicTacToeBoard.ticTacToe[rowInt, collun] == "X")
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine(" Parece que esta posição já esta preenchida");
                            Console.ResetColor();
                        }
                        else
                        {
                            oxSimbol = "O";
                        }
                    }
                }

                TicTacToeBoard.ticTacToe[rowInt, collun] = oxSimbol;

                //Tabuleiro
                TicTacToeBoard.ShowBoard();

                //se alguém ganhar: break
                if (Winner(name1, name2))
                {
                    TicTacToeBoard.ticTacToe = new string[3, 3];
                    break;
                }
            }

            //anunciar vencedor da partida atual
            if (winNamePlayer == name1)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Parabéns {name1} você ganhou a partida!");
                Program.repository.GameScoreTicTacToeInList(name1);
                Console.ResetColor();
            }
            else if (winNamePlayer == name2)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"Parabéns {name2} você ganhou a partida!");
                Program.repository.GameScoreTicTacToeInList(name2);

                Console.ResetColor();
            }
            else
            {
                Console.WriteLine("Houve empate");
            }
        }

        public bool VerifyCollAndRowInput(string name)
        {
            //verificar player1
            do
            {
                Console.WriteLine();
                Console.Write($" {name}, informe a linha (A, B ou C): ");
                row = Console.ReadLine().ToUpper();

                if (row != "A" && row != "B" && row != "C")
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine(" Tente novamente, apenas A, B ou C");
                    Console.ResetColor();
                }
            } while (row != "A" && row != "B" && row != "C");

            do
            {
                Console.Write($" Agora informe a coluna (0, 1 ou 2): ");
                collun = int.Parse(Console.ReadLine());
                Console.WriteLine();

                if (collun != 0 && collun != 1 && collun != 2)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine(" Tente novamente, apenas 0, 1 ou 2");
                    Console.ResetColor();
                }
            } while (collun != 0 && collun != 1 && collun != 2);

            return true;
        }

        public void ConvertInputRow()
        {
            if (row == "A")
            {
                rowInt = 0;
            }
            else if (row == "B")
            {
                rowInt = 1;
            }
            else
            {
                rowInt = 2;
            }
        }

        public bool Winner(string name1, string name2)
        {
            if (
                TicTacToeBoard.ticTacToe[0, 0] == "X" && TicTacToeBoard.ticTacToe[0, 1] == "X" && TicTacToeBoard.ticTacToe[0, 2] == "X" ||
                TicTacToeBoard.ticTacToe[1, 0] == "X" && TicTacToeBoard.ticTacToe[1, 1] == "X" && TicTacToeBoard.ticTacToe[1, 2] == "X" ||
                TicTacToeBoard.ticTacToe[2, 0] == "X" && TicTacToeBoard.ticTacToe[2, 1] == "X" && TicTacToeBoard.ticTacToe[2, 2] == "X" ||

                TicTacToeBoard.ticTacToe[0, 0] == "X" && TicTacToeBoard.ticTacToe[1, 0] == "X" && TicTacToeBoard.ticTacToe[2, 0] == "X" ||
                TicTacToeBoard.ticTacToe[0, 1] == "X" && TicTacToeBoard.ticTacToe[1, 1] == "X" && TicTacToeBoard.ticTacToe[2, 1] == "X" ||
                TicTacToeBoard.ticTacToe[0, 2] == "X" && TicTacToeBoard.ticTacToe[1, 2] == "X" && TicTacToeBoard.ticTacToe[2, 2] == "X" ||

                TicTacToeBoard.ticTacToe[0, 2] == "X" && TicTacToeBoard.ticTacToe[1, 1] == "X" && TicTacToeBoard.ticTacToe[2, 0] == "X" ||
                TicTacToeBoard.ticTacToe[0, 0] == "X" && TicTacToeBoard.ticTacToe[1, 1] == "X" && TicTacToeBoard.ticTacToe[2, 2] == "X"
               )
            {
                winNamePlayer = name1;
                ScorePlayer1.GameScoreTicTacToe++;
                return true;

            }
            else if (
                TicTacToeBoard.ticTacToe[0, 0] == "O" && TicTacToeBoard.ticTacToe[0, 1] == "O" && TicTacToeBoard.ticTacToe[0, 2] == "O" ||
                TicTacToeBoard.ticTacToe[1, 0] == "O" && TicTacToeBoard.ticTacToe[1, 1] == "O" && TicTacToeBoard.ticTacToe[1, 2] == "O" ||
                TicTacToeBoard.ticTacToe[2, 0] == "O" && TicTacToeBoard.ticTacToe[2, 1] == "O" && TicTacToeBoard.ticTacToe[2, 2] == "O" ||

                TicTacToeBoard.ticTacToe[0, 0] == "O" && TicTacToeBoard.ticTacToe[1, 0] == "O" && TicTacToeBoard.ticTacToe[2, 0] == "O" ||
                TicTacToeBoard.ticTacToe[0, 1] == "O" && TicTacToeBoard.ticTacToe[1, 1] == "O" && TicTacToeBoard.ticTacToe[2, 1] == "O" ||
                TicTacToeBoard.ticTacToe[0, 2] == "O" && TicTacToeBoard.ticTacToe[1, 2] == "O" && TicTacToeBoard.ticTacToe[2, 2] == "O" ||

                TicTacToeBoard.ticTacToe[0, 2] == "O" && TicTacToeBoard.ticTacToe[1, 1] == "O" && TicTacToeBoard.ticTacToe[2, 0] == "O" ||
                TicTacToeBoard.ticTacToe[0, 0] == "O" && TicTacToeBoard.ticTacToe[1, 1] == "O" && TicTacToeBoard.ticTacToe[2, 2] == "O"
                     )
            {
                winNamePlayer = name2;
                ScorePlayer2.GameScoreTicTacToe++;
                return true;
            }
            return false;
        }

        public void ShowRules()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("           REGRAS DO JOGO DA VELHA");
            Console.ResetColor();

            Console.WriteLine(" > O jogo é composto por dois competidores");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" > O primeiro jogador recebe  X");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(" > O segundo jogador recebe   O");
            Console.ResetColor();

            Console.WriteLine(" > As jogadas são alternadas");
            Console.WriteLine(" > A cada jogada, é preciso informar uma combinação de linha e coluna");

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(" > Linhas: A, B e C  |  Colunas: 0, 1 e 2");
            Console.ResetColor();

            Console.WriteLine(" > Vence o jogador que conseguir formar primeiro uma linha com três");
            Console.WriteLine("   símbolos iguais, seja ela na horizontal, vertical ou diagonal");
            Console.WriteLine();
            Console.WriteLine("Cerquilha de exemplo");
            Console.WriteLine("                       0 | 1 | 2");
            Console.WriteLine("                     A   |   |  ");
            Console.WriteLine("                      ---|---|---");
            Console.WriteLine("                     B   |   |  ");
            Console.WriteLine("                      ---|---|---");
            Console.WriteLine("                     C   |   |  ");
            Console.WriteLine();
            Console.WriteLine(" Vamos começar!");
            Console.WriteLine();

        }

    }
}
