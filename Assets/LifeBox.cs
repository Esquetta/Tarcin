using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeBox : MonoBehaviour
{
    public Sprite  []BoxFrames;
    int frame;
    SpriteRenderer spriteRenderer;
    float time = 0;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    
    void Update()
    {
        time += Time.deltaTime;
        if (time>0.1f)
        {
            spriteRenderer.sprite = BoxFrames[frame++];
            if (BoxFrames.Length == frame)
            {
                frame = BoxFrames.Length-1;
            }
            time = 0;
        }
        
    }
}
