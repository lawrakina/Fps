using System;
using Controller.TimeRemaining;
using Interface;
using UnityEngine;
using UnityEngine.AI;
using Object = System.Object;


namespace Model
{
    public class BaseUnitModel : BaseObjectScene, ISelectObj
    {
        #region Fields

        [SerializeField] protected float _hp = 100;
        [SerializeField] private float _timeToStun = 2.0f;
        [HideInInspector] protected ITimeRemaining _enabledRigitBody;
        [HideInInspector] protected ITimeRemaining _enabledNavMeshAgent;
        [HideInInspector] protected ITimeRemaining _enabledCharacterController;

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

        private void Start()
        {
            _enabledRigitBody = new TimeRemaining(EnableKinematicRigitBody , _timeToStun);
            _enabledNavMeshAgent = new TimeRemaining(EnableKinematicRigitBody , _timeToStun);
            _enabledCharacterController = new TimeRemaining(EnableKinematicRigitBody , _timeToStun);
        }

        private void EnableKinematicRigitBody()
        {
            Rigidbody.isKinematic = true;
        }
        private void EnabledNavMeshAgent()
        {
            GetComponent<NavMeshAgent>().enabled = true;
        }
        private void EnabledCharacterController()
        {
            GetComponent<CharacterController>().enabled = true;
        }

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