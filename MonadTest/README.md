# 💡 Functional Monads for C#

A lightweight functional programming library for C# that introduces **monads** like `Option` and `IO`, inspired by functional languages such as **Scala** and **Haskell**.

This library aims to make functional programming concepts — purity, composability, and effect isolation — accessible within the C# ecosystem.

---

## 📘 Table of Contents

* [Motivation](#-motivation)
* [Installation](#-installation)
* [Available Monads](#-available-monads)
  * [Option](#option)
  * [IO](#io)
* [Utility Functions](#-utility-functions)
* [Usage Examples](#-usage-examples)
* [Project Structure](#-project-structure)
* [Contributing](#-contributing)
* [License](#-license)

---

## 🚀 Motivation

C# is a powerful language but primarily imperative.
This library brings **functional programming** patterns that help you:

* Isolate **side effects** using `IO<T>`
* Safely handle **nullable values** with `Option<T>`
* Compose functions predictably and purely
* Eliminate `NullReferenceException` from your code

---

## ⚙️ Installation

Simply copy the following files into your project.
No external dependencies are required.

```
MonadTest/
 ├── Monads.cs
 ├── Option.cs
 ├── OptionExtensions.cs
 └── Program.cs
```

---

## 🧠 Available Monads

### `Option<T>`

Represents a value that **may or may not exist**.
`Some(value)` wraps a valid result, while `None` represents the absence of a value — removing the need for null checks.

```csharp
Option<int> number = Option<int>.Some(10);
Option<int> none = Option<int>.None;

var result = number
  .Map(n => n * 2)
  .FlatMap(n => Option<int>.Some(n + 5))
  .GetOrElse(0);

Console.WriteLine(result); // 25
```

#### Core Methods

| Method                  | Description                                     |
| ----------------------- | ----------------------------------------------- |
| `Map`                   | Applies a function to the inner value           |
| `FlatMap` / `Bind`      | Chains functions that return `Option`           |
| `GetOrElse`             | Returns a default value if `None`               |
| `Match`                 | Functional pattern matching for `Some` / `None` |
| `IsEmpty` / `IsDefined` | Checks presence of value                        |

### `IO<T>`

Encapsulates **side effects** (like reading a file, printing to console, or accessing a database) in a lazy, pure context.

```csharp
var attack = IO<int>.Of(() => 10);
var modifier = IO<int>.Of(() => 2);

var totalDamage = attack
  .FlatMap(a => modifier.Map(m => a * m))
  .Map(result => result + 5);

Console.WriteLine(totalDamage.Pure()); // 25
```

#### Core Methods

| Method            | Description                                        |
| ----------------- | -------------------------------------------------- |
| `Of`              | Creates a new `IO` from a side-effecting function  |
| `Map`             | Transforms the result without executing the effect |
| `FlatMap`         | Chains functions that return new `IO` instances    |
| `Fold` / `Reduce` | Combines multiple `IO` effects into one            |
| `Pure`            | Executes the effect and retrieves the result       |

---

## 🧩 Utility Functions

Common helpers that complement monadic behavior:

```csharp
var maybeInt = Option<int>.Some(5);
var incremented = maybeInt.Map(x => x + 1); // Some(6)
var defaulted = maybeInt.GetOrElse(0);      // 5

var none = Option<int>.None;
var fallback = none.GetOrElse(42);          // 42
```

Additional helpers for collections of `Option` and `IO` include `Collect()`, `Foreach()`, and `MatchLogic()`.

---

## 💡 Usage Examples

### Example 1: Chaining Safe Computations

```csharp
Option<int> Parse(string input) =>
    int.TryParse(input, out var n) ? Option<int>.Some(n) : Option<int>.None;

var result = Parse("10")
    .FlatMap(n => Parse((n * 2).ToString()))
    .Map(n => n + 1)
    .GetOrElse(-1);

Console.WriteLine(result); // 21
```

### Example 2: Composing IO Actions

```csharp
var program = IO<Unit>.Of(() => { Console.Write("Enter a number: "); return Unit.Default; })
    .FlatMap(_ => IO<int>.Of(() => int.Parse(Console.ReadLine())))
    .Map(number => number * 2);

Console.WriteLine(program.Pure());
```

### Example 3: Combining `IO<Option<T>>`

```csharp
IO<Option<int>>[] effects = new IO<Option<int>>[]
{
    IO<Option<int>>.Of(() => Option<int>.Some(3)),
    IO<Option<int>>.Of(() => Option<int>.Some(3)),
    IO<Option<int>>.Of(() => Option<int>.Some(3))
};

Option<int> result = effects.Fold(
    Option<int>.Some(0),
    (acc, io) => acc.FlatMap(a => io.Map(v => a + v))
);

Console.WriteLine(result.GetOrElse(0)); // 9
```

---

## 🧱 Project Structure

| File                  | Description                                                |
| --------------------- | ---------------------------------------------------------- |
| `Option.cs`           | Implementation of the `Option` monad                       |
| `OptionExtensions.cs` | Functional extensions for `Option`                         |
| `Monads.cs`           | Implementation of the `IO` monad and aggregation functions |
| `Program.cs`          | Demonstration of library usage                             |

---

## 🤝 Contributing

You are welcome to:

* Open issues for suggestions and improvements
* Submit pull requests
* Propose new monads such as `Either`, `Try`, or `Reader`

---

## 🧾 License

This project is released under the **MIT License**.
You are free to use, modify, and distribute it.

---

> ✨ *Write C# in a functional, pure, and elegant way.*
