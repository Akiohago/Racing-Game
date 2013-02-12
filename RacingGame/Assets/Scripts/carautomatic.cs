// ----------- Modified CAR TUTORIAL SAMPLE PROJECT, by The CoalGuru (based on a scripts by Andrew Gotow 2009,
// ----------- misc tutorials and open resources.
// my c# conversion
// Please put the script into the root car GameObject. 

using UnityEngine;
using System.Collections;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]

public class carautomatic : MonoBehaviour
{
    // All Wheel components down the hierarchy
    public WheelSet[] wheels;
    // is car AI Control?
    public bool aiControl = true;

    // Center of Mass correction.
    public Vector3 massCorrection;

    // Brake and handbrake torques.
    public float brakes = 100.0f;
    public float handbrakes = 500.0f;

    // How much velocity based down-pressure force to be applied.
    public float downpressFx = 0.5f;

    // Steering: wheels can turn up to lowSpeedSteerAngle when car is still;
    // and up to highSpeedSteerAngle when car goes at highSpeed km/h.
    // This is to decrease steering at high velocities so that playing with
    // plain keyboard is possible.
    public float minSteer = 45.0f;
    public float maxSteer = 10.0f;
    public float highSpeed = 100.0f;

    public float WheelRadius = 0.32f;

    // These variables are for the gears, the array is the list of ratios. The script
    // uses the defined gear ratios to determine how much torque to apply to the wheels.
    public float[] GearRatio;
    private int CurrentGear = 1;

    // These variables are just for applying torque to the wheels and shifting gears.
    // using the defined Max and Min Engine RPM, the script can determine what gear the
    // car needs to be in.
    public float EngineTorque = 400.0f;
    public float MaxRPM = 600.0f;
    public float MinRPM = 1000.0f;
    public float RPMLimit = 2000.0f;
    private float EngineRPM = 0.0f;
    private float WheelRPM = 0.0f;
    private float steer = 0.0f;

    public float springLength = 0.1f;
    public float springForce = 2200f;
    public float damperForce = 50f;

    public float FrontSlip = 0.98f;
    public float SideSlip = 0.1f;
    public float WheelMass = 5f;

    public GameObject SlipPrefab;

    private int WheelsN;
    private int MWheelsN = 0;
    private bool brakeAmount;
    private float gasAmount;
    private float steerAmount;

    // UI indicators
    public GUIText uiSpeed;
    public GUIText uiMotorRpm;
// --------------------------- Start ---------------------------------------------------------------------

    public void Start()
    {
        // wheels enumerator
        if (wheels != null)
        {
            WheelsN = wheels.Length;
            foreach (WheelSet w in wheels)
            {

                w.originalRotation = w.wheelgraphic.rotation;
                w.originalBrakeRotation = w.wheelaxle.rotation;

                //create collider 
                GameObject colliderObject = new GameObject("WheelC");
                colliderObject.transform.parent = transform;
                colliderObject.transform.position = w.wheelgraphic.position;
                w.wCollider = colliderObject.AddComponent<WheelCollider>();
                w.wCollider.suspensionDistance = springLength;
                JointSpring springSetup = new JointSpring();
                springSetup.spring = springForce;
                springSetup.damper = damperForce;
                w.wCollider.suspensionSpring = springSetup;
                WheelFrictionCurve forFrictionSetup = new WheelFrictionCurve();
                WheelFrictionCurve sideFrictionSetup = new WheelFrictionCurve();

                forFrictionSetup.stiffness = FrontSlip;
                sideFrictionSetup.stiffness = SideSlip;
                w.wCollider.forwardFriction = forFrictionSetup;
                w.wCollider.sidewaysFriction = sideFrictionSetup;
                w.wCollider.mass = WheelMass;
                w.wCollider.radius = WheelRadius;
                if (w.powered)
                {
                    MWheelsN++;
                }
                CurrentGear = 0;
            }
            // mass center adjustment 
            rigidbody.centerOfMass = massCorrection;



            Debug.Log(rigidbody.centerOfMass);
        }
        else
        {
            Debug.Log("No wheels assigned!");
        }
        if (MWheelsN == null)
        {
            Debug.Log("No motor wheels assigned!");
        }
    }

// ---------------------------- Update -------------------------------------------------------------------

public void Update () {
	
	// AI realisation
if (!aiControl) {	
	gasAmount = Input.GetAxis("Vertical");
	steerAmount = Input.GetAxis ("Horizontal");
	brakeAmount = Input.GetButton("Jump");
    }
    
	// This is to limit the maximum speed of the car, adjusting the drag probably isn't the best way of doing it,
	// but it's easy, and it doesn't interfere with the physics processing.
	rigidbody.drag = rigidbody.velocity.magnitude / 150;
   
	// The current speed in km/h
	float kmPerH = rigidbody.velocity.magnitude / 3.6f;
	float handTorque = 0.0f;
	if( brakeAmount)
		handTorque = handbrakes;
		
	// Compute the engine RPM based on the average RPM of all motorised wheels, 
	// then call the shift gear function and check if car grounded
	float SumRPM = 0.0f;
	bool isCarGrounded = false;
	
	foreach( WheelSet NextWheel in wheels )
	{
		if ( NextWheel.powered) {
		         SumRPM -= NextWheel.wCollider.rpm;
		
		if( NextWheel.wCollider.isGrounded )
				 isCarGrounded = true; 
	    }		 
	}
           
    if (MWheelsN!=0) {
    	
	     EngineRPM = WheelRPM * GearRatio[CurrentGear];
         WheelRPM  =  SumRPM  / MWheelsN ;
    }     
    else WheelRPM = 0;
    
	ShiftGears();

	// set the audio pitch to the percentage of RPM to the maximum RPM plus one, this makes the sound play
	// up to twice it's pitch, where it will suddenly drop when it switches gears.
	audio.pitch = Mathf.Abs(EngineRPM / MaxRPM) + 1.0f ;
	// this line is just to ensure that the pitch does not reach a value higher than is desired.
	if ( audio.pitch > 2.0f ) {
		audio.pitch = 2.0f;
	}

	// apply downpressure to the running grounded car
	if( isCarGrounded )
	{		
		var downPressure =new Vector3(0,0,0);
		downPressure.y = -Mathf.Pow(rigidbody.velocity.magnitude, 1.2f) * downpressFx;
		downPressure.y = Mathf.Max( downPressure.y, -70 );
		rigidbody.AddForce( downPressure, ForceMode.Acceleration );
	}
	
		// motor & brake ****************************************************************
		
	var drTorque = 0.0f;
	var brTorque = 0.0f;
	var GearTorque = EngineTorque / GearRatio[CurrentGear];
    
	if( Mathf.Abs(gasAmount) > 0.1f ) // if gas is pressed
	{
		if( WheelRPM * gasAmount < 0.0 )
		{
			// user is trying to drive in the opposite direction - treat that as brake
			brTorque = brakes;
		}
		if (EngineRPM < RPMLimit)
			drTorque = gasAmount * GearTorque;
	}
    
	// steering **************************************************************************
    steer = steerAmount * minSteer;	
	// find maximum steer angle (dependent on car velocity)
	var limSteer = Mathf.Lerp( minSteer, maxSteer, kmPerH / highSpeed );
    steer = limSteer * steerAmount;

    // rolling ***************************************************************************
	// Apply the values to the wheels.	The torque applied is divided by the current gear, and
	// multiplied by the user input variable.
	// the steer angle is an arbitrary value multiplied by the user input.
	
	for (int i=0;i<WheelsN;i++) {
	
	    UpdateWheel( i, handTorque, drTorque, brTorque,  steer );
	    	
	}
	
	
		// update UI texts if we have them ***********************************************
	if( uiSpeed != null )
//		uiSpeed.text = kmPerH.ToString("f1") + "\tkm/h";
        uiSpeed.text = rigidbody.velocity.magnitude.ToString("f1") + "\tkm/h";
	if( uiMotorRpm != null )
		uiMotorRpm.text = EngineRPM.ToString("f0") + "\trpm";
     
}


       //  update gears *******************************************************************


public void ShiftGears() {
	// this funciton shifts the gears of the vehcile, it loops through all the gears, checking which will make
	// the engine RPM fall within the desired range. The gear is then set to this "appropriate" value.
	if ( EngineRPM >= MaxRPM ) 
	{
		int AppropriateGear = CurrentGear;
		
		for ( int i = 0; i < GearRatio.Length; i ++ ) 
		{
			if ( WheelRPM * GearRatio[i] < MaxRPM ) 
			{
				AppropriateGear = i;
				break;
			}
		}
		
		CurrentGear = AppropriateGear;
	}
	
	if ( EngineRPM <= MinRPM ) 
	{
		int AppropriateGear = CurrentGear;
		
		for ( int j = GearRatio.Length-1; j >= 0; j -- ) 
		{
			if ( WheelRPM * GearRatio[j] > MinRPM ) 
			{
				AppropriateGear = j;
				break;
			}
		}
		
		CurrentGear = AppropriateGear;
	}
}

public void UpdateWheel(int num, float handbrake, float motion, float brake, float steer)
{
    // raycast wheel amortisation
    RaycastHit hit;
    Vector3 ColliderCenterPoint = wheels[num].wCollider.transform.TransformPoint(wheels[num].wCollider.center);

    if (Physics.Raycast(ColliderCenterPoint, -wheels[num].wCollider.transform.up, out hit, wheels[num].wCollider.suspensionDistance + wheels[num].wCollider.radius))
    {
        wheels[num].wheelgraphic.transform.position = hit.point + (wheels[num].wCollider.transform.up * wheels[num].wCollider.radius);
        wheels[num].wheelaxle.transform.position = hit.point + (wheels[num].wCollider.transform.up * wheels[num].wCollider.radius);
    }
    else
    {
        wheels[num].wheelgraphic.transform.position = ColliderCenterPoint - (wheels[num].wCollider.transform.up * wheels[num].wCollider.suspensionDistance);
        wheels[num].wheelaxle.transform.position = ColliderCenterPoint - (wheels[num].wCollider.transform.up * wheels[num].wCollider.suspensionDistance);
    }

    // now set the wheel rotation to the rotation of the collider combined with a new rotation value. This new value
    // is the rotation around the axle, and the rotation from steering input.

    if (wheels[num].powered)
    {
        wheels[num].wCollider.motorTorque = -motion;
    }

    // increase the rotation value by the rotation speed (in degrees per second)
    wheels[num].wheelrotation += wheels[num].wCollider.rpm * (360 / 60) * Time.deltaTime;

    if (wheels[num].steered)
    {
        // now set the wheel steer to the steer of the collider.
        wheels[num].wCollider.steerAngle = steer;
        wheels[num].wheelgraphic.transform.rotation = wheels[num].wCollider.transform.rotation * Quaternion.Euler(wheels[num].wheelrotation, wheels[num].wCollider.steerAngle, 0);
        float originBrackRot = (float)(wheels[num].originalBrakeRotation.x * 2 * 3.1416);
        wheels[num].wheelaxle.transform.rotation = wheels[num].wCollider.transform.rotation * Quaternion.Euler(originBrackRot, wheels[num].wCollider.steerAngle, 0);
    }
    else
        wheels[num].wheelgraphic.transform.rotation = wheels[num].wCollider.transform.rotation * Quaternion.Euler(wheels[num].wheelrotation, 0, 0);

    // apply brake or handbrake, depending on which is larger
    if (wheels[num].handbraked && handbrake > brake)
        brake = handbrake;

    wheels[num].wCollider.brakeTorque = brake;


    // define a wheelhit object, this stores all of the data from the wheel collider and will allow us to determine
    // the slip of the tire.
    WheelHit CorrespondingGroundHit;
    wheels[num].wCollider.GetGroundHit(out CorrespondingGroundHit);

    // if the slip of the tire is greater than 2.0, and the slip prefab exists, create an instance of it on the ground at
    // a zero rotation.
    if (Mathf.Abs(CorrespondingGroundHit.sidewaysSlip) > 2.0)
    {
        if (SlipPrefab)
        {
            Instantiate(SlipPrefab, CorrespondingGroundHit.point, Quaternion.identity);
        }
    }
}

}
