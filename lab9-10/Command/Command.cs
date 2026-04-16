using System;
using System.Collections.Generic;

namespace CommandTask
{
    public class Document
    {
        public string Text { get; set; } = "";
        public string Clipboard { get; set; } = "";

        public void Print()
        {
            Console.WriteLine($"[Текст документа]: {Text}");
        }
    }

    public abstract class Command // Command
    {
        protected Document _document;

        public Command(Document document)
        {
            _document = document;
        }

        public abstract void Execute();
        public abstract void Undo();
    }

    public class InsertCommand : Command // ConcreteCommand
    {
        private string _insertedText;

        public InsertCommand(Document doc, string text) : base(doc)
        {
            _insertedText = text;
        }

        public override void Execute()
        {
            _document.Text += _insertedText;
        }

        public override void Undo()
        {
            _document.Text = _document.Text.Substring(0, _document.Text.Length - _insertedText.Length);
        }
    }

    public class DeleteCommand : Command 
    {
        private int _length;
        private string _deletedText;

        public DeleteCommand(Document doc, int length) : base(doc)
        {
            _length = length;
        }

        public override void Execute()
        {
            if (_document.Text.Length >= _length)
            {
                _deletedText = _document.Text.Substring(_document.Text.Length - _length);
                _document.Text = _document.Text.Substring(0, _document.Text.Length - _length);
            }
        }

        public override void Undo()
        {
            if (_deletedText != null)
            {
                _document.Text += _deletedText;
            }
        }
    }

    public class CopyCommand : Command
    {
        private string _previousClipboard;

        public CopyCommand(Document doc) : base(doc) { }

        public override void Execute()
        {
            _previousClipboard = _document.Clipboard;
            _document.Clipboard = _document.Text;
            Console.WriteLine($"[Система]: Текст скопiйовано до буфера обмiну.");
        }

        public override void Undo()
        {
            _document.Clipboard = _previousClipboard;
        }
    }

    public class TextEditor
    {
        private Document _document = new Document();

        private Stack<Command> _undoStack = new Stack<Command>();
        private Stack<Command> _redoStack = new Stack<Command>();

        public void ExecuteCommand(Command command)
        {
            command.Execute();
            _undoStack.Push(command);
            _redoStack.Clear();       
        }

        public void Undo()
        {
            if (_undoStack.Count > 0)
            {
                Command command = _undoStack.Pop();
                command.Undo();
                _redoStack.Push(command);
                Console.WriteLine("\n--- Вiдмiна останньої дiї (Undo) ---");
            }
        }

        public void Redo()
        {
            if (_redoStack.Count > 0)
            {
                Command command = _redoStack.Pop();
                command.Execute();
                _undoStack.Push(command);
                Console.WriteLine("\n--- Повторення скасованої дiї (Redo) ---");
            }
        }

        public void ShowDocument()
        {
            _document.Print();
        }

        public Document GetDocument() => _document;
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Прототип текстового редактора (Шаблон Command) ===\n");

            TextEditor editor = new TextEditor();

            editor.ExecuteCommand(new InsertCommand(editor.GetDocument(), "Породи котiв: "));
            editor.ExecuteCommand(new InsertCommand(editor.GetDocument(), "Мейн-кун, Сiамська, Перська, Британська короткошерста, Шотландська капловуха, Сфiнкс"));
            editor.ShowDocument();

            editor.ExecuteCommand(new CopyCommand(editor.GetDocument()));

            editor.ExecuteCommand(new DeleteCommand(editor.GetDocument(), 57));
            editor.ShowDocument();

            editor.Undo();
            editor.ShowDocument();

            editor.Redo();
            editor.ShowDocument();

            Console.ReadKey();
        }
    }
}