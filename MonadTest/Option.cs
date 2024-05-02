using System;
namespace MonadTest
{


    public abstract class Option<T>
    {

        public abstract bool IsSome { get; }
        public abstract T Get();
        public abstract bool isEmpty { get; }

        public bool nonEmpty()
        {
            return isDefined();
        }

        public bool isDefined()
        {
            return !isEmpty;
        }

        public T GetOrElse(T defaultValue) => isEmpty ? defaultValue : Get();

        public static Option<T> None => new NoneOption<T>();
        public static Option<T> Some(T value) => new SomeOption<T>(value);

        //public T OrNull() => isEmpty ? T : Get();

        public R Match<R>(Func<T, R> some, Func<R> none)
        {
            if (IsSome)
                return some(Get());
            else
                return none();
        }

        public Option<R> Bind<R>(Func<T, Option<R>> func)
        {
            if (IsSome)
            {
                return func(Get());
            }
            else
            {
                return Option<R>.None;
            }
        }

    }

    public class NoneOption<T> : Option<T> 
    {
        public override bool IsSome => false;
        public override T Get() => throw new InvalidOperationException("None has no value");
        public override bool isEmpty => true;
    
    }


    public class SomeOption<T> : Option<T>
    {
        private readonly T _value;

        public SomeOption(T value)
        {
            _value = value;
        }

        public override bool IsSome => true;
        public override T Get() => _value;
        public override bool isEmpty => false;
    }


}
