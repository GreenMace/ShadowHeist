using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSwitchingScript : MonoBehaviour
{

    public Sprite[] sprites;
    public Vector3[] translations;
    public Quaternion[] rotations;

    new SpriteRenderer renderer;
    new Transform transform;

    int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        transform = GetComponent<Transform>();
        nextSprite();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void nextSprite() {
        renderer.sprite = sprites[index];
        transform.position = translations[index];
        transform.rotation = rotations[index];

        index = ++index % sprites.Length;
    }
}
