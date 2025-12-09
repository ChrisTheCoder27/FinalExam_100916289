using Chapter.Command;
using UnityEngine;

namespace Chapter.State
{
    public class ArwingWreckedState : MonoBehaviour, IArwingState
    {
        ArwingController arwingController;

        public void Handle(ArwingController controller)
        {
            if (!arwingController)
            {
                arwingController = controller;
            }
            arwingController.CurrentSpeed = 4f;
        }
    }
}