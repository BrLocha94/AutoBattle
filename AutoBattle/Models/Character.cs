using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using static AutoBattle.Types;
using AutoBattle.Utils;

namespace AutoBattle.Models
{
    public class Character
    {
        public Action<Character> onDieEvent;

        public string Name { get; set; }
        
        public float Health { get; protected set; }
        public float BaseDamage { get; protected set; }
        public float DamageMultiplier { get; protected set; }
        public CharacterClass CharacterClass { get; protected set; }

        public GridBox currentBox { get; private set; }
        public int PlayerIndex { get; private set; }

        public Character(float Health, float BaseDamage, int PlayerIndex)
        {
            this.Health = Health;
            this.BaseDamage = BaseDamage;
            this.PlayerIndex = PlayerIndex;
        }

        public void SetCurrentBox(GridBox nextBox)
        {
            if (currentBox != null)
                currentBox.currentCharacter = null;

            currentBox = nextBox;
            currentBox.currentCharacter = this;
        }

        public virtual void TakeDamage(float amount)
        {
            if ((Health -= BaseDamage) <= 0)
            {
                Die();
                return;
            }

            return;
        }

        public virtual void PushAway(Grid battlefield, Directions direction)
        {
            GridBox gridBox = battlefield.GetFreeLocation(currentBox, direction);

            if(gridBox != null)
            {
                Console.Write($"Character {PlayerIndex} pushed away to {gridBox.xIndex} {gridBox.yIndex}\n");
                SetCurrentBox(gridBox);
                battlefield.DrawBattlefield();
            }
        }

        protected virtual void Die()
        {
            Console.Write($"Character {PlayerIndex} died\n");

            //Clear player from battlefield
            if (currentBox != null)
            {
                currentBox.currentCharacter = null;
                currentBox = null;
            }

            onDieEvent?.Invoke(this);
        }

        protected virtual void MoveTo(GridBox nextBox)
        {
            SetCurrentBox(nextBox);
            Console.WriteLine($"Player {PlayerIndex} walked to {currentBox.xIndex} {currentBox.yIndex}\n");
        }

        protected virtual void Attack(Character target)
        {
            int damage = Utilities.GetRandomInt(0, (int)BaseDamage);
            Console.WriteLine($"Player {PlayerIndex} is attacking the player {target.PlayerIndex} and did {BaseDamage} damage\n");
            
            target.TakeDamage(damage);
        }

        public void StartTurn(Grid battlefield)
        {
            GridBox targetBox = battlefield.CheckTargetsOnRange(currentBox, PlayerIndex);

            if (targetBox != null) 
            {
                Character target = targetBox.currentCharacter;

                Attack(target);

                if (target.Health <= 0) return;

                // Try to push away
                int random = Utilities.GetRandomInt(0, 100);

                if (random < 90)
                {
                    Directions direction = Directions.Null;

                    if (currentBox.xIndex < target.currentBox.xIndex)
                        direction = Directions.Right;
                    
                    else if (currentBox.xIndex > target.currentBox.xIndex)
                        direction = Directions.Left;
                    
                    else if (currentBox.yIndex < target.currentBox.yIndex)
                        direction = Directions.Down;
                    
                    else if (currentBox.yIndex > target.currentBox.yIndex)
                        direction = Directions.Up;

                    target.PushAway(battlefield, direction);
                    return;
                }
            }
            // if there is no target close enough, calculates in wich direction this character should move to be closer to a possible target
            else
            {
                targetBox = battlefield.FindTarget(currentBox, PlayerIndex);

                if (targetBox == null)
                {
                    //TODO: ACTIONS WHEN THERE IS NO TARGET
                    Console.WriteLine($"There are no targets for character {PlayerIndex} on {currentBox.xIndex} {currentBox.yIndex}\n");
                    return;
                }

                GridBox nextBox = null;

                //Try Walk left
                if (currentBox.xIndex > targetBox.xIndex)
                {
                    nextBox = battlefield.GetLocation(currentBox.xIndex - 1, currentBox.yIndex);

                    if(nextBox != null && !nextBox.IsOcupied)
                    {
                        MoveTo(nextBox);
                        battlefield.DrawBattlefield();
                        return;
                    }
                }
                //Try walk right
                else if(currentBox.xIndex < targetBox.xIndex)
                {
                    nextBox = battlefield.GetLocation(currentBox.xIndex + 1, currentBox.yIndex);

                    if (nextBox != null && !nextBox.IsOcupied)
                    {
                        MoveTo(nextBox);
                        battlefield.DrawBattlefield();
                        return;
                    }
                }
                
                //Try walk up
                if (currentBox.yIndex < targetBox.yIndex)
                {
                    nextBox = battlefield.GetLocation(currentBox.xIndex, currentBox.yIndex + 1);

                    if (nextBox != null && !nextBox.IsOcupied)
                    {
                        MoveTo(nextBox);
                        battlefield.DrawBattlefield();
                        return;
                    }
                }
                //Try walk down
                else if(currentBox.yIndex > targetBox.yIndex)
                {
                    nextBox = battlefield.GetLocation(currentBox.xIndex, currentBox.yIndex - 1);

                    if (nextBox != null && !nextBox.IsOcupied)
                    {
                        MoveTo(nextBox);
                        battlefield.DrawBattlefield();
                        return;
                    }
                }

                Console.WriteLine($"Player {PlayerIndex} has nowhere to move\n");
                return;
            }
        }
    }
}
