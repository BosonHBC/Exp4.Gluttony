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
    [SerializeField]
    BodyController bCtrl;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        particle = transform.GetChild(0).GetChild(0).GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!GameManager.instance.bGameOver)
        {
            transform.LookAt(CameraController.instance.worldPoint);
            PerformSplit();
        }
     
    }

    void PerformSplit()
    {
        if (Input.GetMouseButton(0))
        {
            rb.AddForce(-transform.forward * fSplitForce, mode);
            particle.Emit(1);
            bCtrl.ReduceHeight();
            bCtrl.IncreaseVom();
        }

    }
}
