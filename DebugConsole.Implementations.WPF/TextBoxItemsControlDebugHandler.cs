namespace DebugConsole.Implementations.WPF
{
    using System.Windows.Controls;

    /// <summary>
    ///   A <see cref="DebugConsole"/> handler for writing to a <see cref="ItemsControl"/> and reading from a <see cref="TextBox"/>.
    /// </summary>
    public class TextBoxItemsControlDebugHandler : DebugConsoleHandler
    {
        private readonly TextBox input;
        private readonly ItemsControl output;

        /// <summary>
        ///   Initializes a new instance of the <see cref="TextBoxItemsControlDebugHandler"/> class.
        /// </summary>
        public TextBoxItemsControlDebugHandler(TextBox input, ItemsControl output)
        {
            this.input = input;
            this.output = output;

            input.KeyDown += (s, e) =>
            {
                if (e.Key == System.Windows.Input.Key.Enter)
                {
                    OnInput(input.Text);
                    input.Clear();
                }
            };
        }

        public override void Write(string value) => Append(value);

        public override void WriteError(string value) => WriteLine(value);

        public override void WriteLine(string value)
        {
            Append(value);
            output.Items.Add(string.Empty);
        }

        private void Append(string value)
        {
            if (output.Items.Count == 0)
                output.Items.Add(string.Empty);
            output.Items[output.Items.Count - 1] = (output.Items[output.Items.Count - 1] as string) + value;
        }
    }
}
