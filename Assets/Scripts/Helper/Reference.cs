using UnityEngine;
using Model.Ai;
using UnityEditor.Experimental.GraphView;


namespace Helper
{
    public sealed class Reference : MonoBehaviour
    {
        public Bot Bot;
        public LayerMask EnvironmentLayerMask;
        public LayerMask UnitLayerMask;
    }
}