using NUnit.Framework;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int totalWeaponSlots = 3;
    public GameObject weaponManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        List weapons = new List();
    }

    void AddWeapon(WeaponManager.WeaponConfigurable weapon) {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
