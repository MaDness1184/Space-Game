using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteScroller : MonoBehaviour
{
    public Vector2 scrollSpeed;

    Vector2 offset; // The amount of movement of the texture each frame
    Material material;

    void Awake()
    {
        material = GetComponent<SpriteRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        offset = scrollSpeed * Time.deltaTime; // Offset of sprite each frame
        material.mainTextureOffset += offset; // Add the offset to the material's offset
    }
}
