using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Intro;
using Projeto_Hub_de_Jogos.Service.Games.TicTacToe;
using Projeto_Hub_de_Jogos.Service.Players;
using Intro;

namespace Projeto_Hub_de_Jogos.Service.Games.Battleship
{
    public class BattlershipService
    {
        public static BattlershipBoard board = new BattlershipBoard();

        public static string row { get; set; }
        public static int rowInt { get; set; }
        public int collun { get; set; }
        public string simbol;
        public string pointPlayer1;
        public string pointPlayer2;
        public int TotalCountShip = 10;
        public int CountHitShipPlayer1 = 0;
        public int CountHitShipPlayer2 = 0;

        public void ShowRules()
        {
            Console.WriteLine("REGRAS");
            Console.WriteLine(" > A Batalha Naval do Hub é diferente das que você conhece ");
            Console.WriteLine(" > o player1 e player2 terão que se juntar contra o navio");
            Console.WriteLine("   do oponente: o vilão do hub em um tabuleiro 8x8");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(" > Linhas: A, B, C, D, E, F, G e H  |  Colunas: 0, 1, 2, 3, 4, 5, 6, 7");
            Console.ResetColor();
            Console.WriteLine(" > São jogadas intercaladas entre o player1 e o player2");
            Console.WriteLine(" > Ganha quem acertar mais navios e/ou pedaços de navio");
            Console.WriteLine(" > Quando acertar a agua recebe o simbolo: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" > Player 1 => ░");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(" > Player 2 => ▓");
            Console.ResetColor();
            Console.WriteLine(" > Quando acertar o navio do vilão é atribuido o icone: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" > Player 1 => ▲");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(" > Player 2 => ▼");
            Console.ResetColor();
            Console.WriteLine(" > Serão distribuido entre os dois jogadores 10 tiros");
            Console.WriteLine();
            Console.WriteLine(" Vamos começar!");
            Console.WriteLine();
        }
        
        public void Play(Player player1, Player player2)
        {
            for (int i = 0; i < 16; i++)
            {
                if (i % 2 == 0)
                {
                    if (VerifyCollAndRowInput(player1))
                    {
                        ConvertInputRow();
                        if (
                            BattlershipBoard.battlership[rowInt, collun] == "  ▓  " || BattlershipBoard.battlership[rowInt, collun] == "  ░  " ||
                            BattlershipBoard.battlership[rowInt, collun] == "  ▼  " || BattlershipBoard.battlership[rowInt, collun] == "  ▲  "
                            )
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine(" Esta posição já esta ocupada");
                            Console.ResetColor();
                        }
                        else
                        {
                            if (HitPosition(player1))
                            {
                                simbol = "  ▲  ";
                                CountHitShipPlayer1++;
                                TotalCountShip--;
                            }
                            else
                            {
                                simbol = "  ░  ";
                            }
                        }
                    }
                }
                else
                {
                    if (VerifyCollAndRowInput(player2))
                    {
                        ConvertInputRow();
                        if (
                            BattlershipBoard.battlership[rowInt, collun] == "  ▓  " || BattlershipBoard.battlership[rowInt, collun] == "  ░  " ||
                            BattlershipBoard.battlership[rowInt, collun] == "  ▼  " || BattlershipBoard.battlership[rowInt, collun] == "  ▲  "
                            )
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine(" Esta posição já esta ocupada");
                            Console.ResetColor();
                        }
                        else
                        {
                            if (HitPosition(player2))
                            {
                                simbol = "  ▼  ";
                                CountHitShipPlayer2++;
                                TotalCountShip--;
                            }
                            else
                            {
                                simbol = "  ▓  ";
                            }
                        }
                    }
                }

                BattlershipBoard.battlership[rowInt, collun] = simbol;

                board.ShowBoard();

                if (TotalCountShip == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine();
                    Console.WriteLine("      RESULTADO DESTA PARTIDA");
                    Console.ResetColor();
                    if (CountHitShipPlayer1 > CountHitShipPlayer2)
                    {
                        Program.player1.GameScoreBattleship++;
                        //ScorePlayer1.GameScoreBattleship++;
                        Program.repository.GameScoreBattleShipInList(player1.Name);

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"      {player1.Name} ganhou a partida");
                        Console.WriteLine();
                        Console.ResetColor();
                    }
                    else if (CountHitShipPlayer1 < CountHitShipPlayer2)
                    {
                        Program.player2.GameScoreBattleship++;

                        //ScorePlayer2.GameScoreBattleship++;
                        Program.repository.GameScoreBattleShipInList(player2.Name);

                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine($"      {player2.Name} ganhou a partida");
                        Console.WriteLine();
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("           Empate");
                        Console.WriteLine();
                        Console.ResetColor();
                    }

                    BattlershipBoard.battlership = new string[8,8];
                    break;
                }
            }
        }

        //inserir posição 
        public bool VerifyCollAndRowInput(Player name)
        {
            do
            {
                Console.WriteLine();
                Console.Write($" {name.Name}, informe a linha (A, B, C, D, E, F, G e H): ");
                row = Console.ReadLine().ToUpper();

                if (row != "A" && row != "B" && row != "C" && row != "D" && row != "E" && row != "F" && row != "G" && row != "H")
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine(" Tente novamente, apenas A, B, C, D, E, F, G e H");
                    Console.ResetColor();
                }
            } while (row != "A" && row != "B" && row != "C" && row != "D" && row != "E" && row != "F" && row != "G" && row != "H");

            do
            {
                Console.Write($" Agora informe a coluna (0, 1, 2, 3, 4, 5, 6, 7): ");
                collun = int.Parse(Console.ReadLine());
                Console.WriteLine();

                if (collun != 0 && collun != 1 && collun != 2 && collun != 3 && collun != 4 && collun != 5 && collun != 6 && collun != 7)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine(" Tente novamente, apenas 0, 1, 2, 3, 4, 5, 6, 7");
                    Console.ResetColor();
                }
            } while (collun != 0 && collun != 1 && collun != 2 && collun != 3 && collun != 4 && collun != 5 && collun != 6 && collun != 7);

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
            else if (row == "C")
            {
                rowInt = 2;
            }
            else if (row == "D")
            {
                rowInt = 3;
            }
            else if (row == "E")
            {
                rowInt = 4;
            }
            else if (row == "F")
            {
                rowInt = 5;
            }
            else if (row == "G")
            {
                rowInt = 6;
            }
            else if (row == "H")
            {
                rowInt = 7;
            }

        }

        public bool HitPosition(Player player)
        {
            string[,] BoardTemplate = new string[8, 8];
            BoardTemplate[2, 2] = "i"; BoardTemplate[4, 2] = "i"; BoardTemplate[7, 1] = "i";
            BoardTemplate[2, 6] = "i"; BoardTemplate[4, 3] = "i"; BoardTemplate[7, 2] = "i";
            BoardTemplate[6, 6] = "i"; BoardTemplate[4, 4] = "i";
            BoardTemplate[7, 7] = "i"; BoardTemplate[4, 5] = "i";

            if (BoardTemplate[rowInt, collun] == "i")
            {
                player.GameScoreBattleship++;
                //ship
                return true;
            }
            //water
            return false;
        }




        ////cada player tem seu tabuleiro 
        //public bool navioPlayer1()
        //{
        //    int row = 0;
        //    int col = 0;

        //    string[,] GabaritoTabuleiroDoPlayer1 = new string[row, col];

        //     Console.WriteLine("informe linha ");
        //    int linha = Console.Read();
        //    Console.WriteLine("informe coluna ");
        //    int coluna = Console.Read();

        //    row = linha;
        //    col = coluna;

        //    GabaritoTabuleiroDoPlayer1[row, col] = "N";

        //    //Play2 jogando
        //    string[,]GabaritoTabuleiroDoPlayer2 = new string[row, col]; //ele informou esse 
        //    if (GabaritoTabuleiroDoPlayer1[row, col] == "N") // se oq foi informado 
        //    {
        //        GabaritoTabuleiroDoPlayer2[row, col] = "N";
        //        return true;
        //    }
        //    else
        //    {
        //        GabaritoTabuleiroDoPlayer2[row, col] = "A";
        //        return false; 
        //    }

    }
}