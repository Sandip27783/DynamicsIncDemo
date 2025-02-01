namespace ProgrammingDemo
{
    public class PrimeMode : IMode
    {
        private readonly int _start;
        private readonly int _end;
        private readonly Dictionary<string, int> _inputValues;
        
        public PrimeMode(int start, int end)
        {
            _start = start;
            _end = end;
            _inputValues = new Dictionary<string, int>();
            Initialize();
        }

        // Note: It can be done from constructer.
        public void Initialize()
        {
            // Initialize the dictionary with Start and End keys as per ask.
            _inputValues.Add(HelperFunctions.startKey, _start);
            _inputValues.Add(HelperFunctions.endKey, _end);
        }

        public void Execute()
        {
            var startVal = _inputValues[HelperFunctions.startKey];
            var endVal = _inputValues[HelperFunctions.endKey];

            if (startVal <= 0 || endVal <= 0)
            {
                Console.WriteLine("Numbers must be Positive.");
            }
            else if (startVal > endVal)
            {
                Console.WriteLine("Start value should be smaller than end value.");
            }
            else
            {
                var primeNumberCounter = 0;

                for (int i = startVal+1; i < endVal; i++)
                {
                    if (HelperFunctions.IsPrime(i))
                    {
                        primeNumberCounter++;
                    }
                }

                Console.WriteLine($"start: {startVal}  end: {endVal}        retVal: {primeNumberCounter}");
            }
        }        
    }
}
