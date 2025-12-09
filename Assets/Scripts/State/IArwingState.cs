using Chapter.Command;

namespace Chapter.State
{
    public interface IArwingState
    {
        void Handle(ArwingController controller);
    }
}