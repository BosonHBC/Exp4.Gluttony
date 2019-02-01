using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadLookAt : MonoBehaviour
{
    [SerializeField]
    private float fSplitForce;
    [SerializeField]
    ForceMode mode;
    private Rigidbody rb;

    private ParticleSystem particle;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        particle = transform.GetChild(0).GetChild(0).GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(CameraController.instance.worldPoint);
        PerformSplit();
    }

    void PerformSplit()
    {
        if (Input.GetMouseButton(0))
        {
            rb.AddForce(-transform.forward * fSplitForce, mode);
            if (!particle.isPlaying)
            {
                particle.Play();
            }
                
        }
        else
        {
            if (particle.isPlaying)
                particle.Stop();
        }
    }
}
