namespace ProgrammingDemo
{
    // 1) An interface called IMode that consists of at least a function int Execute()
    // and a function void Initialize(Dictionary<string,int>)
    // Note: Execute method doesn't require to return anything so keeping it as void return type.
    public interface IMode
    {
        void Initialize();
        void Execute();
    }
}
