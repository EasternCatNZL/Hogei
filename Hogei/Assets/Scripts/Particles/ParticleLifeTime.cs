using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleLifeTime : MonoBehaviour {

    [Header("Particle sound")]
    [Tooltip("Sound played when particle appears")]
    public AudioSource sound;
    public Vector2 PitchRange = Vector2.zero;

    private ParticleSystem partsSystem;
    private float particleDuration;

    // Use this for initialization
    void Start()
    {
        partsSystem = GetComponent<ParticleSystem>();
        if (!partsSystem)
            partsSystem = GetComponentInChildren<ParticleSystem>();
        particleDuration = partsSystem.main.duration/* + partsSystem.main.startLifetimeMultiplier*/;
        //startTime = Time.time;
        if (sound)
        {
            sound.pitch = Random.Range(PitchRange.x, PitchRange.y);
            sound.Play();
        }
        Destroy(gameObject, particleDuration);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
