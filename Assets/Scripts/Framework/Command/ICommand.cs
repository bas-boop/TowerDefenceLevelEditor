namespace Framework.Command
{
    public interface ICommand
    {
        void Execute();
        void Undo();
    }
}