using System.Collections.Generic;

namespace Framework.Command
{
    public class CommandManager
    {
        private readonly Stack<ICommand> _undoStack = new ();
        private readonly Stack<ICommand> _redoStack = new ();

        public void ExecuteCommand(ICommand command)
        {
            command.Execute();
            _undoStack.Push(command);
            _redoStack.Clear();
        }

        public void Undo()
        {
            if (_undoStack.Count == 0)
                return;

            ICommand command = _undoStack.Pop();
            command.Undo();
            _redoStack.Push(command);
        }

        public void Redo()
        {
            if (_redoStack.Count == 0)
                return;

            ICommand command = _redoStack.Pop();
            command.Execute();
            _undoStack.Push(command);
        }
    }
}
