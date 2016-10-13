using System.Collections.Generic;

namespace PlaytikaTestTask.Constants
{
    public enum AvailableActions : int { all = 0 , cpp, reversed1, reversed2 }
    public static class Constant
    {
        public static readonly Dictionary<string, AvailableActions> AvailableActionsDictionary;
        public const string destinationFileName = "results.txt";
        public const string cppAddString = " /";
        public const string cppSearchPattern = "*.cpp";

        static Constant()
        {
            AvailableActionsDictionary = new Dictionary<string, AvailableActions>()
            {
                { "all", AvailableActions.all },
                { "cpp", AvailableActions.cpp },
                { "reversed1", AvailableActions.reversed1 },
                { "reversed2", AvailableActions.reversed2 }
            };
        }
    }
}
