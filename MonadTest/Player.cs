using System;
namespace MonadTest
{
    public class Player
    {
        private readonly int currentAttack;
        private readonly int defense;
        private readonly int specialDefense;
        private readonly int modifier;
        private readonly int bonus;

        public Player(int currentAttack, int defense, int specialDefense, int modifier, int bonus)
        {
            this.currentAttack = currentAttack;
            this.defense = defense;
            this.specialDefense = specialDefense;
            this.modifier = modifier;
            this.bonus = bonus;
        }

        public int CurrentAttack => currentAttack;
        public int Defense => defense;
        public int SpecialDefense => specialDefense;
        public int Modifier => modifier;
        public int Bonus => bonus;


        // only examples
        public static IO<int> calcDamage(int attack, int modifier)
        {
            return IO<int>.Of(() => attack * modifier);
        }

        public static IO<int> calcHitReduced(int playerAttack, int targetDefense)
        {
            return IO<int>.Of(() => playerAttack / targetDefense);
        }

        public static IO<int> generateAttackBonus(int hit, int bonus)
        {
            return IO<int>.Of(() => hit * bonus);
        }

        public static Option<int> triggerTargetSpecialDefense(int attack,
                                                               int sDefense,
                                                               int modifierSpecial)
        {
            var damage = attack / (sDefense * modifierSpecial);
            return damage > 0 ? Option<int>.Some(damage) : Option<int>.None;
        }
    }
}

