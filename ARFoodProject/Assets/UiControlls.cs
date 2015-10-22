using UnityEngine;
using System.Collections;
using Vuforia;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using System.Timers;

public class UiControlls : MonoBehaviour {

    public GameObject character;
    public GameObject animatorObject;
    private Animator _animator;
    public GameObject mesh;
    private SkinnedMeshRenderer r;
    private Material _mat;

    private Dropdown _dropdown;
    private GameObject _uiElements;

    public float morphingIntervalMS;
    public float morphingDurationMS;
    private float _timeRemaining;

    
  
    // Use this for initialization
    void Start () {
        r = mesh.GetComponent<SkinnedMeshRenderer>();
        _mat = r.material;

        _animator = animatorObject.GetComponent<Animator>();

        _uiElements = GameObject.Find("UiElements");
        

        Debug.LogWarning("Loaded level: " + Application.loadedLevelName);
        _dropdown = GameObject.Find("LevelsDropdown").GetComponent<Dropdown>();
        if(Application.loadedLevelName == "ExtendedTrackingLevel")
        {
            _dropdown.value = 1;
        }
        else
        {
            _dropdown.value = 0;
        }

        _uiElements.SetActive(false);
    }

	// Update is called once per frame
	void Update () {
    	if(setBlend && timer1.Enabled)
        {
            setBlend = false;
            float blend = _timeRemaining / morphingDurationMS;
            if (textureOne)
            {
                blend = 1.0f - blend;
            }
            _mat.SetFloat("_Blend", blend);
        }
        
    }

    public void Click_ShowUi()
    {
        _uiElements.SetActive(!_uiElements.activeSelf);

    }

    public void Click_Lasers()
    {
        GameObject[] lasers = GameObject.FindGameObjectsWithTag("Lasers");
        foreach(var laser in lasers)
        {
            ParticleSystem ps = laser.GetComponent<ParticleSystem>();
            if (ps.isPlaying)
                ps.Stop();
            else
                ps.Play();
          //  laser.SetActive(!laser.activeSelf);
        }
    }

    static float scaleInc = 0.3f;
    public void ScaleUp_Click()
    {
       character.transform.localScale += new Vector3(scaleInc, scaleInc, scaleInc);
    }
    public void ScaleDown_Click()
    {
        character.transform.localScale -= new Vector3(scaleInc, scaleInc, scaleInc);
    }

    private bool downTarget = true;
    static float targetRotate = 90;
    public void RotateFront_Click()
    {
        if (!downTarget)
            return;
        downTarget = false;
        character.transform.localEulerAngles = new Vector3(-targetRotate, 0, 0);
        character.transform.position = character.transform.position + new Vector3(-40.0f, 60.0f,0.0f);
    }

    public void RotateDown_Click()
    {
        if (downTarget)
            return;
        downTarget = true;
        character.transform.localEulerAngles = new Vector3(0, 0, 0);
        character.transform.position = character.transform.position - new Vector3(-40.0f, 60.0f, 0.0f);
    }

    public void ResetLevel_Click()
    {
        Application.LoadLevel(Application.loadedLevelName);
    }

    public void ChangeLevel_Click()
    {
        int opt = _dropdown.value;
        if (opt == 0)
        {

            Application.LoadLevel("NormalTrackingLevel");
        }
        else
        {
            Application.LoadLevel("ExtendedTrackingLevel");
        }
    }

    private Timer timer1;
    private bool setBlend;
    private bool textureOne = true;
    public void Morph_Click()
    {


        _timeRemaining = morphingDurationMS;
        timer1 = new Timer(morphingIntervalMS);
   
        timer1.Elapsed += TimerTick;
        timer1.Disposed += Timer1_Disposed;
        timer1.Start();

     

          //  changeCount = 1.0;
        //r.material.SetTexture("_Texture2", newTexture);
      //  r.material.SetFloat("_Blend", 1);

    }
    private void TimerTick(object o, EventArgs e)
    {
        //Debug.Log("Tick");

        // r.material.SetFloat("_Blend", 1.0f);
        _timeRemaining -= morphingIntervalMS;
        setBlend = true;
        if (_timeRemaining <= morphingIntervalMS * 2)
        {
            timer1.Stop();
            timer1.Dispose();
        }

    }
    private void Timer1_Disposed(object sender, EventArgs e)
    {
        if (textureOne)
            textureOne = false;
        else
            textureOne = true;
        Debug.Log("Tick");
    }

    public void Jump_Click()
    {
        _animator.ResetTrigger("Jump");
        _animator.SetTrigger("Jump");
    }

}
