using System;
using System.Collections.Generic;
using System.Linq;

namespace MonadTest
{
    public static class OptionExtensions 
    {
        public static Option<U> FlatMap<T, U>(this Option<T> option, Func<T, Option<U>> mapper) 
        {
            return option.IsSome ? mapper(option.Get()) : Option<U>.None;
        }

        public static Option<U> Map<T, U>(this Option<T> option, Func<T, U> mapper)
        {
            return option.IsSome ? Option<U>.Some(mapper(option.Get())) : Option<U>.None;
        }

        public static Option<U> Collect2<T, U>(Option<T> opt, Func<T, Option<U>> partialFun)
        {
            return opt.IsSome ? partialFun(opt.Get()) : Option<U>.None;
        }

        public static IEnumerable<T> Collect<T>(IEnumerable<Option<T>> options)
        {
            return options.Where(opt => opt.IsSome).Select(opt => opt.Get());
        }

        public static IEnumerable<TResult> MatchLogic<T, TResult>(this IEnumerable<Option<T>> source, Func<T, bool> pattern, Func<T, TResult> someFunc, Func<TResult> noneFunc)
        {
            return source.Select(opt =>
            {
                if (opt.IsSome && pattern(opt.Get()))
                {
                    return someFunc(opt.Get());
                }
                else
                {
                    return noneFunc();
                }
            });
        }

        public static U MatchLogic<T, U>(this Option<T> option, Func<T, bool> pattern, Func<T, U> some, Func<U> none)
        {
            if (option.IsSome && pattern(option.Get()))
            {
                return some(option.Get());
            }
            else
            {
                return none();
            }
        }


        //public static IEnumerable<T> Flatten<T, TResult>(this IEnumerable<Option<T>> source)
        //{
        //    return source.Select(opt =>
        //    {
        //        if (opt.IsSome)
        //        {
        //            return opt.Get();
        //        }
        //        else
        //        {
        //           return opt.OrNull();
                   
        //        }
        //    }).Where(x => x != null);
        //}


        public static void Foreach<T>(this IEnumerable<T> source, Action<T> someFunc)
        {
            foreach (var item in source)
            {
                someFunc(item);
            }
        }




    }
}
