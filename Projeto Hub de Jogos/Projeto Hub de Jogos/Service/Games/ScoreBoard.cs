using Intro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projeto_Hub_de_Jogos.Repository;
using Projeto_Hub_de_Jogos.Service.Players;

namespace Projeto_Hub_de_Jogos.Service.Games
{
    public class ScoreBoard
    {
        static void ShowHeader(string subheader)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine();
            Console.WriteLine(" ------------------------------------------------------- ");
            Console.WriteLine("|                                                       |");
            Console.WriteLine("|                                                       |");
            Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=- HUB =-=-=-=-=-=-=-=-=-=-=-=-=-");
            Console.WriteLine("|                                                       |");
            Console.WriteLine("|                                                       |");
            Console.WriteLine(" ------------------------------------------------------- ");
            Console.WriteLine($"      >>>>>>>       {subheader}         <<<<<<< ");
            Console.WriteLine();
            Console.WriteLine();
            Console.ResetColor();
        }

        static void ShowOptions()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("  Seguir para ");
            Console.WriteLine("  1 - Placar Jogo da Velha");
            Console.WriteLine("  2 - Placar Batalha Naval");
            Console.WriteLine("  0 - Voltar para menu do Game");
            Console.ResetColor();
        }

        static void ShowScoreMenu(Player player1, Player player2)
        {
            ShowOptions();

            int choiceScoreMenu;
            do
            {
                Console.Write("Digite o numero da opção: ");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                choiceScoreMenu = int.Parse(Console.ReadLine());
                Console.WriteLine();
                Console.ResetColor();

                switch (choiceScoreMenu)
                {
                    case 1:
                        ShowScoreTictacToe(player1, player2);
                        ShowOptions();
                        break;
                    case 2:
                        ShowScoreBattlerShip(player1, player2);
                        ShowOptions();
                        break;
                    case 0:
                        break;
                    default:
                        Console.WriteLine(" Digite uma opção válida");
                        break;
                }
            } while (choiceScoreMenu != 0);
        }

        public void ShowGeralScore(Player player1, Player player2)
        {
            JsonRepository repository = new JsonRepository();
            Player pl1 = repository.GetPlayerScore(player1.Name);
            Player pl2 = repository.GetPlayerScore(player2.Name);

            int sumScore1 = pl1.GameScoreBattleship + pl1.GameScoreTicTacToe;
            int sumScore2 = pl2.GameScoreBattleship + pl2.GameScoreTicTacToe;

            ShowHeader(" PLACAR GERAL ");
            if (sumScore1 > sumScore2)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"   Primeiro lugar => {pl1.Name}");
                Console.WriteLine($"   Com {sumScore1} partida(s) ganha(s) no total");
                Console.ResetColor();
                Console.WriteLine("   VS        ");
                Console.WriteLine($"   {pl2.Name} com {sumScore2} partida(s) ganha(s) ");
                Console.WriteLine();
            }
            else if (sumScore1 < sumScore2)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"   Primeiro lugar => {pl2.Name}");
                Console.WriteLine($"   Com {sumScore2} partida(s) ganha(s) no total");
                Console.ResetColor();
                Console.WriteLine("   VS        ");
                Console.WriteLine($"   {pl1.Name} com {sumScore1} partida(s) ganha(s) ");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Vocês estão empatados, continuem jogando para virar o jogo!");
                Console.WriteLine();
            }
            ShowScoreMenu(player1, player2);
        }

        static void ShowScoreTictacToe(Player player1, Player player2)
        {
            JsonRepository repository = new JsonRepository();
            Player pl1 = repository.GetPlayerScore(player1.Name);
            Player pl2 = repository.GetPlayerScore(player2.Name);

            ShowHeader(" PLACAR JOGO DA VELHA ");
            if (pl1.GameScoreTicTacToe <= 0 && pl2.GameScoreTicTacToe <= 0)
            {
                Console.WriteLine("Parece que vocês não jogaram uma partida de Jogo da Velha");
            }
            else
            {
                if (pl1.GameScoreTicTacToe > pl2.GameScoreTicTacToe)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"   Primeiro lugar => {pl1.Name}");
                    Console.WriteLine($"   Com {pl1.GameScoreTicTacToe} partida(s) ganha(s) no total");
                    Console.ResetColor();

                    Console.WriteLine("   VS        ");
                    Console.WriteLine($"   {pl2.Name} com {pl2.GameScoreTicTacToe} partida(s) ganhada(s) ");
                }
                else if (pl1.GameScoreTicTacToe < pl2.GameScoreTicTacToe)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"   Primeiro lugar => {pl2.Name}");
                    Console.WriteLine($"   Com {pl2.GameScoreTicTacToe} partida(s) ganha(s) no total");
                    Console.ResetColor();

                    Console.WriteLine("   VS        ");
                    Console.WriteLine($"   {pl1.Name} com {pl1.GameScoreTicTacToe} partida(s) ganhada(s) ");
                }
                if (pl1.GameScoreTicTacToe == pl2.GameScoreTicTacToe)
                {
                    Console.WriteLine();
                    Console.WriteLine(" Vocês estão empatados, continuem jogando para virar o jogo!");
                    Console.WriteLine();
                }
              
                Console.WriteLine();
                Console.WriteLine();
                Console.Write("   Digite enter para voltar");
                Console.ReadLine();
                Console.WriteLine();
            }
        }

        static void ShowScoreBattlerShip(Player player1, Player player2)
        {
            JsonRepository repository = new JsonRepository();
            Player pl1 = repository.GetPlayerScore(player1.Name);
            Player pl2 = repository.GetPlayerScore(player2.Name);

            ShowHeader(" PLACAR BATALHA NAVAL ");
            if (pl1.GameScoreBattleship <= 0 && pl2.GameScoreBattleship <= 0)
            {
                Console.WriteLine("Parece que vocês não jogaram uma partida de Batalha Naval");
            }
            else
            {
                if (pl1.GameScoreBattleship > pl2.GameScoreBattleship)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"   Primeiro lugar => {pl1.Name}");
                    Console.WriteLine($"   Com {pl1.GameScoreBattleship} partidas ganhas no total");
                    Console.ResetColor();

                    Console.WriteLine("   VS        ");
                    Console.WriteLine($"   {pl2.Name} com {pl2.GameScoreBattleship} partidas ganhada ");
                }
                else if (pl1.GameScoreBattleship < pl2.GameScoreBattleship)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"   Primeiro lugar => {pl2.Name}");
                    Console.WriteLine($"   Com {pl2.GameScoreBattleship} partidas ganhas no total");
                    Console.ResetColor();

                    Console.WriteLine("   VS        ");
                    Console.WriteLine($"   {pl1.Name} com {pl1.GameScoreBattleship} partidas ganhada ");
                }
                if (pl1.GameScoreBattleship == pl2.GameScoreBattleship)
                {
                    Console.WriteLine();
                    Console.WriteLine("   Vocês estão empatados, continuem jogando para virar o jogo!");
                    Console.WriteLine();
                }
                Console.WriteLine();
                Console.WriteLine();
                Console.ReadLine();
                Console.Write("   Digite enter para voltar");
            }
        }
    }
}
