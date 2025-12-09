using Chapter.Command;
using UnityEngine;

namespace Chapter.State
{
    public class ArwingDamagedState : MonoBehaviour, IArwingState
    {
        ArwingController arwingController;

        public void Handle(ArwingController controller)
        {
            if (!arwingController)
            {
                arwingController = controller;
            }
            arwingController.CurrentSpeed = 8f;
        }
    }
}