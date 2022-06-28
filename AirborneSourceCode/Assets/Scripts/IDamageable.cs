using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Damagable characters inherit from this class. Contains def for Taking Damage.
public interface IDamageable 
{
    void TakeDamage(float damage);
}


