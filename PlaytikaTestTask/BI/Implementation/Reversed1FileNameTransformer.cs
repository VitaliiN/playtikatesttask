using PlaytikaTestTask.BI.Interfaces;
using System;
using System.Linq;

namespace PlaytikaTestTask.BI.Implementation
{
    public class Reversed1FileNameTransformer : IFileNameTransformer
    {
        public string Transform(string fileName)
        {
            if (fileName == null)
                throw new ArgumentNullException();
            return String.Join("\\", fileName.Split('\\').Reverse());
        }
    }
}
