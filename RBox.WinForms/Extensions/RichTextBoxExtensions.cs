using System.Drawing;
using System.Windows.Forms;

namespace RBox.WinForms.Extensions
{
    public static class RichTextBoxExtensions
    {
        public static void AppendLine(this RichTextBox richTextBox, string text, Color color)
        {
            richTextBox.SelectionStart = richTextBox.TextLength;
            richTextBox.SelectionLength = 0;
            richTextBox.SelectionColor = color;
            richTextBox.AppendText(text + System.Environment.NewLine);
            richTextBox.SelectionColor = richTextBox.ForeColor;
        }
    }
}
