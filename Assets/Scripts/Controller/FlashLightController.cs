using Enums;
using Interface;
using Model;
using UnityEngine;


namespace Controller
{
    public sealed class FlashLightController : BaseController, IExecute, IInitialization
    {
        #region Fields

        private FlashLightModel _flashLightModel;

        #endregion


        #region IInitialization

        public void Initialization()
        {
            UiInterface.LightUiText.SetActive(false);
            UiInterface.FlashLightUiBar.SetActive(false);
        }

        #endregion


        #region Methods

        public override void On(params BaseObjectScene[] flashLight)
        {
            if (IsActive) return;
            if (flashLight.Length > 0) _flashLightModel = flashLight[0] as FlashLightModel;
            if (_flashLightModel == null) return;
            if (_flashLightModel.BatteryChargeCurrent <= 0) return;
            base.On(_flashLightModel);
            _flashLightModel.Switch(FlashLightActiveType.On);
            UiInterface.LightUiText.SetActive(true);
            UiInterface.FlashLightUiBar.SetActive(true);
            UiInterface.FlashLightUiBar.SetColor(Color.green);
        }

        public override void Off()
        {
            if (!IsActive) return;
            base.Off();
            _flashLightModel.Switch(FlashLightActiveType.Off);
            ;
            UiInterface.FlashLightUiBar.SetActive(false);
            UiInterface.LightUiText.SetActive(false);
        }

        #endregion

        
        #region IExecute

        public void Execute()
        {
            if (!IsActive)
            {
                return;
            }

            if (_flashLightModel.EditBatteryCharge())
            {
                UiInterface.LightUiText.Text = _flashLightModel.BatteryChargeCurrent;
                UiInterface.FlashLightUiBar.Fill = _flashLightModel.Charge;
                _flashLightModel.Rotation();

                if (_flashLightModel.LowBattery())
                {
                    UiInterface.FlashLightUiBar.SetColor(Color.red);
                }
            }
            else
            {
                Off();
            }
        }

        #endregion
    }
}