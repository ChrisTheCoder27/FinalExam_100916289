namespace Chapter.Command
{
    public class DamagePlayer : Command
    {
        ArwingController arwingController;

        public DamagePlayer(ArwingController controller)
        {
            arwingController = controller;
        }

        public override void Execute()
        {
            arwingController.DamagePlayer();
        }

        public override void Undo()
        {
            arwingController.ReverseDamage();
        }
    }
}