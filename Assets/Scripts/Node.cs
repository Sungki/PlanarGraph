using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public bool visited;

    private ParticleSystem particle;
    private AudioSource audioSource;
    private Renderer render;

    private void Start()
    {
        // Particle, Audio and change of colors
        particle = GetComponent<ParticleSystem>();
        audioSource = GetComponent<AudioSource>();
        render = GetComponent<Renderer>();

        render.material.color = new Color((float)Random.Range(0, 1f), (float)Random.Range(0, 1f), (float)Random.Range(0, 1f));
    }

    private void OnMouseDown()
    {
        particle.Play();
        audioSource.Play();
    }
}
