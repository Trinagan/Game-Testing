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

        public WeaponConfigurable (String name, WeaponType type, GameObject proj) 
        {
            displayName = name;
            weaponType = type;
            projectile = proj;
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
