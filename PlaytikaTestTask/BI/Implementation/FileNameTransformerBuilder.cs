using PlaytikaTestTask.BI.Interfaces;
using System;
using System.Collections.Generic;
using PlaytikaTestTask.Constants;

namespace PlaytikaTestTask.BI.Implementation
{
    public class FileNameTransformerBuilder : IFileNameTransformerBuilder
    {
        private readonly Dictionary<AvailableActions, Func<IFileNameTransformer>> transformerInitializer;

        public FileNameTransformerBuilder()
        {
            transformerInitializer = new Dictionary<AvailableActions, Func<IFileNameTransformer>>();
            transformerInitializer.Add(AvailableActions.all, () => { return new SimpleFileNameTransformer(); });
            transformerInitializer.Add(AvailableActions.cpp, () => { return new CppFileNameTransformer(); });
            transformerInitializer.Add(AvailableActions.reversed1, () => { return new Reversed1FileNameTransformer(); });
            transformerInitializer.Add(AvailableActions.reversed2, () => { return new Reversed2FileNameTransformer(); });
        }

        public IFileNameTransformer GetTransformer(AvailableActions selectedAction)
        {
            var func = transformerInitializer[selectedAction];
            return func();
        }
    }
}
