using System;
using System.Collections.Generic;
using System.Text;
using AutoBattle.Models;
using AutoBattle.Models.Characters;
using static AutoBattle.Types;

namespace AutoBattle.Utils
{
    public static class CharacterFactory
    {
        public static Character CreateCharacter(CharacterClass characterClass, int PlayerIndex)
        {
            switch (characterClass)
            {
                case CharacterClass.Paladin:
                    return CreatePaladin(PlayerIndex);

                case CharacterClass.Warrior:
                    return CreateWarior(PlayerIndex);

                case CharacterClass.Cleric:
                    return CreateCleric(PlayerIndex);

                case CharacterClass.Archer:
                    return CreateArcher(PlayerIndex);
            }

            return null;
        }

        private static Character CreatePaladin(int PlayerIndex)
        {
            float health = 100f;
            float baseDamage = 20f;

            return new Paladin(health, baseDamage, PlayerIndex);
        }

        private static Character CreateWarior(int PlayerIndex)
        {
            float health = 100f;
            float baseDamage = 20f;

            return new Warrior(health, baseDamage, PlayerIndex);
        }

        private static Character CreateCleric(int PlayerIndex)
        {
            float health = 100f;
            float baseDamage = 20f;

            return new Cleric(health, baseDamage, PlayerIndex);
        }

        private static Character CreateArcher(int PlayerIndex)
        {
            float health = 100f;
            float baseDamage = 20f;

            return new Archer(health, baseDamage, PlayerIndex);
        }
    }
}
