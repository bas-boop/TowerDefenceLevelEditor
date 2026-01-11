namespace Framework.Command
{
    public class CommandSystem : Singleton<CommandSystem>
    {
        private readonly CommandManager _manager = new ();
        
        public void Execute(ICommand command) => _manager.ExecuteCommand(command);
        
        public void Undo() => _manager.Undo();
        
        public void Redo() => _manager.Redo();
    }
}