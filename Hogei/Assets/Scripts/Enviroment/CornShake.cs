using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CornShake : MonoBehaviour {
  
    public float ShakeLength = 1f;
    public float ShakeIntensity = 10f;
    private bool Used = false;
    private bool VFXCooldown = false;

    public GameObject HitVFX = null;
    public Vector3 VFXRotationOffset = Vector3.zero;
    public Vector3 VFXScaleMultiper = Vector3.one;

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
            if (!VFX.GetComponent<ParticleSystem>().isPlaying)
            {
                VFX.GetComponent<ParticleSystem>().Play();
            }
            VFXCooldown = true;
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
            VFX.transform.localScale.Scale(VFXScaleMultiper);
            if (!VFX.GetComponent<ParticleSystem>().isPlaying)
            {
                VFX.GetComponent<ParticleSystem>().Play();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        VFXCooldown = false;
    }
}
