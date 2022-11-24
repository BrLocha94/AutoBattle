using System;
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
            
            Character PlayerCharacter = null;
            Character EnemyCharacter = null;
            Character WinnerCharacter = null;

            List<Character> AllPlayers = new List<Character>();
            
            int currentTurn = 0;

            const int PlayerIndex = 0;
            const int EnemyIndex = 1;

            GameStates currentGameState = GameStates.Starting;

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
                PlayerCharacter.onDieEvent += HandleCharacterDead;

                CreateEnemyCharacter();
            }

            void CreateEnemyCharacter()
            {
                //randomly choose the enemy class and set up vital variables
                CharacterClass enemyClass = (CharacterClass)Utilities.GetRandomInt(1, 4);
                Console.WriteLine($"Enemy Class Choice: {enemyClass}");
                
                EnemyCharacter = CharacterFactory.CreateCharacter(enemyClass, EnemyIndex);
                EnemyCharacter.onDieEvent += HandleCharacterDead;

                StartGame();
            }

            void StartGame()
            {
                AllPlayers.Add(PlayerCharacter);
                AllPlayers.Add(EnemyCharacter);
                AllPlayers.ShuffeList();

                AlocatePlayers();

                Console.WriteLine($"\nBATTLE START!!!\n");
                Console.Write(Environment.NewLine + Environment.NewLine);
                
                currentGameState = GameStates.Running;
                StartTurn();
            }

            void StartTurn()
            {
                foreach (Character character in AllPlayers)
                {
                    //Dont need to start next turn if game is over
                    if (currentGameState != GameStates.Running)
                        break;

                    //If character is dead dont need to start a turn
                    if(character.Health > 0)
                        character.StartTurn(grid);
                }

                if (currentGameState == GameStates.GameOver)
                {
                    FinishGame();
                    return;
                }

                currentTurn++;
                HandleTurn();
            }

            void HandleTurn()
            {
                Console.Write(Environment.NewLine + Environment.NewLine);
                Console.WriteLine("Click on any key to start the next turn...\n");
                Console.Write(Environment.NewLine + Environment.NewLine);

                ConsoleKeyInfo key = Console.ReadKey();
                StartTurn();
            }

            void FinishGame()
            {
                Console.Write(Environment.NewLine + Environment.NewLine);
                Console.Write($"Battle finished: Character {WinnerCharacter.PlayerIndex} won the battle\n");
                Console.Write(Environment.NewLine + Environment.NewLine);
                Console.WriteLine("Click on any key to close game...\n");

                ConsoleKeyInfo key = Console.ReadKey();
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

            void HandleCharacterDead(Character target)
            {
                //Unregister event to avoid null pointer error
                target.onDieEvent -= HandleCharacterDead;

                Character winner = null;

                foreach(Character character in AllPlayers)
                {
                    if(character.Health > 0)
                    {
                        if (winner == null)
                        {
                            winner = character;
                            continue;
                        }

                        return;
                    }
                }

                // Only 1 character is alive
                WinnerCharacter = winner;
                currentGameState = GameStates.GameOver;
            }
        }
    }
}
