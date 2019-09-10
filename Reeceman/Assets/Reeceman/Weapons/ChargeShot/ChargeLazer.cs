using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeLazer : MonoBehaviour
{
    public float charge;
    public GameObject projectile;
    public AudioSource chargeAudioSource;
    public AudioSource launchAudioSource;

    public AudioClip chargeNoise;
    public AudioClip launchNoise;
    public float scaleMultiplier;
    public float chargeDoneAt;
    public float ejectForce = 500;


    public bool hasCreatedProjectile = false;
    public GameObject currProjectile;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Chack input
        if (Input.GetMouseButton(1))
        {
            charge += 0.01f;
        } else
        {
            if (charge > 0) {
                charge = 0;
                hasCreatedProjectile = false;
                fireCharge();
            }
        }

        // Check to create projectile
        if (!hasCreatedProjectile)
        {
            if (charge > 0)
            {
                createProjectile();
                hasCreatedProjectile = true;
            }
        }

        if (currProjectile != null) currProjectile.transform.localScale += new Vector3(scaleMultiplier, scaleMultiplier, scaleMultiplier);

        // Check to fire 
        if (charge >= chargeDoneAt)
        {
            fireCharge();
            charge = 0;
        }
    }

    private void fireCharge()
    {
        Debug.Log("fireCharge");
        chargeAudioSource.Stop();
        launchAudioSource.PlayOneShot(launchNoise);
        currProjectile.transform.parent = null;
        Rigidbody rb = currProjectile.GetComponent<Rigidbody>();
        if (rb) rb.isKinematic = false;
        if (rb) rb.AddForce(transform.forward * ejectForce);
        currProjectile = null;
    }

    private void createProjectile()
    {
        currProjectile = Instantiate(projectile, transform.position, transform.rotation, transform);
        currProjectile.transform.localScale = Vector3.zero;
        chargeAudioSource.clip = chargeNoise;
        chargeAudioSource.Play();
    }
}
