using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyLifetime : MonoBehaviour
{
    public float lifeTime = 3f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }
    
}
