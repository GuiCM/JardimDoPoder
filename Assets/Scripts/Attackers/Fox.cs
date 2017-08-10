using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fox : Attacker
{
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(name + " bateu de frente com " + collider);
        if (collision.tag != "Defender")
            return;

        Defender defender = collision.GetComponent<Defender>();

        //A fox sabe pular a mureta do desespero
        if (defender.defenderType == Defender.DefenderType.Gravestone)
            anim.SetTrigger("IsJumping");
        else
        {
            currentTarget = collision.gameObject;
            anim.SetBool("IsAttacking", true);
        }
    }
}
