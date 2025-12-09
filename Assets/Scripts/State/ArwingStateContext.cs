using Chapter.Command;

namespace Chapter.State
{
    public class ArwingStateContext
    {
        public IArwingState CurrentState
        {
            get; set;
        }

        readonly ArwingController arwingController;

        public ArwingStateContext(ArwingController controller)
        {
            arwingController = controller;
        }

        public void Transition(IArwingState state)
        {
            CurrentState = state;
            CurrentState.Handle(arwingController);
        }
    }
}