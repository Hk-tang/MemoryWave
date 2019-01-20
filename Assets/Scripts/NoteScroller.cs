using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteScroller : MonoBehaviour
{

    public float beatTempo;
    // Start is called before the first frame update
    void Start()
    {

        beatTempo = 90;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= new Vector3(0f, beatTempo * Time.deltaTime, 0f); 
    }
}
