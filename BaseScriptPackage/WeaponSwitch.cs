using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    //This script allows you to swap between multiple weapons.
    //This script requires you to use the PlayerAttack_SideScroller script
    
    public List<Weapon> weaponList;
    private PlayerAttack_SideScroller player;


    void Start()
    {
        player = GetComponent<PlayerAttack_SideScroller>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            switchWeaponAtIndex(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            switchWeaponAtIndex(1);
        }
    }


    public void switchWeaponAtIndex(int index)
    {
        //Makes sure that if the index exists, then a switch will occur
        if (index < weaponList.Count && weaponList[index])
        {
            //Deactivate current weapon
            player.weapon.gameObject.SetActive(false);

            //Switch weapon to index then activate
            player.weapon = weaponList[index];
            player.weapon.gameObject.SetActive(true);
        }
        
    }
}
