using System;
using Controller.TimeRemaining;
using Helper;
using Interface;
using Packages.Rider.Editor.UnitTesting;
using UnityEngine;
using UnityEngine.AI;

namespace Model
{
    public sealed class Mine : BaseObjectScene
    {
        #region Properties

        public event Action OnPointChange = delegate { };

        #endregion


        #region Fields

        [SerializeField] private float _radius = 5.0f;
        [SerializeField] private float _damagePoints = 20.0f;
        [SerializeField] private float _boomPower = 10;
        [SerializeField] private LayerMask _mask;
        private Collider[] _arrayCollider = new Collider[5];
        [SerializeField] private bool _enableDetection = true;

        #endregion

        // private void OnEnable()
        // {
        //     Debug.Log($"Mine.OnEnable.mineCollider.OnApplyDamageChange += Boom");
        //     var mineCollider = GetComponentInChildren<MineCollider>();
        //     if (mineCollider != null) mineCollider.OnApplyDamageChange += Boom;
        // }
        //
        // private void OnDisable()
        // {
        //     Debug.Log($"Mine.OnEnable.mineCollider.OnApplyDamageChange -= Boom");
        //     var mineCollider = GetComponentInChildren<MineCollider>();
        //     if (mineCollider != null) mineCollider.OnApplyDamageChange -= Boom;
        // }

        private void Boom()
        {
            if (!_enableDetection) return;
            Debug.Log($"Mine.Boom");
            // gameObject.GetComponent<Renderer>().enabled = false;
            // gameObject.GetComponent<Collider>().enabled = false;
            DebugExtension.DebugCircle(Transform.position, _radius);
            _arrayCollider = Physics.OverlapSphere(Transform.position, _radius, _mask);
            foreach (var item in _arrayCollider)
            {
                if (item != null)
                {
                    Debug.Log($"item[{item.name}]");
                    var characterController = item.gameObject.GetComponent<CharacterController>();
                    if (characterController != null)
                        characterController.enabled = false;

                    var navMeshAgent = item.GetComponent<NavMeshAgent>();
                    if (navMeshAgent != null)
                        navMeshAgent.enabled = false;

                    var rigitBody = item.gameObject.GetComponent<Rigidbody>();
                    if (rigitBody != null)
                    {
                        _enableDetection = false;
                        rigitBody.isKinematic = false;


                        //смещаем точку импульса вниз на 1 юнит, для реалистичности взрыва
                        rigitBody.AddForce((rigitBody.position - new Vector3(Transform.position.x,
                                Transform.position.y - 1, Transform.position.z)
                            ).normalized * _boomPower, ForceMode.Impulse);

                        var tempObj = rigitBody.gameObject.GetComponent<ICollision>();

                        if (tempObj != null)
                        {
                            tempObj.OnCollision(new InfoCollision(_damagePoints, new ContactPoint(),
                                new RectTransform(), Rigidbody.velocity));
                        }

                        Destroy(gameObject, 0.1f);
                    }

                    if (characterController != null)
                    {
                        void Enable()
                        {
                            ITimeRemaining test = new TimeRemaining(delegate
                            {
                                
                                item.GetComponent<CharacterController>().enabled = true;
                            }, 2.0f);
                            test.AddTimeRemainingExecute();
                        }
                        Enable();
                    }
                    if (navMeshAgent != null)
                    {
                        void Enable()
                        {
                            ITimeRemaining test = new TimeRemaining(delegate { item.GetComponent<NavMeshAgent>().enabled = true; }, 2.0f);
                            test.AddTimeRemainingExecute();
                        }
                        Enable();
                    }
                    if (rigitBody != null)
                    {
                        void Enable()
                        {
                            ITimeRemaining test = new TimeRemaining(delegate { item.GetComponent<Rigidbody>().isKinematic = true; }, 2.0f);
                            test.AddTimeRemainingExecute();
                        }
                        Enable();
                    }
                    
                }
            }
        }

        #region ICollision

        // private void OnCollisionEnter(Collision other)
        // {
        //     var tempObj = other.gameObject.GetComponent<ICollision>();
        //     Debug.Log($"{tempObj}, {other.gameObject.name}");
        //     if (tempObj != null)
        //     {
        //         tempObj.OnCollision(new InfoCollision(_damagePoints, new ContactPoint(), transform,
        //             other.transform.position - transform.position));
        //
        //         Destroy(gameObject, 0.5f);
        //     }
        // }
        //
        // private void OnCollisionStay(Collision other)
        // {
        //     var tempObj = other.gameObject.GetComponent<ICollision>();
        //     Debug.Log($"{tempObj}, {other.gameObject.name}");
        //     if (tempObj != null)
        //     {
        //         tempObj.OnCollision(new InfoCollision(_damagePoints, new ContactPoint(), transform,
        //             other.transform.position - transform.position));
        //
        //         Destroy(gameObject, 0.5f);
        //     }
        // }

        private void OnTriggerEnter(Collider other)
        {
            Boom();
            // Debug.Log($"123123123");
            // var tempObj = other.gameObject.GetComponent<ICollision>();
            //
            // if (tempObj != null)
            // {
            //     tempObj.OnCollision(new InfoCollision(_damagePoints, new ContactPoint(), transform,
            //         other.transform.position - transform.position));
            //
            //     Destroy(gameObject, 0.5f);
            // }
        }

        #endregion
    }
}