using UnityEngine;
using System.Collections;

public class ControlledSize : MonoBehaviour {

    public float sizeMultiplie = 1.2f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SizeUp()
    {
        gameObject.transform.localScale *= sizeMultiplie;
    }

    public void SizeDown()
    {
        float coef = 1.0f - (sizeMultiplie - 1.0f);
        gameObject.transform.localScale *= coef;
    }

}
