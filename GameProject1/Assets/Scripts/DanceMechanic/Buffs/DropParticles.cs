using System;
using System.Collections;
using System.Collections.Generic;
using System.Security;
using Bolt;
using UnityEngine;

public class DropParticles : MonoBehaviour
{
    [SerializeField] private ParticleSystem particles;
    public FloatingObject floating;

    private void Awake()
    {
        particles.transform.localScale = Vector3.one * (particles.transform.lossyScale.x);
    }

    public void DestroyHeart()
    {
        StartCoroutine(HeartsUp());
    }

    private IEnumerator HeartsUp()
    {
        float timer = 1.0f;
        particles.transform.parent = null;

        particles.transform.localScale = Vector3.one * (1.0f/particles.transform.lossyScale.x);
        
        Destroy(particles.gameObject,1.0f);
        
        Destroy(this.gameObject,1.0f);
        
        while (true)
        {
            yield return null;
            this.transform.position += Vector3.up * Time.deltaTime;
            timer -= Time.deltaTime;
        }
    }
}
