using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimator : MonoBehaviour {
    [SerializeField] private Sprite[] frameArray;
    private int currentFrame;
    private float timer;


    private void Update() {
        
        timer += Time.deltaTime;
        
        if(timer >= .25f) {
            timer -= .25f;
            currentFrame++;
            gameObject.GetComponent<SpriteRenderer>().sprite = frameArray[currentFrame];
        }
    } 
}