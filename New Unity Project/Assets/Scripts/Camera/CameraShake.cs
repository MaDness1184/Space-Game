using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] float shakeDuration = 0.5f; // duration of screen shake
    [SerializeField] float shakeMagnitude = 0.5f; // how far the camera is going to move per frame

    Vector3 initialPosition;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
    }

    public void PlayCameraShake() // Call private Coroutine with default magnitude and default shake
    {
        StartCoroutine(CameraShakeCo(shakeMagnitude, shakeDuration));
    }

    public void PlayCameraShake(float newMagnitude, float newDuration) // Overloaded Method to customize magnitude of shake for a single instance
    {
        StartCoroutine(CameraShakeCo(newMagnitude, newDuration));
    }

    IEnumerator CameraShakeCo(float magnitude, float duration) // Coroutine to shake the camera for a set amount of time
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            transform.position = initialPosition + (Vector3)Random.insideUnitCircle * magnitude; // position of Camera Object = Unitcircle * change in radius
                                                   // Using (VectorType) before a variable casts it as the VectorType
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame(); // Wait for the end of frame before looping again
        }
        transform.position = initialPosition; // ResetCamera to original position
    }

}
