using System;
using System.Collections.Generic;
using System.Text;
using static AutoBattle.Types;

namespace AutoBattle.Models.Characters
{
    public sealed class Archer : Character
    {
        public Archer(float health, float baseDamage, int characterIndex) : base(health, baseDamage, characterIndex)
        {
            CharacterClass = CharacterClass.Archer;
        }
    }
}
