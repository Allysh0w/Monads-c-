using System;
using System.Collections.Generic;
namespace MonadTest
{
    public class IO<T>
    {
        private readonly Func<T> _effect;

        private IO(Func<T> effect)
        {
            _effect = effect;
        }

        public static IO<T> Of(Func<T> effect)
        {
            return new IO<T>(effect);
        }

        public T Pure()
        {
            return _effect();
        }

        public IO<R> Map<R>(Func<T, R> func)
        {
            return IO<R>.Of(() => func(_effect()));
        }

        public IO<R> FlatMap<R>(Func<T, IO<R>> func)
        {
            return IO<R>.Of(() => func(_effect()).Pure());
        }

        public T Reduce(T seed, Func<T, T, T> func)
        {       
            return func(seed, _effect());
        }

        public T Fold(T seed, Func<T, T, T> func)
        {         
            return func(seed, _effect());
        }
    }

    public static class IOExtensions
    {
        public static T Reduce<T>(this IEnumerable<IO<T>> source, T seed, Func<T, T, T> func)
        {
            T result = seed;
            foreach (var io in source)
            {
                result = func(result, io.Pure());
            }
            return result;
        }

        public static T Fold<T>(this IEnumerable<IO<T>> source, T seed, Func<T, T, T> func)
        {
            T result = seed;
            foreach (var io in source)
            {
                result = func(result, io.Pure());
            }
            return result;
        }
    }
}
