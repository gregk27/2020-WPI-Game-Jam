using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParticleScript : MonoBehaviour
{

    private ParticleSystem particleSystem;
    // Start is called before the first frame update
    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        particleSystem.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (!particleSystem.isPlaying) {
            Destroy(gameObject);
        }
    }
}
