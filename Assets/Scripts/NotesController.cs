using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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

    void spawnLeftBigRing()
    {
        var currentRing = Instantiate(leftBigRing, leftSpawnerBig.position, leftSpawnerBig.rotation);
        currentRing.transform.SetParent(parentObject.transform);
        currentRing.transform.localScale = new Vector3(1.15f, 0.85f, 0);
        currentRing.tag = "rings";
    }

    void spawnLeftSmallRing()
    {
        var currentRing = Instantiate(leftSmallRing, leftSpawnerSmall.position, leftSpawnerSmall.rotation);
        currentRing.transform.SetParent(parentObject.transform);
        currentRing.transform.localScale = new Vector3(1.0f, 0.85f, 0);
        currentRing.tag = "rings";
    }

    void spawnRightBigRing()
    {
        var currentRing = Instantiate(rightBigRing, rightSpawnerBig.position, rightSpawnerBig.rotation);
        currentRing.transform.SetParent(parentObject.transform);
        currentRing.transform.localScale = new Vector3(1.15f, 0.85f, 0);
        currentRing.tag = "rings";
    }

    void spawnRightSmallRing()
    {
        var currentRing = Instantiate(rightSmallRing, rightSpawnerSmall.position, rightSpawnerSmall.rotation);
        currentRing.transform.SetParent(parentObject.transform);
        currentRing.transform.localScale = new Vector3(1.0f, 0.85f, 0);
        currentRing.tag = "rings";
    }

    public void spawnNotes(HitObject hitObject)
    {
        if (hitObject.getX() == 64)
        {
            spawnLeftBigRing();
        }
        else if (hitObject.getX() == 192)
        {
            spawnLeftSmallRing();
        }
        else if (hitObject.getX() == 320)
        {
            spawnRightBigRing();
        }
        else if (hitObject.getX() == 448)
        {
            spawnRightSmallRing();
        }
    }


}
