using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSwitchingScript : MonoBehaviour
{

    public bool autoInitialize = false;
    public Sprite[] sprites;
    public Vector3[] translations;
    public Quaternion[] rotations;

    public SpriteRenderer renderer;
    public Transform transform;

    int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        transform = GetComponent<Transform>();
        
        if (autoInitialize) {
            
            for (int i = 0; i < sprites.Length; ++i) {
                SetTransform(i, trans: transform.position, rot: transform.rotation);
            }
        }
        
        NextSprite();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextSprite() {
        renderer.sprite = sprites[index];
        transform.position = translations[index];
        transform.rotation = rotations[index];

        index = ++index % sprites.Length;
    }

    public void SetTransform(int index, Vector3? trans = null, Quaternion? rot = null) {
        if (trans != null) {
            translations[index] = (Vector3)trans;
        }

        if (rot != null) {
            rotations[index] = (Quaternion)rot;
        }
    }
}
