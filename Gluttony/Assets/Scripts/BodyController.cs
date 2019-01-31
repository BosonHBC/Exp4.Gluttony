using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyController : MonoBehaviour
{

    private Rigidbody rb;
    [SerializeField]
    private float fMoveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        PerformSpeed();
    }

    void PerformSpeed()
    {
        float hori = Input.GetAxis("Horizontal");

        rb.AddForce(transform.right * hori * fMoveSpeed, ForceMode.Force);
    }
}
