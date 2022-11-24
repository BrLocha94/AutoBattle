﻿using System;
using System.Collections.Generic;
using System.Linq;
using AutoBattle.Utils;
using AutoBattle.Models;
using static AutoBattle.Types;

namespace AutoBattle
{
    class Program
    {
        static void Main(string[] args)
        {
            Grid grid = new Grid(5, 5);
            
            Character PlayerCharacter;
            Character EnemyCharacter;

            List<Character> AllPlayers = new List<Character>();
            
            int currentTurn = 0;

            const int PlayerIndex = 0;
            const int EnemyIndex = 1; 

            Setup(); 

            void Setup()
            {
                GetPlayerChoice();
            }

            void GetPlayerChoice()
            {
                //asks for the player to choose between for possible classes via console.
                Console.WriteLine("Choose Between One of this Classes:\n");
                Console.WriteLine("[1] Paladin, [2] Warrior, [3] Cleric, [4] Archer");
                
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CreatePlayerCharacter(Int32.Parse(choice));
                        break;
                    case "2":
                        CreatePlayerCharacter(Int32.Parse(choice));
                        break;
                    case "3":
                        CreatePlayerCharacter(Int32.Parse(choice));
                        break;
                    case "4":
                        CreatePlayerCharacter(Int32.Parse(choice));
                        break;
                    default:
                        GetPlayerChoice();
                        break;
                }
            }

            void CreatePlayerCharacter(int classIndex)
            {
                CharacterClass characterClass = (CharacterClass)classIndex;
                Console.WriteLine($"Player Class Choice: {characterClass}");

                PlayerCharacter = CharacterFactory.CreateCharacter(characterClass, PlayerIndex);
                
                CreateEnemyCharacter();
            }

            void CreateEnemyCharacter()
            {
                //randomly choose the enemy class and set up vital variables
                CharacterClass enemyClass = (CharacterClass)Utilities.GetRandomInt(1, 4);
                Console.WriteLine($"Enemy Class Choice: {enemyClass}");
                
                EnemyCharacter = CharacterFactory.CreateCharacter(enemyClass, EnemyIndex);
                
                StartGame();
            }

            void StartGame()
            {
                //populates the character variables and targets
                AllPlayers.Add(PlayerCharacter);
                AllPlayers.Add(EnemyCharacter);
                AlocatePlayers();
                StartTurn();
            }

            void StartTurn(){

                if (currentTurn == 0)
                {
                    AllPlayers.ShuffeList();
                    Console.WriteLine($"\nBATTLE START!!!\n");

                    Console.WriteLine("Click on any key to start the turn...\n");
                    Console.Write(Environment.NewLine + Environment.NewLine);
                    ConsoleKeyInfo key = Console.ReadKey();
                }

                foreach(Character character in AllPlayers)
                {
                    character.StartTurn(grid);
                }

                currentTurn++;
                HandleTurn();
            }

            void HandleTurn()
            {
                if(PlayerCharacter.Health == 0)
                {
                    Console.Write(Environment.NewLine + Environment.NewLine);

                    Console.Write($"Game over: Player {PlayerCharacter.PlayerIndex} died\n");

                    Console.Write(Environment.NewLine + Environment.NewLine);

                    Console.WriteLine("Click on any key to close game...\n");
                    ConsoleKeyInfo key = Console.ReadKey();
                    return;
                } 
                else if (EnemyCharacter.Health == 0)
                {
                    Console.Write(Environment.NewLine + Environment.NewLine);

                    Console.Write($"Game clear: Enemy {PlayerCharacter.PlayerIndex} died\n");

                    Console.Write(Environment.NewLine + Environment.NewLine);

                    Console.WriteLine("Click on any key to close game...\n");
                    ConsoleKeyInfo key = Console.ReadKey();
                    return;
                } 
                else
                {
                    Console.Write(Environment.NewLine + Environment.NewLine);
                    Console.WriteLine("Click on any key to start the next turn...\n");
                    Console.Write(Environment.NewLine + Environment.NewLine);

                    ConsoleKeyInfo key = Console.ReadKey();
                    StartTurn();
                }
            }

            void AlocatePlayers()
            {
                AlocateCharacter(PlayerCharacter);
                AlocateCharacter(EnemyCharacter);
                grid.DrawBattlefield();
            }

            void AlocateCharacter(Character target)
            {
                GridBox location = grid.GetRandomFreeLocation();
                Console.Write($"Character {target.PlayerIndex} location: [{location.xIndex}] [{location.yIndex}]\n");

                target.SetCurrentBox(location);
            }
        }
    }
}
