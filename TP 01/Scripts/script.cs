using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int[] myArray = new int[3];
        myArray[0] = 3;
        Debug.Log(myArray[0]);

        List<int> myList = new List<int>();
        myList.Add(1);
        myList[0] = 3;
        Debug.Log (myList[0]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
