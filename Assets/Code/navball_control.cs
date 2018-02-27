using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class navball_control : MonoBehaviour {

    private float throttlePosition;
    private int velocity;
    private int altitude;
    private GameObject throttleFill;
    private GameObject velocityReadout;
    private GameObject altitudeReadout;

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

    private void Start()
    {
        //Find UI components
        throttleFill = transform.Find("throttle_fill").gameObject;
        velocityReadout = transform.Find("navball_vel").gameObject;
        altitudeReadout = transform.Find("navball_alt").gameObject;
    }

    private void Update()
    {
        //Update all meters
        throttleFill.GetComponent<Image>().fillAmount = throttlePosition;
        velocityReadout.GetComponent<Text>().text = velocity.ToString();
        altitudeReadout.GetComponent<Text>().text = altitude.ToString();
    }
}
