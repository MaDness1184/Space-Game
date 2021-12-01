using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    const string PROJECTILE_PARENT_NAME = "Projectiles";
    GameObject projectileParent;

    public GameObject projectilePrefab;
    public float projectileSpeed = 10f;
    public float projectileLifetime = 5f;
    public float basefireingRate = 0.1f;
    public float firingRateVariance = 0f;
    public float minFiringRate = 0.1f;
    public bool useAI;
    public bool randomFireRate;

    [HideInInspector] public bool isFireing;

    // Private
    Rigidbody2D myRigidbody2D;
    Coroutine FireCo;
    AudioPlayer audioPlayer;

    void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>(); // Reference to the AudioPlayer Object
    }

    // Start is called before the first frame update
    void Start()
    {
        CreateProjectileParent();
        if (useAI)
        {
            isFireing = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Fire();
    }

    public void CreateProjectileParent()
    {
        projectileParent = GameObject.Find(PROJECTILE_PARENT_NAME);
        if (!projectileParent)
        {
            projectileParent = new GameObject(PROJECTILE_PARENT_NAME);
        }
    }

    void Fire()
    {
        if (isFireing && FireCo == null)
            FireCo = StartCoroutine(FireContinuouslyCo());
        else if (!isFireing && FireCo != null)
        {
            StopCoroutine(FireCo);
            FireCo = null;
        }
    }

    IEnumerator FireContinuouslyCo() // Fires projectiles continuously and deletes them after a lifetime
    {
        while(true) // indefinate loop (never ends)
        {
            GameObject instance = Instantiate(projectilePrefab, transform.position, transform.rotation); // Instantiate object at Player's position
            instance.transform.parent = projectileParent.transform;
            Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
            if (rb != null)
                rb.velocity = transform.up * projectileSpeed;
            Destroy(instance, projectileLifetime); // destroy instance after life time
            audioPlayer.PlayShootingClip(); // Play the shooting audio
            yield return new WaitForSeconds(FireRate()); // Wait for a firing delay
        }
    }

    float FireRate()
    {
        if (randomFireRate)
        {
            return Mathf.Clamp(Random.Range(basefireingRate - firingRateVariance, basefireingRate + firingRateVariance),
                minFiringRate, float.MaxValue);
        }
        return basefireingRate;
    }
}
