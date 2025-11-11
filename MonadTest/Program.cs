using System;
using System.Collections.Generic;


namespace MonadTest
{
    class MainClass 
    {
        public static void Main(string[] args) 
        {

          //basic example character hit
            const int player1CurrentAttack = 10;
            const int player2Defense = 10;
            const int player2SpecialDefense = 3;
            const int modifier = 2;
            const int playerBonus = 3;

           // must be const IO<Option<int>>
            var attackResult = IO<int>.Of(() => player1CurrentAttack)
            .FlatMap(attack => calcDamage(attack, modifier))
            .FlatMap(res => calcHitReduced(res, player2Defense))
            .FlatMap(hitReduced => generateAttackBonus(hitReduced, playerBonus))
            .Map(finalHit => triggerTargetSpecialDefense(finalHit, player2SpecialDefense, 3));

            // must be const
            var renderDamage = attackResult.Map(x => x.Match(
                some: v => v.ToString(),
                none: () => "MISS")
            );

            Console.WriteLine(renderDamage.Pure()); // Result? 


        }

        private static IO<int> calcDamage(int attack, int modifier)
        {
            return IO<int>.Of(() => attack * modifier);
        }

        private static IO<int> calcHitReduced(int playerAttack, int targetDefense)
        {
            return IO<int>.Of(() => playerAttack / targetDefense);
        }

        private static IO<int> generateAttackBonus(int hit, int bonus)
        {
            return IO<int>.Of(() => hit * bonus); 
        }

        private static Option<int> triggerTargetSpecialDefense(int attack, 
                                                               int sDefense, 
                                                               int modifierSpecial)
        {
            var damage = attack / (sDefense * modifierSpecial); 
            return damage > 0 ? Option<int>.Some(damage) : Option<int>.None; // just test
        }

    }
}
