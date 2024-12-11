using System;
using System.Linq;
using System.Collections.Generic;


namespace MonadTest
{
    class MainClass 
    {
        public static void Main(string[] args) 
        {

            // Option<int> option2 = Option<int>.None;

            //Option<int> option1 = Option<int>.Some(40);
            //var mappedOption = option1.Map(n => n.ToString());

            List<Option<string>> a = new List<Option<string>> { Option<string>.Some("dfga"), Option<string>.None, Option<string>.Some("bdfg") };
           // var z = a.Flatten<string, string>();


            ////var f = a.Select( x => x.Map<Option<string>>(y => x ?? Option<string>.Some("none")));

            //var s = a.Select(x => x.FlatMap(t => x));

            ////Console.WriteLine(mappedOption);
           // z.Foreach(x => Console.WriteLine(x));

            Option<int> optionNumber = Option<int>.Some(10); 
            Func<int, Option<string>> value = n => n == 11 ? Option<string>.Some("correct number") : Option<string>.None;
            Option<string> maybeResult = optionNumber.FlatMap(value);


            //string result = maybeResult.Match(
            //    some: v => v,
            //    none: () => "none"
            //);

           //var resultkk =  maybeResult.GetOrElse("kkkkk");

            //Console.WriteLine(resultkk);

            Option<int> option = Option<int>.Some(2);

            var result2 = option.MatchLogic(
                pattern: n => n==2,
                some: n => "some",
                none: () => "none"
            );

            //Console.WriteLine(result2);



            Option<int> option2 = Option<int>.Some(2);
           var t =  option2
            .Bind(num => Option<int>.Some(3))
            .Map(x => x * 3);

           

            Option<int> option3 = Option<int>.None;
            var b = option3
             .Bind(num => Option<int>.Some(3))
             .Map(x => x * 3)
             .FlatMap(f => Option<int>.Some( f + 33));

            var g = b.GetOrElse(0);
            //Console.WriteLine(g);

            //var g =  b.Match(
            //    some: v => "value " + v,
            //    none: () => "none"
            //);

            var listMonad = new List<Option<int>>{ t, b };
            //Console.WriteLine("list size => " + listMonad.Count);

            //var g = listMonad.Flatten<int, int>();

            //g.Foreach(x => Console.WriteLine(x));
            //Console.WriteLine(g);

            //var hello = IO<int>.Of(() => 2)
            //.Fold(0, (x, y) => x + y);
            //.Map(x => x + x)

            //var reduce = monadList.Reduce(0, (_, x) => _ + x);


            var monadList = new IO<Option<int>>[] { IO<Option<int>>.Of(() => Option<int>.Some(3)), IO<Option<int>>.Of(() => Option<int>.Some(3)), IO<Option<int>>.Of(() => Option<int>.Some(3)) };
            var h = monadList.Fold(Option<int>.Some(0), (_, y) => _.FlatMap(f => y.Map(z => f + z)));
            var result = h;
            Console.WriteLine(result.GetOrElse(0)); // print 9


           var string pp =  h.Match(
            some: v => "The value is: " + v,
            none: () => "None");

            Console.WriteLine(pp); // print The value is: 9

        }
    }
}
