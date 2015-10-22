using UnityEngine;
using System.Collections;

public class StackContainer : MonoBehaviour {


    public GameObject[] childs;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void Next()
    {
        int i = 0;
        foreach (var obj in childs)
        {
            if (obj.activeSelf == true)
            {
                
                if (i == childs.Length - 1)
                {
                    childs[0].SetActive(true);
                }
                else
                {
                    childs[i + 1].SetActive(true);
                }
                obj.SetActive(false);
            }
            i++;
        }
    }

}
