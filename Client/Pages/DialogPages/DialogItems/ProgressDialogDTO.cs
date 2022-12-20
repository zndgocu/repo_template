using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blazor_wasm.Client.Pages.DialogPages.DialogItems
{
    public class ProgressDialogDTO
    {
        public bool IsVisible { get; set; } = false;
        public int ProgressValue { get; set; } = 0;
        public int Value { get; set; } = 0;
        public int MinValue { get; set; } = 0;
        public int MaxValue { get; set; } = 0;

        public List<string> TextContent = new();

        public void AddMaxValue(int addValue)
        {
            MaxValue += addValue;
        }

        public void AddFirstTextContent(string addText)
        {
            TextContent.Insert(0, addText);
        }

        public void AddValueOne()
        {
            Value++;
            ProgressValue = Value;
        }

        public void Show()
        {
            Clear();
            IsVisible = true;
        }
        public void UnShow()
        {
            IsVisible = false;
        }

        public void Complete()
        {
            TextContent.Insert(0, $"complete {Value} to {MaxValue}");
            Value = MaxValue;
        }

        public void Clear()
        {
            IsVisible = false;
            ProgressValue = 0;
            Value = 0;
            MinValue = 0;
            MaxValue = 0;
            TextContent.Clear();
        }
    }
}