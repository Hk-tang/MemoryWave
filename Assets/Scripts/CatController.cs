using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatController : MonoBehaviour
{
    public Image
    public Sprite BothHandDownSprite;
    public Sprite leftHandDownSprite;
    public Sprite rightHandDownSprite;
    public Sprite BothHandUpSprite;

    public KeyCode leftHandDownKeyCode;
    public KeyCode rightHandDownKeyCode;
    public bool leftHandDown;
    public bool rightHandDown;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(leftHandDownKeyCode))
        {
            leftHandDown = true;
        }

        if (Input.GetKeyUp(leftHandDownKeyCode))
        {
            leftHandDown = false;
        }

        if (Input.GetKeyDown(rightHandDownKeyCode))
        {
            rightHandDown = true;
        }

        if (Input.GetKeyUp(rightHandDownKeyCode))
        {
            rightHandDown = false;
        }
    }
}
