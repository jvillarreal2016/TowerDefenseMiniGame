using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] int hitPoints = 10;
    [SerializeField] Collider collisionMesh;
    [SerializeField] ParticleSystem hitParticlePrefab;
    [SerializeField] ParticleSystem deathParticlePrefab;
    [SerializeField] AudioClip enemyHitSFX;
    [SerializeField] AudioClip enemyDeathSFX;

    AudioSource myAudioSource;
 
    // Start is called before the first frame update
    void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
    }

    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
        if(hitPoints <= 0)
        {
            print("I died");
            KillEnemy();
        }
    }

    void ProcessHit()
    {
        hitPoints = hitPoints - 1;
        hitParticlePrefab.Play();
        myAudioSource.PlayOneShot(enemyHitSFX);
    }

    private void KillEnemy()
    {
        //important to instantiate before destroying this object
        var vfx = Instantiate(deathParticlePrefab, transform.position, Quaternion.identity);
        vfx.Play();
        Destroy(vfx.gameObject, vfx.main.duration);
        AudioSource.PlayClipAtPoint(enemyDeathSFX, Camera.main.transform.position);
        // ^ this is because audio is being listened to in 3D, 
        // the other sound effects are doing this in 2D so we need to place the audio next to the listener

        Destroy(gameObject);
    }
}
