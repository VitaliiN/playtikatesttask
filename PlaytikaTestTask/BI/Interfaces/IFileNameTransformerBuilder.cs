using PlaytikaTestTask.Constants;

namespace PlaytikaTestTask.BI.Interfaces
{
    public interface IFileNameTransformerBuilder
    {
        IFileNameTransformer GetTransformer(AvailableActions selectedAction);
    }
}
