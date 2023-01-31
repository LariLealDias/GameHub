using Projeto_Hub_de_Jogos.Repository;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Reflection.Emit;
using static System.Formats.Asn1.AsnWriter;
using Projeto_Hub_de_Jogos.Service.Games.Battleship;
using Projeto_Hub_de_Jogos.Service.Games.TicTacToe;
using Projeto_Hub_de_Jogos.Service.Games;
using Projeto_Hub_de_Jogos.Service.Players;

namespace Intro
{
    public class Program
    {
        public static Player player1 = new Player();
        public static Player player2 = new Player();

        public static TicTacToeService ticTactToe = new TicTacToeService(player1, player2);
        public static BattlershipService battlership = new BattlershipService();
        public static ScoreBoard score = new ScoreBoard();
        public static JsonRepository repository = new JsonRepository();
        
        static void ShowMessageLogin()
        {
            ShowHeader("     LOGIN ");
            Console.WriteLine(" Para jogar no Hub, são necessarios 2 usuario logados");
            Console.WriteLine();
        }

        public static bool Login()
        {
            ShowMessageLogin();
            Console.Write(" Player 1, informe seu nome: ");
            Console.ForegroundColor = ConsoleColor.Green;
            string name = Console.ReadLine();
            Console.ResetColor();
            Console.Write(" Informe a senha: ");
            Console.ForegroundColor = ConsoleColor.Green;
            string key = Console.ReadLine();
            Console.ResetColor();

            if (repository.LoginInList(name, key))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(" Logado com sucesso ");
                Console.WriteLine(" Aperte enter para continuar");
                Console.ResetColor();
                Console.ReadLine();
               // player1.Name = name;
                player1 = repository.GetDataPlayer();
               // player1.Key = key;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(" não foi possivel encontrar usuário");
                Console.WriteLine(" Aperte enter para continuar");
                Console.ResetColor();
                Console.ReadLine();
                return false;
            }

            Console.ResetColor();
            Console.WriteLine();

            Console.Write(" Player 2, informe seu nome: ");
            Console.ForegroundColor = ConsoleColor.Blue;
            string name2 = Console.ReadLine();

            Console.ResetColor();
            Console.Write(" Informe a senha: ");
            Console.ForegroundColor = ConsoleColor.Blue;
            string key2 = Console.ReadLine();

            if (repository.LoginInList(name2, key2))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(" Logado com sucesso ");
                Console.WriteLine(" Aperte enter para continuar");
                Console.ResetColor();
                Console.ReadLine();
                player2.Name = name2;
                //player2 = repository.GetPlayerInList(player2);
                 player2.Key = key2;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(" não foi possivel encontrar usuário");
                Console.WriteLine(" Aperte enter para continuar");
                Console.ResetColor();
                Console.ReadLine();
                return false;
            }
            return true;
        }

        static void Register()
        {
            Console.WriteLine();
            Player player = new Player();
            Console.Write("Informe o nome: ");
            player.SetName(Console.ReadLine());

            Console.Write("Informe a senha: ");
            player.SetKey(Console.ReadLine());

            repository.RegisterInList(player);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(" Cadastrado com sucesso ");
            Console.WriteLine(" Aperte enter para continuar");
            Console.ResetColor();
            Console.ReadLine();
        }

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

        static void ShowMainMenu()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("  Bem vindo ao HUB de jogos, temos disponiveis dois jogos");
            Console.WriteLine("  1 - Jogo da Velha");
            Console.WriteLine("  2 - Batalha Naval");
            Console.WriteLine("  3 - Sobre o HUB ");
            Console.WriteLine("  0 - Voltar ");
            Console.WriteLine();
            Console.ResetColor();
        }
        
        static void BackToMenu()
        {
            Console.Write("      Aperte ENTER para voltar ao menu principal ");
            Console.ReadLine();
        }

        static void AboutTheHub()
        {
            Console.WriteLine("  O Hub é uma plataforma de jogos com interação pelo terminal");
            Console.WriteLine("  O projeto é um exercicio do bootcamp SharpCoders da Imã Learning Place");
            Console.WriteLine("  O objetivo é aprofundar conhecimentos em Orientação a Objetos com C#");
            Console.WriteLine();
            Console.WriteLine("  Autora: Larissa Leal");
            Console.WriteLine();

            BackToMenu();
        }

        static void ShowGameMenu()
        {
            int choiceGameMenu;
            do
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("  O que deseja realizar agora?");
                Console.WriteLine("  1 - Jogar Jogo da Velha");
                Console.WriteLine("  2 - Jogar Batalha Naval");
                Console.WriteLine("  3 - Placar ");
                Console.WriteLine("  0 - Voltar ao menu principal ");
                Console.WriteLine();
                Console.Write("Digite a opção desejada: ");
                Console.ResetColor();
                choiceGameMenu = int.Parse(Console.ReadLine());
                Console.WriteLine();

                switch (choiceGameMenu)
                {
                    case 1:
                        ShowHeader("Jogo da velha");
                        ToPlayOrSeeRulesTicTacToe();
                        //ticTactToe.Play(player1.GetLoginName(), player2.GetLoginName());
                        ShowGameMenu();
                        break;
                    case 2:
                        ShowHeader("Batalha Naval");
                        ToPlayOrSeeRulesBattlership();
                        //battlership.Play(player1, player2);
                        break;
                    case 3:
                        score.ShowGeralScore(player1, player2);
                        break;
                    case 0:
                        Console.WriteLine("Programa Encerrado");
                        break;
                    default:
                        Console.WriteLine("Opção inválida, tente novamente");
                        break;
                }
            } while (choiceGameMenu != 0);
        }

        static void ToPlayOrSeeRulesTicTacToe()
        {
            string rulesOrPlay;

            do
            {
                Console.Write("Ver regras ou seguir para o jogo? (regras/jogar) ou (R/J): ");
                rulesOrPlay = Console.ReadLine().ToLower();

                if (rulesOrPlay == "regras" || rulesOrPlay == "r")
                {
                    ticTactToe.ShowRules();
                    Program.ticTactToe.Play(player1.GetLoginName(), player2.GetLoginName());
                    ShowGameMenu();
                    break;
                }
                else if (rulesOrPlay == "jogar" || rulesOrPlay == "j")
                {
                    Program.ticTactToe.Play(player1.GetLoginName(), player2.GetLoginName());
                    ShowGameMenu();
                    break;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("                    Algo deu errado");
                    Console.WriteLine("     certifique que esteja escrevendo corretamente");
                    Console.WriteLine();
                }
            } while (rulesOrPlay != "regras" || rulesOrPlay != "r" && rulesOrPlay != "jogar" || rulesOrPlay != "j");
        }

        static void ToPlayOrSeeRulesBattlership()
        {
            string rulesOrPlay;
            do
            {
                Console.Write("Ver regras ou seguir para o jogo? (regras/jogar) ou (R/J): ");
                rulesOrPlay = Console.ReadLine().ToLower();

                if (rulesOrPlay == "regras" || rulesOrPlay == "r")
                {
                    battlership.ShowRules();
                    battlership.Play(player1, player2);
                    ShowGameMenu();
                    break;
                }
                else if (rulesOrPlay == "jogar" || rulesOrPlay == "j")
                {
                    battlership.Play(player1, player2);
                    ShowGameMenu();
                    break;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("                    Algo deu errado");
                    Console.WriteLine("     certifique que esteja escrevendo corretamente");
                    Console.WriteLine();
                }
            } while (rulesOrPlay != "regras" || rulesOrPlay != "r" && rulesOrPlay != "jogar" || rulesOrPlay != "j");
        }

        static void MainMenu()
        {
            int choiceMainMenu;
            do
            {
                ShowHeader("Menu");
                ShowMainMenu();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("Digite a opção desejada: ");
                Console.ResetColor();
                choiceMainMenu = int.Parse(Console.ReadLine());

                switch (choiceMainMenu)
                {
                    case 1:
                        ShowHeader("Jogo da velha");
                        ToPlayOrSeeRulesTicTacToe();
                        break;
                    case 2:
                        ShowHeader("Batalha Naval");
                        ToPlayOrSeeRulesBattlership();
                        break;
                    case 3:
                        ShowHeader("Sobre o HUB");
                        AboutTheHub();
                        break;
                    case 0:
                        ShowHeader("Programa Encerrado");
                        break;
                    default:
                        ShowHeader("Opção inválida, tente novamente");
                        BackToMenu();
                        break;
                }
            } while (choiceMainMenu != 0);

        }

        static void Main(string[] args)
        {
            repository.VerifyJsonFile();

            int choiceLoginCreate;
            do
            {
                ShowHeader("Login/Cadastrar");
                Console.WriteLine("Gostaria de se cadastrar ou logar?");
                Console.WriteLine("1 - cadastrar");
                Console.WriteLine("2 - Logar");
                Console.WriteLine("0 - Fechar programa");
                Console.Write("Digite a opção: ");
                choiceLoginCreate = int.Parse(Console.ReadLine());

                switch (choiceLoginCreate)
                {
                    case 1:
                        Register();
                        break;
                    case 2:
                        if(Login())
                        {
                            MainMenu();
                        }
                        break;
                    case 0:
                        ShowHeader("Programa Encerrado");
                        break;
                    default:
                        Console.WriteLine("Insira uma opção válida");
                        break;
                }
            } while (choiceLoginCreate != 0);
           
        }
    }
}