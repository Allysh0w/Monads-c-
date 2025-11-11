using System;
using System.Collections.Generic;


namespace MonadTest
{
    class MainClass 
    {
        public static void Main(string[] args) 
        {

            var player1 = new Player(currentAttack: 10,
                                     defense: 1,
                                     specialDefense: 1,
                                     modifier: 2,
                                     bonus: 3);

            var player2 = new Player(currentAttack: 10,
                                     defense: 10,
                                     specialDefense: 3,
                                     modifier: 2,
                                     bonus: 3);

            // must be const IO<Option<int>>
            var attackResult = IO<int>.Of(() => player1.CurrentAttack)
            .FlatMap(attack => Player.calcDamage(attack, player2.Modifier))
            .FlatMap(res => Player.calcHitReduced(res, player2.Defense))
            .FlatMap(hitReduced => Player.generateAttackBonus(hitReduced, player1.Bonus))
            .Map(finalHit => Player.triggerTargetSpecialDefense(finalHit, player2.SpecialDefense, player2.Bonus));

            // must be const
            var renderDamage = attackResult.Map(x => x.Match(
                some: v => v.ToString(),
                none: () => "MISS")
            );

            Console.WriteLine(renderDamage.Pure());


        }

    }
}
