using UnityEngine;
using UnityEngine.UI;


namespace View
{
    public sealed class FlashLightUiText : MonoBehaviour
    {
        #region Fields

        private Text _text;

        #endregion


        #region UnityMethods

        private void Awake()
        {
            _text = GetComponent<Text>();
        }

        #endregion


        #region Methods

        public float Text
        {
            set => _text.text = $"{value:0.0}";
        }

        public void SetActive(bool value)
        {
            _text.gameObject.SetActive(value);
        }

        #endregion
    }
}