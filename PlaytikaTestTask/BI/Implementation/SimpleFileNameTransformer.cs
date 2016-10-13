using PlaytikaTestTask.BI.Interfaces;
using System;

namespace PlaytikaTestTask.BI.Implementation
{
    public class SimpleFileNameTransformer : IFileNameTransformer
    {
        public string Transform(string fileName)
        {
            if (fileName == null)
                throw new ArgumentNullException();
            return fileName;
        }
    }
}
