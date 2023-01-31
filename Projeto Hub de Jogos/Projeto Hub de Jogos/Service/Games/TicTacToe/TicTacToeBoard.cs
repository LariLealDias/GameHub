using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_Hub_de_Jogos.Service.Games.TicTacToe
{
    class TicTacToeBoard
    {
        public static string[,] ticTacToe { get; set; } = new string[3, 3];

        public static void ShowBoard()
        {
            Console.WriteLine("    0 | 1 | 2");
            Console.WriteLine();
            for (int j = 0; j < 3; j++)
            {
                Console.Write((char)('A' + j) + "   ");
                for (int x = 0; x < 3; x++)
                {
                    Console.Write(ticTacToe[j, x] + "  ");
                }
                Console.WriteLine();
                Console.WriteLine("    --------");
            }

        }

    }
}
