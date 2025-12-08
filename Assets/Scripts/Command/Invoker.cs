using UnityEngine;

namespace Chapter.Command
{
    public class Invoker : MonoBehaviour
    {
        public void ExecuteCommand(Command command)
        {
            command.Execute();
        }

        public void UndoCommand(Command command)
        {
            command.Undo();
        }
    }
}