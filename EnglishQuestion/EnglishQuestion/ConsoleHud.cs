﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using EnglishQuestion.Logical;

namespace EnglishQuestion
{
    class ConsoleHud
    {       
        private int _baseLenght;
        private bool _twoPlayers;
        private bool _rus;
        private IPlayer _player;
        private LogicalClass logicLogic;

        string[] variants = new string[4];
        WordsDataBase[] wdbvariants = new WordsDataBase[4];

        public ConsoleHud(WordsDataBase[] wdb, int len, bool rus, bool two)
        {
            Console.WriteLine("Вас приветствует Тюлень и его произведение.\n" +
                              "Данная игра сделана с целью подкрепления знаний в Английском языке.\n" +
                              "На данный момент в базе данных содержится {0} пар слов\n" +
                              "Никогда не поздно пополнить их"
                              , len);

            _baseLenght = len;
            _rus = rus;
            _twoPlayers = two;
            logicLogic = new LogicalClass(wdb,len, true);

        }

        public void GetStartGame()
        {
            int count = 0;
            string nameOne = Console.ReadLine();
            string nameTwo = Console.ReadLine();
            logicLogic.StartGame(nameOne,nameTwo,10);
            do
            {                
                Console.Clear();
                Console.WriteLine("Количество очков:");
                Console.WriteLine(GetInfoPlayer() +"\n");
                count++;
                Console.WriteLine("Moves"+count);
                MovePlayer();
                Console.ReadLine();
            } while (count < 15);
        }

        public void MovePlayer()
        {
            if(_rus)
                RusQuestion();
        }

        private void RusQuestion()
        {         
                variants = logicLogic.GetdVariants();
            if (variants != null)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Ходит игрок ");
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write(logicLogic.GetInfoPlayer().PlayerInfo.Name);

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n\n\nКак переводится слово {0}", logicLogic.GetWord());

                for (int i = 0; i < wdbvariants.Length; i++)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("{0})  {1}", i + 1, variants[i]);
                }

                ChoseVariants();
            }
            else Console.WriteLine("Игра окончена");
        }

        private void ChoseVariants()
        {
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.D1:
                    logicLogic.Move(variants[0]);
                    break;
                case ConsoleKey.D2:
                    logicLogic.Move(variants[1]);
                    break;
                case ConsoleKey.D3:
                    logicLogic.Move(variants[2]);
                    break;
                case ConsoleKey.D4:
                    logicLogic.Move(variants[3]);
                    break;
                default:
                    Console.WriteLine(" <---Неправильно введен символ!");
                    ChoseVariants();
                    break;
            }
        }

        public string GetInfoPlayer()
        {
            if(_twoPlayers)
                return logicLogic.GetInfoPlayer().PlayerOneInfo.Name + " :: " + logicLogic.GetInfoPlayer().PlayerOneInfo.Scores +
                    "       " + logicLogic.GetInfoPlayer().PlayerTwoInfo.Name + " :: " + logicLogic.GetInfoPlayer().PlayerTwoInfo.Scores;
            return logicLogic.GetInfoPlayer().PlayerInfo.Name + " :: " + logicLogic.GetInfoPlayer().PlayerInfo.Scores;
        }
    }
}
