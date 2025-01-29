using System;
using System.ComponentModel;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public enum WeaponType {Epsilon, Zeta, Delta};
    public enum FiringMode {FullAuto, SemiAuto};

    public class WeaponConfigurable
    {
        public String displayName;
        public WeaponType weaponType;
        public int maxReserves;
        public int magSize;
        public float reloadTime;
        public GameObject projectile;
        public float fireRate;

        public WeaponConfigurable (String name, WeaponType type, GameObject proj, int maxAmmo, int maxMagAmmo, float reload, float firerate) 
        {
            displayName = name;
            weaponType = type;
            projectile = proj;
            maxReserves = maxAmmo;
            magSize = maxMagAmmo;
            reloadTime = reload;
            fireRate = firerate;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
