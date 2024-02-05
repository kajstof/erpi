namespace Erpi.BuildingBlocks.Application;

public class ApplicationLogicException : Exception
{
    public ApplicationLogicException()
    {
    }

    public ApplicationLogicException(string message)
        : base(message)
    {
    }

    public ApplicationLogicException(string message, Exception inner)
        : base(message, inner)
    {
    }
}
