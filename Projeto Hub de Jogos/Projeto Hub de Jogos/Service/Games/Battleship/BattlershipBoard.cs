using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_Hub_de_Jogos.Service.Games.Battleship
{
    public class BattlershipBoard
    {
        public static string[,] battlership { get; set; } = new string[8, 8];

        public void ShowBoard()
        {
            Console.WriteLine("    0  | 1 | 2 | 3 | 4 | 5 | 6 | 7  ");
            Console.WriteLine();
            for (int j = 0; j < 8; j++)
            {
                Console.Write((char)('A' + j) + "   ");
                for (int x = 0; x < 8; x++)
                {
                    Console.Write(battlership[j, x] + "  ");
                }
                Console.WriteLine();
                Console.WriteLine("    ------------------------------------]");
            }

            //PositionVilanShip();
        }

        //public void PositionVilanShip()
        //{
        //    //pedaços
        //    battlership[2, 2] = "i";
        //    battlership[2, 6] = "i";
        //    battlership[6, 6] = "i";
        //    battlership[7, 7] = "i";

        //    //navio completo
        //    battlership[4, 2] = "i"; 
        //    battlership[4, 3] = "i";
        //    battlership[4, 4] = "i";
        //    battlership[4, 5] = "i";

        //    battlership[7, 1] = "i";
        //    battlership[7, 2] = "i";
        //    battlership[7, 3] = "i";
        //}
    }
}
