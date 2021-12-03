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

    private bool invulnerable = false;

    CameraShake cameraShake;
    AudioPlayer audioPlayer;
    ScoreKeeper scoreKeeper;
    LevelManager levelManager;

    Animator myAnimator;

    void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>(); // Find Reference to AudioPlayer Object
        cameraShake = Camera.main.GetComponent<CameraShake>(); // Get Reference to CameraShake Component on the Main Camera
        scoreKeeper = FindObjectOfType<ScoreKeeper>(); // Find Reference to ScoreKeeper Object
        levelManager = FindObjectOfType<LevelManager>(); // Find Reference to LevelManager Object
        myAnimator = GetComponent<Animator>(); // Reference to Animator Component
    }

    void OnTriggerEnter2D(Collider2D otherCollision)
    {
        DamageDealer damageDealer = otherCollision.GetComponent<DamageDealer>();

        if (!invulnerable)
        {
            if (damageDealer != null)
            {
                TakeDamage(damageDealer.GetDamage());
                PlayHitEffect(); // instantiate hitEffectPS
                if (applyCameraShake)
                    ShakeCamera(); // shake the camera
                damageDealer.Hit(); // Destroy the projectile/damage dealer
            }
        }
    }

    public int GetHealth() // returns current health of Object
    {
        return health;
    }

    public bool GetInvulnerability()
    {
        return invulnerable;
    }

    public void SetInvulnerability(bool newBool)
    {
        invulnerable = newBool;
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
            myAnimator.SetTrigger("takeDamage");
        }
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
        ParticleSystem instance = Instantiate(hitEffectPS, transform.position, Quaternion.identity); // Instantiate an instance of hitEffectPS at this Object's transform
        Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax); // Destroy instance after a breaf period - Destroy(gameObject, timePeriod);
        if (isPlayer)
        {
            audioPlayer.PlayDeathClip();
            levelManager.LoadGameOver();
        }
        else
        {
            scoreKeeper.UpdateCurrentScore(scoreValue);
            audioPlayer.PlayDamageClip();
        }
    }
}
