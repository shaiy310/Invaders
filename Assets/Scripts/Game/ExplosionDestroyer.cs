using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionDestroyer : MonoBehaviour
{
    public void DestroyExplosion()
    {
        Destroy(gameObject);
    }
}
