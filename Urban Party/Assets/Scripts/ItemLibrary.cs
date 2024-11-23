using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLibrary : MonoBehaviour
{
    public static WeaponData[] weaponLibrary;
    // Start is called before the first frame update
    void Start()
    {
        weaponLibrary = gameObject.GetComponentsInChildren<WeaponData>();
    }
}
