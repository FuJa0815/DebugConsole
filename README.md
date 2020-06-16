# DebugConsole
A framework for managing debugging from a console

This framework is used to execute debug input.
It allows you to define methods as debug executable and then execute them in the console.
You can also define properies as debug readable and writeable.

## Getting Started
DebugConsole has some predefined debug consoles for taking care of your input and output. See Predefined Debug Consoles.

You must set a public static property to get started.
You can use your own implementation for handling input and output, or you can use one of the predefined implementations.

In this example, we define the Console as our DebugHandler.
> DebugCon.Handler = new ConsoleDebugHandler();

Next, we have to define static Methods as `DebugFunctions`. This allows you to execute Methods from your console.

You can also define static Properties as `DebugPropeties`. This allows you to read and (optionally) write from and to your property.

## Example

```c#
class Program
{
   static void Main(string[] args)
   {
        DebugCon.Handler = new ConsoleDebugHandler();
        Thread.Sleep(Timeout.Infinite);
   }

   [DebugFunction]
   public static void MyFunction(string a1, int i1)
   {
       for(int i = 0; i < i1; i++)
            DebugConsole.WriteLine("You executed me with " + a1 + ". The first property is " + Text + ", the second one is " + ReadOnlyProperty);
   }

   [DebugProperty]
   public static string Text { get; set; } = "foo";

   [DebugProperty("Text2", false)]
   public static string ReadOnlyProperty { get; set; } = "bar";
}
```

This example allows us to type the following commands into the console
Input | Output
------|-------
`MyFunction hi 2` | `You executed me with hi. The first property is foo, the second one is bar`<br>`You executed me with hi. The first property is foo, the second one is bar`
`MyFunction hi` | `Useage for MyFunction`<br>`a1 typeof System.String`<br>`i1 typeof System.Int32`
`Get Text` | `Get returned foo`
`Get Program.Text` | `Get returned foo`
`Get Text2` | `Get returned bar`
`Get ReadOnlyProperty` | `Property ReadOnlyProperty not found`
`Set Text wow` | 
`Set Text2 wow2` | `Property Text2 is readonly`
`MyFunction h 1` | `You executed me with h. The first property is wow, the second one is bar`

## Predefined Debug Consoles
* `DebugConsole.Implementations.Console.ConsoleDebugHandler`
* `DebugConsole.Implementations.WPF.TextBoxItemsControlDebugHandler`
* `DebugConsole.Implementations.Misc.FileDebugHandler` (Readonly)
* `DebugConsole.Implementations.Misc.IdeDebugHandler` (Readonly)

## Custom Debug Console
If you want to create a custom Debug Console (e.g. somewhere hidden in your GUI or in a database) you need to create a class which implements inherits [`DebugConsoleHandler`](https://github.com/FuJa0815/DebugConsole/blob/master/DebugConsole/DebugConsoleHandler.cs).

See xml-summaries in the source code for further information.
You have to override:
* `Write(string)`
* `WriteLine(string)`
* `WriteError(string)`

You may override:
* `ReadLoop()`

You need to call `OnInput(string)` if you got input from the user.
