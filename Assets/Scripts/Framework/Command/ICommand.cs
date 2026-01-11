namespace Framework.Command
{
    public interface ICommand
    {
        void Execute(bool runtime = false);
        void Undo();
    }
}