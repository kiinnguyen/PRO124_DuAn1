using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemHealth : Item
{


    
        public int healAmount;

        public override void Use(Player player)
        {
            player.Health(healAmount);
        }
    
}
