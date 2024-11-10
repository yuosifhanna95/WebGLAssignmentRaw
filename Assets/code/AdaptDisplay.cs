using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdaptDisplay : MonoBehaviour
{
    //class for screen adaption mobile/desktop
    public Canvas canvas;
    // Start is called before the first frame update
    void Start()
    {
        float ratio = (float) Screen.width / Screen.height;
        canvas.scaleFactor *= (float)Screen.height / 2400;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
