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
            DestroyAmmunition(_timeToDestruct);
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

        protected void DestroyAmmunition(float timeToDestruct = 0)
        {
            Destroy(gameObject, timeToDestruct);
            CancelInvoke(nameof(LossOfDamage));
            // Вернуть в пул
        }

        #endregion
    }
}