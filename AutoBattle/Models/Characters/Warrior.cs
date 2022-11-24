using System;
using System.Collections.Generic;
using System.Text;
using static AutoBattle.Types;

namespace AutoBattle.Models.Characters
{
    public sealed class Warrior : Character
    {
        public Warrior(float health, float baseDamage, int characterIndex) : base(health, baseDamage, characterIndex)
        {
            CharacterClass = CharacterClass.Warrior;
        }
    }
}
