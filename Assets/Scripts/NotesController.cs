using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesController : MonoBehaviour
{
    public Transform leftSpawnerBig;
    public Transform leftSpawnerSmall;
    public Transform rightSpawnerBig;
    public Transform rightSpawnerSmall;

    public GameObject leftBigRing;
    public GameObject leftSmallRing;
    public GameObject rightBigRing;
    public GameObject rightSmallRing;
    public GameObject parentObject;

    // Start is called before the first frame update
    void Start()
    {
        var currentRing = Instantiate(leftBigRing, leftSpawnerBig.position, leftSpawnerBig.rotation);
        currentRing.transform.parent = parentObject.transform;
        currentRing.transform.localScale = new Vector3(1.15f, 0.85f, 0);

        currentRing = Instantiate(leftSmallRing, leftSpawnerSmall.position, leftSpawnerSmall.rotation);
        currentRing.transform.parent = parentObject.transform;
        currentRing.transform.localScale = new Vector3(1.0f, 0.85f, 0);

        currentRing = Instantiate(rightBigRing, rightSpawnerBig.position, rightSpawnerBig.rotation);
        currentRing.transform.parent = parentObject.transform;
        currentRing.transform.localScale = new Vector3(1.15f, 0.85f, 0);

        currentRing = Instantiate(rightSmallRing, rightSpawnerSmall.position, rightSpawnerSmall.rotation);
        currentRing.transform.parent = parentObject.transform;
        currentRing.transform.localScale = new Vector3(1.0f, 0.85f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
