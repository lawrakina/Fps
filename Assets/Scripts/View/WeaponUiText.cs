﻿using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public sealed class WeaponUiText : MonoBehaviour
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

        public void ShowData(int countAmmunition, int countClip)
        {
            _text.text = $"{countAmmunition}/{countClip}";
        }

        public void SetActive(bool value)
        {
            _text.gameObject.SetActive(value);
        }

        #endregion
    }
}