using UnityEngine;
using System.Collections;


public class WheelSet
{
    public float wheelrotation = 0.0f;
    public WheelCollider wCollider;
    public Transform wheelgraphic;
    public Transform wheelaxle;
    public float lastSkidMark = -1f;
    public bool steered = false;
    public bool powered = false;
    public bool handbraked = false;
    public Quaternion originalRotation;
    public Quaternion originalBrakeRotation;
}
