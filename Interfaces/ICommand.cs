namespace Monopoly.Interfaces
{
    public interface ICommand
    {
        public void Execute();

        public string Log();
    }
}
