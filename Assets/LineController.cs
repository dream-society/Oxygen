using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    private LineRenderer lineRenderer;
    [SerializeField] private Texture[] textures;
    private int animationStep;
    [SerializeField] private float fps = 30f;
    private float fpdCounter;

    public Transform origin;
    public Transform dest;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.SetPosition(0, origin.position);
        lineRenderer.SetPosition(1, dest.position);

        fpdCounter += Time.deltaTime;
        if (fpdCounter >= 1f / fps)
        {
            animationStep++;
            if (animationStep == textures.Length)
            {
                animationStep = 0;
            }

            lineRenderer.material.SetTexture("_MainTex", textures[animationStep]);

            fpdCounter = 0f;
        }
    }
}
