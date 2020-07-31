using Interface;


namespace Controller
{
    public sealed class PlayerController : BaseController, IExecute
    {
        #region Fields

        private readonly IMotor _motor;

        #endregion


        public PlayerController(IMotor motor)
        {
            _motor = motor;
        }


        #region IExecute

        public void Execute()
        {
            if (!IsActive) return;
            _motor.Move();
        }

        #endregion
    }
}