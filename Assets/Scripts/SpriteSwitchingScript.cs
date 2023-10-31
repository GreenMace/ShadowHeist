using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSwitchingScript : MonoBehaviour
{

    public bool autoInitialize = false;
    public Sprite[] sprites;
    public Vector3[] translations;
    public float[] rotations;

    public SpriteRenderer spriteRenderer;
    public Rigidbody2D rb;

    int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        
        if (autoInitialize) {
            
            for (int i = 0; i < sprites.Length; ++i) {
                SetTransform(i, trans: rb.position, rot: rb.rotation);
            }
        }
        
        SpriteByIndex(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextSprite() {
        index = ++index % sprites.Length;
        SpriteByIndex(index);
    }

    public void SpriteByIndex(int idx) {
        if (idx >= 0 && idx < sprites.Length) {
            index = idx;
            spriteRenderer.sprite = sprites[index];
            rb.position = translations[index];
            rb.SetRotation(rotations[index]);
        }
    }

    public void SetTransform(int index, Vector3? trans = null, float? rot = null) {
        if (trans != null) {
            translations[index] = (Vector3)trans;
        }

        if (rot != null) {
            rotations[index] = (float)rot;
        }
    }
}
