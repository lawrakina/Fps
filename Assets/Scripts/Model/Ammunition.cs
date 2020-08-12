using Controller;
using Controller.TimeRemaining;
using Enums;
using UnityEngine;


namespace Model
{
    public abstract class Ammunition : BaseObjectScene
    {
        #region Fields

        [SerializeField] private float _timeToDestruct = 10;
        [SerializeField] private float _baseDamage = 10;
        protected float _curDamage;
        private float _lossOfDamageAtTime = 0.2f;
        private ITimeRemaining _timePutToPool;


        public AmmunitionType Type = AmmunitionType.Bullet;

        #endregion


        #region UnityMethods

        protected override void Awake()
        {
            base.Awake();
            _curDamage = _baseDamage;
        }

        private void Start()
        {
            _timePutToPool = new TimeRemaining(DestroyAmmunition, _timeToDestruct);
            _timePutToPool.AddTimeRemainingExecute();
            // DestroyAmmunition(_timeToDestruct);
            InvokeRepeating(nameof(LossOfDamage), 0, 1);
        }

        #endregion


        #region Methods

        public void AddForce(Vector3 dir)
        {
            if (!Rigidbody) return;
            Rigidbody.AddForce(dir);
        }

        private void LossOfDamage()
        {
            _curDamage -= _lossOfDamageAtTime;
        }

        protected void DestroyAmmunition()
        {
            // Destroy(gameObject, timeToDestruct);
            // CancelInvoke(nameof(LossOfDamage));

            // DisableRigidBody();
            //todo при отправке в пулл восстановить базовые значения _curDamage = _baseDamage
            _timePutToPool.RemoveTimeRemainingExecute();
            ServiceLocator.Resolve<PoolController>().PutToPool(this);
        }

        #endregion
    }
}