using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CornShake : MonoBehaviour {
  
    [Header("Shake Settings")]
    public float ShakeLength = 1f;
    public float ShakeIntensity = 10f;
    private bool Used = false;
    private bool VFXCooldown = false;
    [Header("Hit VFX Settings")]
    public GameObject HitVFX = null;
    public Vector3 VFXRotationOffset = Vector3.zero;
    public Vector3 VFXScaleOverride = Vector3.one;
    [Header("Sound Settings")]
    public AudioClip ShakeSound = null;
    [Range(0f,1f)]
    public float ShakeSoundVolume = 1f;
    public Vector2 PitchVarianceRange = Vector2.zero;

    private void OnTriggerEnter(Collider other)
    {
        VFXCooldown = false;
        transform.DOComplete();
        transform.DOShakeRotation(ShakeLength, ShakeIntensity);
        //Add item drop
        if(!Used && GetComponent<Drops>())
        {
            GetComponent<Drops>().DropItem();
            Used = true;
        }
        //Create HitVFX
        if(!VFXCooldown && HitVFX)
        {
            GameObject VFX = Instantiate(HitVFX, transform.position, Quaternion.identity);
            VFX.transform.localRotation = Quaternion.Euler(transform.rotation.eulerAngles + VFXRotationOffset);
            VFX.transform.GetChild(0).localScale = (VFXScaleOverride);
            if (!VFX.GetComponent<ParticleSystem>().isPlaying)
            {
                VFX.GetComponent<ParticleSystem>().Play();
            }
            VFXCooldown = true;
        }
        if(ShakeSound)
        {
            PlaySound();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        VFXCooldown = false;
        transform.DOComplete();
        transform.DOShakeRotation(ShakeLength, ShakeIntensity);
        //Add item drop
        if (!Used && GetComponent<Drops>())
        {
            GetComponent<Drops>().DropItem();
            Used = true;
        }
        //Create HitVFX
        if (!VFXCooldown && HitVFX)
        {
            GameObject VFX = Instantiate(HitVFX, transform.position, Quaternion.identity);
            VFX.transform.localRotation = Quaternion.Euler(transform.rotation.eulerAngles + VFXRotationOffset);
            VFX.transform.GetChild(0).localScale = (VFXScaleOverride);
            if (!VFX.GetComponent<ParticleSystem>().isPlaying)
            {
                VFX.GetComponent<ParticleSystem>().Play();
            }
        }
        if(ShakeSound)
        {
            PlaySound();
        }
    }

    private void PlaySound()
    {
        float Pitch = Random.Range(PitchVarianceRange.x, PitchVarianceRange.y);
        MusicManager.GetInstance().PlaySoundAtLocation(ShakeSound, transform.position, Pitch, ShakeSoundVolume);
    }

    private void OnTriggerExit(Collider other)
    {
        VFXCooldown = false;
    }
}
