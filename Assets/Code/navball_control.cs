using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class navball_control : MonoBehaviour {

    private Quaternion navballRotation;
    private float throttlePosition;
    private int velocity;
    private int altitude;
    public bool rcs = false;
    public bool sas = false;

    private GameObject navballCameras;
    private GameObject throttleFill;
    private GameObject velocityReadout;
    private GameObject altitudeReadout;
    private GameObject rcsIndicator;
    private GameObject sasIndicator;

    //Public method to set throttle position display
	public void setNewThrottlePosition(float pos)
    {
        throttlePosition = pos / 4;
    }

    //Public method to set velocity value
    public void setNewVelocity (float vel)
    {
        velocity = Convert.ToInt32(vel);
    }

    //Public method to set altitude value
    public void setNewAltitude(float alt)
    {
        altitude = Convert.ToInt32(alt);
    }

    //public method to set navball rotation
    public void setNewNavballRotation (Quaternion rot)
    {
        navballRotation = rot;
    }

    public void setNewRcsState (bool state)
    {
        rcs = state;
    }

    public void setNewSasState (bool state)
    {
        sas = state;
    }

    private void Start()
    {
        //Find UI components
        navballCameras = transform.Find("navball_cameras").gameObject;
        throttleFill = transform.Find("throttle_fill").gameObject;
        velocityReadout = transform.Find("navball_vel").gameObject;
        altitudeReadout = transform.Find("navball_alt").gameObject;
        rcsIndicator = transform.Find("navball_rcs").gameObject;
        sasIndicator = transform.Find("navball_sas").gameObject;
    }

    private void Update()
    {
        //Update all meters
        navballCameras.transform.rotation = navballRotation;
        throttleFill.GetComponent<Image>().fillAmount = throttlePosition;
        velocityReadout.GetComponent<Text>().text = velocity.ToString();
        altitudeReadout.GetComponent<Text>().text = altitude.ToString();

        //RCS Indicator
        if (rcs)
        {
            rcsIndicator.GetComponent<Text>().text = "<color=#96FFACFF>RCS</color>";
        }
        else
        {
            rcsIndicator.GetComponent<Text>().text = "<color=#000000FF>RCS</color>";
        }

        //SAS Indicator
        if (sas)
        {
            sasIndicator.GetComponent<Text>().text = "<color=#96C2FFFF>SAS</color>";
        }
        else
        {
            sasIndicator.GetComponent<Text>().text = "<color=#000000FF>SAS</color>";
        }
    }
}
