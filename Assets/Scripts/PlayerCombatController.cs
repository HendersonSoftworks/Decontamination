using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    public enum weapons
    {
        PowerWasher,
        Flamethrower,
    };

    public GameObject weaponObj;

    [SerializeField]
    private weapons currentWeapon;

    [SerializeField]
    private float[] weaponDamages;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
