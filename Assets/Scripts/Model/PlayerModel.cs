using System;
using Helper;
using Interface;
using UnityEngine;

namespace Model
{
    public sealed class PlayerModel : BaseObjectScene, ICollision
    {
        #region Fields

        [SerializeField] private float _hp = 100;
        public event Action<PlayerModel> OnDieChange;

        #endregion


        #region Properties

        public float Hp
        {
            get { return _hp; }
            set { _hp = value; } //todo добавить расчет коэффициента снижения урона по броне (все закешировать)
        } 
        public float PercentXp => Hp; //todo добавить расчет по формуле: выносливость * 17 с занесением в кеш

        #endregion

        public void OnCollision(InfoCollision info)
        {
            if (Hp > 0)
            {
                Hp -= info.Damage;
            }

            if (Hp <= 0)
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