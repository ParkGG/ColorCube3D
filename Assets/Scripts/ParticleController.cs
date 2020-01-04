using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    ParticleSystem particle;

    private void Start()
    {
        particle = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {

        if (particle)
        {
            if (!particle.IsAlive(true))
            {
                Destroy(gameObject);
            }
        }
    }   
}
