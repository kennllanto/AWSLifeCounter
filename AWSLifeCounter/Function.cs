using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Amazon.Lambda.Core;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace AWSLifeCounter
{
    public class Function
    {
        public class Player
        {
            public string name { get; set; }
            public int life { get; set; }
            public int commanderDamageReceived { get; set; }
        }

        public class Game
        {
            public List<String> playerNames { get; set; }
            public int StartingLife { get; set; }
        }

        public List<Player> Players { get; set; }
        public int StartingLife { get; set; }

        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="input"></param>
        /// <param name="game"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public List<Player> CreateGame(Game game, ILambdaContext context)
        {
            Players = new List<Player>();
            StartingLife = game.StartingLife;
            foreach (string name in game.playerNames)
            {
                AddPlayer(name);
            }
            return Players;
        }

        public bool ReceiveDamage(string name, int damage, ILambdaContext context)
        {
            int playerIndexOf = Players.FindIndex(p => p.name.Equals(name));
            Players[playerIndexOf].life -= damage;
            return true;
        }

        public bool GainLife(string name, int lifeGain, ILambdaContext context)
        {
            int playerIndexOf = Players.FindIndex(p => p.name.Equals(name));
            Players[playerIndexOf].life -= lifeGain;
            return true;
        }

        private bool AddPlayer(string name)
        {
            Player player = new Player()
            {
                name = name,
                life = StartingLife
            };
            Players.Add(player);
            return true;
        }
    }
}
