using Interface;
using UnityEngine;


namespace Model
{
    public class BaseUnitModel : BaseObjectScene, ISelectObj
    {
        #region Fields

        [SerializeField] protected float _hp = 100;

        #endregion
        
        
        #region Properties

        public float Hp
        {
            get { return _hp; }
            set { _hp = value; } //todo добавить расчет коэффициента снижения урона по броне (все закешировать)
        } 
        public float PercentXp => Hp; //todo добавить расчет по формуле: выносливость * 17 с занесением в кеш

        #endregion
        
        
        #region Methods

        public void OnHealing(float delta)
        {
            if (Hp > 0)
            {
                Hp += delta;
            }
        }

        #endregion


        #region ISelectObject

        public string GetMessage()
        {
            return $"{name}, Hp:{Hp}";
        }

        #endregion
    }
}