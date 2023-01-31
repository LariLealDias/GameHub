using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_Hub_de_Jogos.Service.Players
{
    public class Player
    {
        public string Name { get; set; }
        public string Key { get; set; }
        public int GameScoreTicTacToe { get; set; } = 0;
        public int GameScoreBattleship { get; set; }

        public void SetName(string playerName)
        {
            Name = playerName;
        }
        public void SetKey(string keyPlayer)
        {
            Key = keyPlayer;
        }

        public string GetLoginName()
        {
            return Name;
        }

        public string GetLoginKey()
        {
            return Key;
        }
    }
}
