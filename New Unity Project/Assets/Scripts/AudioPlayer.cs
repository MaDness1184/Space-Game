using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] AudioClip shootingClip;
    [SerializeField] [Range(0f, 1f)] float shootingVolume = 0.2f;

    [Header("Damage")]
    [SerializeField] AudioClip damageClip;
    [SerializeField] [Range(0f, 1f)] float damageVolume = 0.2f;

    [Header("Death")]
    [SerializeField] AudioClip deathClip;
    [SerializeField] [Range(0f, 1f)] float deathVolume = 0.2f;

    // Private
    static AudioPlayer audioPlayerInstance;
    Camera mainCamera;

    // Singleton instance
    public AudioPlayer GetInstance() // USE CAREFULLY
    {
        return audioPlayerInstance;
    }

    void Awake()
    {
        ManageSingleton();
        mainCamera = Camera.main;
    }

    void ManageSingleton()
    {
        if (audioPlayerInstance != null)
        {
            gameObject.SetActive(false); // Disable before destoying so that objects wont try to access it befor it's destroyed
            Destroy(gameObject);
        }
        else
        {
            audioPlayerInstance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlayShootingClip() // Play shooting audio at camera's transform
    {
        if (shootingClip != null)
        {
            PlayClip(shootingClip, shootingVolume);
        }
    }

    public void PlayDamageClip() // Play damage audio
    {
        PlayClip(damageClip, damageVolume);
    }

    public void PlayDeathClip() // Play death audio
    {
        PlayClip(deathClip, deathVolume);
    }

    private void PlayClip(AudioClip clip, float volume) // Play clipped audio at camera's transform
    {
        Vector3 cameraPos = mainCamera.transform.position;

        if (clip != null)
        {
            AudioSource.PlayClipAtPoint(clip, cameraPos, volume);
        }
    }
}
