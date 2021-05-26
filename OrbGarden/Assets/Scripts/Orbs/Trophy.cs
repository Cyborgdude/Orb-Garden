using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trophy : BasicOrb
{
    [SerializeField]
    protected GameObject confetPS;
    public override void Use()
    {
        if(useCooldown <= 0)
        {
            base.Use();
            useCooldown = maxUseCooldown;
            Celebrate();
        }

        else
        {
            FailUse();
        }
    }

    protected void Celebrate()
    {
        SpawnThenDestroyParticle(confetPS, transform);
    }
}
