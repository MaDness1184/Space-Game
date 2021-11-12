using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] bool isPlayer;
    [SerializeField] int scoreValue = 50;

    public int health = 50;
    public ParticleSystem hitEffectPS; // Stored reference to a explosion type PS Object

    public bool applyCameraShake; // a bool determining if the Object hit will make the camera shake
    public float gameOverDelay = 1f;

    CameraShake cameraShake;
    AudioPlayer audioPlayer;
    ScoreKeeper scoreKeeper;
    LevelManager LevelManager;

    void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>(); // Find Reference to AudioPlayer Object
        cameraShake = Camera.main.GetComponent<CameraShake>(); // Get Reference to CameraShake Component
        scoreKeeper = FindObjectOfType<ScoreKeeper>(); // Find Reference to ScoreKeeper Object
        LevelManager = FindObjectOfType<LevelManager>(); // Find Reference to LevelManager Object
    }

    void OnTriggerEnter2D(Collider2D otherCollision)
    {
        DamageDealer damageDealer = otherCollision.GetComponent<DamageDealer>();

        if (damageDealer != null)
        {
            TakeDamage(damageDealer.GetDamage());
            PlayHitEffect(); // instantiate hitEffectPS
            if (applyCameraShake)
                ShakeCamera(); // shake the camera
            damageDealer.Hit(); // Destroy the projectile/damage dealer
        }
    }

    public int GetHealth() // returns current health of Object
    {
        return health;
    }

   void TakeDamage(int damage) // Updates health of Object and destroys it when health reaches 0;
    {
        health -= damage;
        if (health <= 0)
        {
            Death();
        }
        else
            audioPlayer.PlayDamageClip();
    }

    void PlayHitEffect() // 
    {
        if (hitEffectPS != null) // if explosion PS is attached to hitEffectPS Object
        {
            ParticleSystem instance = Instantiate(hitEffectPS, transform.position, Quaternion.identity); // Instantiate an instance of hitEffectPS at this Object's transform
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax); // Destroy instance after a breaf period - Destroy(gameObject, timePeriod);
        }                              // instance.main.duration - duration of PS       instance.main.startLifetime.constantMax - the total lifetime in seconds that each new particle has
    }

    void ShakeCamera() // Shake camera when gameObject
    {
        if (cameraShake != null)
        {
            cameraShake.PlayCameraShake();
        }
    }

    void Death() // Death method for gameObject
    {
        Destroy(gameObject);
        if (isPlayer)
        {
            audioPlayer.PlayDeathClip();
            LevelManager.LoadGameOver();
        }
        else
        {
            scoreKeeper.UpdateCurrentScore(scoreValue);
            audioPlayer.PlayDamageClip();
        }
    }
}
