using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    private PlayerCombat _playerCombat => GetComponent<PlayerCombat>();
    private GameObject _currentPickupToDrop;
    private GameObject _currentDroppedPickup;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Pickup>(out Pickup pickup))
        {
            if (_currentDroppedPickup == other.gameObject)
            {
                return; 
            }

            if (_currentPickupToDrop != null)
            {
                _currentPickupToDrop.transform.position = transform.position;
                _currentPickupToDrop.SetActive(true);
                _currentDroppedPickup = _currentPickupToDrop;
            }
            _playerCombat.SwitchWeapon(pickup.Weapon);
            _currentPickupToDrop = other.gameObject;
            _currentPickupToDrop.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_currentDroppedPickup == other.gameObject)
        {
            _currentDroppedPickup = null;
        }
    }
}
