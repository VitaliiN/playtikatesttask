using PlaytikaTestTask.BI.Interfaces;
using PlaytikaTestTask.Constants;
using System;

namespace PlaytikaTestTask.BI.Implementation
{
    public class CppFileNameTransformer : IFileNameTransformer
    {
        public string Transform(string fileName)
        {
            if (fileName == null)
                throw new ArgumentNullException();
            return fileName + Constant.cppAddString;
        }
    }
}
