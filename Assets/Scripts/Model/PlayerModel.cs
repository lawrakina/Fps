using System;
using Helper;
using Interface;
using UnityEngine;

namespace Model
{
    public sealed class PlayerModel : BaseObjectScene, ICollision
    {
        #region Fields

        [SerializeField] private float _xp = 100;
        public event Action<PlayerModel> OnDieChange;

        #endregion


        #region Properties

        public float Xp
        {
            get { return _xp; }
            set { _xp = value; } //todo добавить расчет коэффициента снижения урона по броне (все закешировать)
        } 
        public float PercentXp => Xp; //todo добавить расчет по формуле: выносливость * 17 с занесением в кеш

        #endregion

        public void OnCollision(InfoCollision info)
        {
            if (Xp > 0)
            {
                Xp -= info.Damage;
            }

            if (Xp <= 0)
            {
                foreach (var child in GetComponentsInChildren<Transform>())
                {
                    child.parent = null;

                    var tempRbChild = child.GetComponent<Rigidbody>();
                    if (!tempRbChild)
                    {
                        tempRbChild = child.gameObject.AddComponent<Rigidbody>();
                    }
                    Destroy(child.gameObject, 10);
                }

                OnDieChange?.Invoke(this);
            }
        }
    }
}