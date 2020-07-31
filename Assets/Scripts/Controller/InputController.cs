using Enums;
using Interface;
using Model;
using UnityEngine;
using Cursor = UnityEngine.Cursor;


namespace Controller
{
    public sealed class InputController : BaseController, IExecute
    {
        #region Fields

        private KeyCode _activeFlashLight = KeyCode.F;
        private KeyCode _cancel = KeyCode.Escape;
        private KeyCode _reloadClip = KeyCode.R;
        private KeyCode _removeWeapon = KeyCode.T;
        private int _mouseButton = (int)MouseButton.LeftButton;

        #endregion
        

        public InputController()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        #region IInitialization

    

        #endregion


        #region IExecute
        
        public void Execute()
        {
            if (!IsActive) return;
            if (Input.GetKeyDown(_activeFlashLight))
            {
                ServiceLocator.Resolve<FlashLightController>().Switch();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SelectWeapon(0);
            }
            else if (Input.GetKeyDown(_cancel))
            {
                ServiceLocator.Resolve<WeaponController>().Off();
                ServiceLocator.Resolve<FlashLightController>().Off();
            } 
            else if (Input.GetKeyDown(_reloadClip))
            {
                ServiceLocator.Resolve<WeaponController>().ReloadClip();
            } 
            else if (Input.GetKeyDown(_removeWeapon))
            {
                ServiceLocator.Resolve<WeaponController>().Off();
                ServiceLocator.Resolve<Inventory>().RemoveWeapon();
            }
            
            if (Input.GetMouseButton(_mouseButton))
            {
                if (ServiceLocator.Resolve<WeaponController>().IsActive)
                {
                    ServiceLocator.Resolve<WeaponController>().Fire();
                }
            }
            
            if (Input.GetAxis("Mouse ScrollWheel") > 0) // todo manager
            {
                MouseScroll(MouseScrollWheel.Up);
            }

            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                MouseScroll(MouseScrollWheel.Down);
            }
        }

        #endregion


        #region Methods

        private void SelectWeapon(int value)
        {
            var tempWeapon = ServiceLocator.Resolve<Inventory>().SelectWeapon(value);
            SelectWeapon(tempWeapon);
        }

        private void SelectWeapon(Weapon weapon)
        {
            ServiceLocator.Resolve<WeaponController>().Off();
            if (weapon != null)
            {
                ServiceLocator.Resolve<WeaponController>().On(weapon);
            }
        }

        private void MouseScroll(MouseScrollWheel value)
        {
            var tempWeapon = ServiceLocator.Resolve<Inventory>().SelectWeapon(value);
            SelectWeapon(tempWeapon);
        }

        #endregion
        

        
		

        
    }
}