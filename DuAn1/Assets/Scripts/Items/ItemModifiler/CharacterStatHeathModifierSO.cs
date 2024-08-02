using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterStatHeathModifierSO : CharacterStatModifierSO
{
    public override void AffectCharacter(GameObject character, float val)
    {
        PlayerManager heath = character.GetComponent<PlayerManager>(); 
        if (heath != null)
        {
            heath.Heal((int)val);
        }
    }
}
