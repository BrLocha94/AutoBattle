using System;
using System.Collections.Generic;
using System.Text;
using AutoBattle.Models;
using static AutoBattle.Types;

namespace AutoBattle.Utils
{
    public static class CharacterFactory
    {
        static float DefaultHealth = 100f;
        static float DefaultBaseDamage = 20;

        public static Character CreateCharacter(CharacterClass characterClass, int PlayerIndex)
        {
            Character character = new Character(characterClass, DefaultHealth, DefaultBaseDamage, PlayerIndex);
            return character;
        }
    }
}
