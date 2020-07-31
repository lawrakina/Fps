using Controller.TimeRemaining;

namespace Model
{
    public sealed class Gun : Weapon
    {
        #region Fields

        

        #endregion


        #region Methods

        public override void Fire()
        {
            if (!_isReady) return;
            if (Clip.CountAmmunition <= 0) return;
            var temAmmunition = Instantiate(Ammunition, _barrel.position, _barrel.rotation); //todo Pool object
            temAmmunition.AddForce(_barrel.forward * _force);
            Clip.CountAmmunition--;
            _isReady = false;
            _timeRemaining.AddTimeRemaining();
        }

        #endregion
    }
}