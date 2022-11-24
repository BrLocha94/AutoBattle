using System;
using System.Collections.Generic;
using System.Text;

namespace AutoBattle
{
    public class Types
    {
        public struct CharacterClassSpecific
        {
            CharacterClass CharacterClass;
            float hpModifier;
            float ClassDamage;
            CharacterSkills[] skills;
        }

        public struct CharacterSkills
        {
            string Name;
            float damage;
            float damageMultiplier;
        }

        public enum CharacterClass : uint
        {
            Paladin = 1,
            Warrior = 2,
            Cleric = 3,
            Archer = 4
        }

        public enum GameStates : uint
        {
            Starting = 1,
            Running = 2,
            GameOver = 3
        }

        public enum Directions : uint
        {
            Null = 0,
            Up = 1,
            Down = 2,
            Left = 3,
            Right = 4
        }
    }
}
