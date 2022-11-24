using System;
using System.Collections.Generic;
using System.Text;
using static AutoBattle.Types;

namespace AutoBattle.Models.Characters
{
    public sealed class Cleric : Character
    {
        public Cleric(float health, float baseDamage, int characterIndex) : base(health, baseDamage, characterIndex)
        {
            CharacterClass = CharacterClass.Cleric;
        }
    }
}
