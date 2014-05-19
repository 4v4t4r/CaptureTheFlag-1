using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Interactivity;

namespace CaptureTheFlag.Behaviors
{
    public class UpdateSourceOnTextChangedBehavior : Behavior<TextBox>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.TextChanged += OnTextChanged;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.TextChanged -= OnTextChanged;
            base.OnDetaching();
        }

        private void OnTextChanged(object sender, TextChangedEventArgs args)
        {
            var textBox = (sender as TextBox);
            if (textBox != null)
            {
                BindingExpression binding = textBox.GetBindingExpression(TextBox.TextProperty);
                if (binding != null)
                    binding.UpdateSource();
            }
        }
    }
}
