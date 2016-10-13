using PlaytikaTestTask.BI.Interfaces;
using System;
using System.Linq;

namespace PlaytikaTestTask.BI.Implementation
{
    public class Reversed2FileNameTransformer : IFileNameTransformer
    {
        public string Transform(string fileName)
        {
            if (fileName == null)
                throw new ArgumentNullException();
            return new String(fileName.Reverse().ToArray());
        }
    }
}
