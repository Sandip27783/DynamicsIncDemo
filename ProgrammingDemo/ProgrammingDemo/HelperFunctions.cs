namespace ProgrammingDemo
{
    public static class HelperFunctions
    {
        public const string delimeter = "|", startKey = "START", endKey = "END";
        public static bool IsPrime(int input)
        {
            if (input <= 1)
                return false;

            for (int i = 2; i * i <= input; i++)
            {
                if (input % i == 0)
                    return false;
            }
            return true;
        }
    }
}
