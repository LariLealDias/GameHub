using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using Intro;
using System.Numerics;
using System.Collections;
using System.Xml.Linq;
using Projeto_Hub_de_Jogos.Service.Players;

namespace Projeto_Hub_de_Jogos.Repository
{
    public class JsonRepository
    {
        public string jsonFile = @"C:\Users\laril\OneDrive\Documentos\Ima - Sharp Coders\Projetos\Projeto Hub\Projeto Hub de Jogos\Projeto Hub de Jogos\Repository\PlayerJson.json";
        public string playerJson;
        public List<Player> list;
        public Player player;


        //serializando
        public void Save(List<Player> obj)
        {
            var option = new JsonSerializerOptions { WriteIndented = true };
            this.playerJson = JsonSerializer.Serialize(obj, option);
            File.WriteAllText(this.jsonFile, this.playerJson);
                                //Parametro = 1ºcaminho - e 2º o que quer salvar nesse caminho
        }

        //verificando
        public void VerifyJsonFile()
        {
            if (File.Exists(this.jsonFile))
            {
                try
                {
                    string json = File.ReadAllText(this.jsonFile);
                    JsonSerializer.Deserialize<List<Player>>(json);
                }
                catch (JsonException)
                {
                    File.Delete(jsonFile);
                    File.Create(jsonFile).Dispose();
                    Save(new List<Player>());
                }
            }
            else
            {
                File.Create(jsonFile).Dispose();
                Save(new List<Player>());
            }
        }

        //deserializando
        public List<Player> Read()
        {
            string jsonLines = File.ReadAllText(jsonFile);
            List<Player> objectFile = JsonSerializer.Deserialize<List<Player>>(jsonLines);
            return objectFile;
        }

        //permanecendo os dados em uma lista
        public void RegisterInList(Player player)
        {
           this.list = new List<Player>();

            list = this.Read();
            bool exist = false;

            foreach(Player i in list)
            {
                if(i.Name == player.Name)
                {
                    exist = true;
                    break;
                }
            }

            if (!exist)
            {
                list.Add(player);
                Save(list);
            }
        }

        //encontrar se existe a conta
        public bool LoginInList(string name, string key)
        {
            this.list = new List<Player>();

            list = this.Read();
            bool exist = false;

            foreach (Player i in list)
            {
                if (i.Name == name && i.Key == key)
                {
                    exist = true;
                    player = i;
                    break;
                }
            }
            return exist;
        }

        //encontrar a posição do score de cada game 
        public void GameScoreTicTacToeInList(string name)
        {
            this.list = Read();
            int position = this.list.FindIndex(p => p.Name == name);
            if (position != -1)
            {
                this.list[position].GameScoreTicTacToe++;
                Save(this.list);
            }
        }

        public void GameScoreBattleShipInList(string name)
        {
            this.list = Read();
            int position = this.list.FindIndex(p => p.Name == name);
            if (position != -1)
            {
                this.list[position].GameScoreBattleship++;
                Save(this.list);
            }
        }

        public Player GetDataPlayer()
        {
            return player;
        }

        public Player GetPlayerScore(string name)
        {
            this.list = Read();
            int position = this.list.FindIndex(p => p.Name == name);

            return this.list[position];
        }
    }
}
