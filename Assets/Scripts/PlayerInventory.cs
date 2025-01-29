using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour
{
    public int totalWeaponSlots = 3;
    public GameObject weaponManager;
    List<WeaponManager.WeaponConfigurable> weapons = new List<WeaponManager.WeaponConfigurable>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    void AddWeapon(WeaponManager.WeaponConfigurable weapon) 
    {
        weapons.Add(weapon);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
