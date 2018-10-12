using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : Effect {

    public bool isStart;
    public bool isEnd;
    
    public void doEffect()
    {

        if (isStart)
        {

        }
        else if (isEnd)
        {
            print("End of Level");
        }
        else {

        }

    }
}
