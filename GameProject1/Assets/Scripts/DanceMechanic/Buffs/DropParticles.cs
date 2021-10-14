using System;
using System.Collections;
using System.Collections.Generic;
using System.Security;
using Bolt;
using UnityEngine;

public class DropParticles : MonoBehaviour
{
    [SerializeField] private ParticleSystem particles;
    [SerializeField] private SpriteRenderer renderer;
    [SerializeField] private float fadeSpeed = 2;
    public FloatingObject floating;

    private void Awake()
    {
        floating = this.GetComponent<FloatingObject>();
        renderer = this.GetComponent<SpriteRenderer>();
        particles.transform.localScale = Vector3.one * (particles.transform.lossyScale.x);
    }

    public void LoseHearts()
    {
        StartCoroutine(HeartsRed());
    }
    
    public void ConsumeHearts()
    {
        StartCoroutine(HeartsUp());
    }

    private IEnumerator HeartsUp()
    {
        float timer = 1.0f;

        floating.enabled = false;
        
        particles.transform.parent = null;

        particles.transform.localScale = Vector3.one * (1.0f/particles.transform.lossyScale.x);

        while (timer > 0)
        {
            this.transform.position += Vector3.up * 30 * Time.deltaTime;
            timer -= Time.deltaTime;
            
            var adjustedColor = renderer.color;
            adjustedColor.a -= Time.deltaTime * fadeSpeed;
            renderer.color = adjustedColor;
            yield return null;
        }
        
        Destroy(particles.gameObject);

        Destroy(this.gameObject);
    }

    private IEnumerator HeartsRed()
    {
        float timer = 1.0f;

        floating.enabled = false;
        
        Destroy(particles.gameObject);
        
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            Color adjustedColor = Color.Lerp(renderer.color,Color.red, Time.deltaTime * fadeSpeed) ;
            renderer.color = adjustedColor;
            this.gameObject.transform.localScale *= 0.99f;
            yield return null;
        }

        Destroy(this.gameObject);
    }
}
