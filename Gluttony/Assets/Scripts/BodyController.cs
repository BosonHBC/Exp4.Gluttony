using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyController : MonoBehaviour
{



    private Rigidbody rb;
    [SerializeField]
    private float fMoveSpeed;

    [SerializeField]
    private GameObject neckPrefab;
    private Transform head;

    List<Transform> necks = new List<Transform>();
    private float fAnchorValue = 0.53f;
    private float fNeckHeight = 0.8637f;

    private bool bConsuming;
    [SerializeField]
    private float fConsumeTime = 1f;
    private float collpaseTime;

    ParticleSystem poop;
    [SerializeField]
    float fReduceSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        poop = transform.GetChild(0).GetComponent<ParticleSystem>();
        head = transform.parent.GetChild(2);
        necks.Add(transform.parent.GetChild(1));
        Debug.Log(necks.Count);
    }

    // Update is called once per frame
    void Update()
    {
        PerformSpeed();
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            NewNeck();
        }

        if (bConsuming)
        {
            collpaseTime += Time.deltaTime;
            IncreaseHeight();
            if(collpaseTime > fConsumeTime)
            {
                bConsuming = false;
                collpaseTime = 0;
            }
        }

        if (Input.GetKey(KeyCode.Alpha2))
        {
            IncreaseHeight();
        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            //IncreaseHeight(-1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            CleanLastNeck();
        }

        if (Vector3.Angle(transform.up, Vector3.up) > 75f)
        {
            poop.Emit(1);
            ReduceHeight();
        }
           
    }

    void PerformSpeed()
    {
        float hori = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");

        Vector3 dir = (transform.parent.right * hori + transform.parent.forward * vert).normalized;

        rb.AddForce(dir * fMoveSpeed, ForceMode.Force);
    }
    public void NewNeck()
    {
        GameObject go = Instantiate(neckPrefab);
        go.transform.parent = transform.parent;
        go.transform.localScale = new Vector3(1, 0.001f, 1);

        Transform lastNeck = necks[necks.Count - 1];
        go.transform.SetSiblingIndex(lastNeck.GetSiblingIndex() + 1);
        go.name = "Neck_" + necks.Count;
        head.localPosition += head.up * 0.01f;
        go.transform.position = lastNeck.position + lastNeck.up;
        go.transform.localRotation = lastNeck.localRotation;

        SpringJoint sj_Last = lastNeck.GetComponent<SpringJoint>();
        SpringJoint sj_New = go.GetComponent<SpringJoint>();

        sj_Last.connectedBody = go.GetComponent<Rigidbody>();
        sj_Last.autoConfigureConnectedAnchor = false;
        sj_Last.anchor = new Vector3(0, fAnchorValue, 0);
        sj_Last.connectedAnchor = new Vector3(0, -fAnchorValue, 0);

        sj_New.connectedBody = head.GetComponent<Rigidbody>();
        sj_New.autoConfigureConnectedAnchor = true;
        // lastNeck.GetComponent<SpringJoint>().anchor = new Vector3(0, 0.86f, 0);
        necks.Add(go.transform);

        Debug.Log(necks.Count);
    }

    void IncreaseHeight()
    {
        if (necks[necks.Count - 1].localScale.y <= fNeckHeight)
        {
            necks[necks.Count - 1].localScale += new Vector3(0, Time.deltaTime, 0);
            Debug.Log("Adding");
        }
        else
        {
            NewNeck();
            return;
        }

        //if (necks[necks.Count - 1].localScale.y > fNeckHeight)
        //    necks[necks.Count - 1].localScale = new Vector3(1, fNeckHeight, 1);
        //else if (necks[necks.Count - 1].localScale.y < 0.01f)
        //    necks[necks.Count - 1].localScale = new Vector3(1, 0.01f, 1);
    }
    void ReduceHeight()
    {
        if (necks[necks.Count - 1].localScale.y >= 0.01f)
        {
            necks[necks.Count - 1].localScale -= new Vector3(0, fReduceSpeed*Time.deltaTime, 0);
            Debug.Log("Reducing");
        }
        else
        {
            CleanLastNeck();
            return;
        }
    }

    public void CleanLastNeck()
    {

        GameObject _neck = necks[necks.Count - 1].gameObject;
        if (!_neck)
            Debug.LogError("No Last Neck founded");

        necks.RemoveAt(necks.Count - 1);

        SpringJoint sj_Last = necks[necks.Count - 1].GetComponent<SpringJoint>();
        sj_Last.connectedBody = head.GetComponent<Rigidbody>();
        sj_Last.autoConfigureConnectedAnchor = true;
        Destroy(_neck);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Food"))
        {
            bConsuming = true;
           
        }
    }
}
