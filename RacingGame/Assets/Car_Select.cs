using UnityEngine;
using System.Collections;


public class Car_Select : MonoBehaviour{

    private int Selected_Car;
    private float Total_Width;
    private int Top_Speed;
    private int Acceleration;
    private int Break;
    private int Handling;
    public float Max_Top_Speed;
    public float Max_Acceleration;
    public float Max_Break;
    public float Max_Handling;
    public RigidBodyCar[] Rig_Car;
    public GameObject[] m_cars;
	public Texture2D choosecar2;
	public Texture2D Jon_Car;
	public Texture2D Masie_Car;
	public Texture2D Nick_Car;
	public Texture2D Oscar_Car;
	public Texture2D Webster_Car;
    public Texture2D Stat_Window;
    public Texture2D Stat_Bar;

    void Start(){

        Total_Width = Stat_Bar.width;
        Rig_Car = new RigidBodyCar[m_cars.Length];
        for (int i = 0; i < m_cars.Length; i++)
        {
            Rig_Car[i] = (RigidBodyCar)m_cars[i].GetComponent<RigidBodyCar>();
        }
}
    void OnGUI()
    {
		GUI.Box(new Rect(0,0,950,475),(choosecar2));
        GUI.Box(new Rect(435, 107, Stat_Window.width, Stat_Window.height), (Stat_Window));
        GUI.Box(new Rect(550, 123, Top_Speed, Stat_Bar.height), (Stat_Bar));
        GUI.Box(new Rect(550, 183, Acceleration, Stat_Bar.height), (Stat_Bar));
        GUI.Box(new Rect(550, 253, Break, Stat_Bar.height), (Stat_Bar));
        GUI.Box(new Rect(550, 313, Handling, Stat_Bar.height), (Stat_Bar));

        if (GUI.Button(new Rect(279, 107, 50, 75), Jon_Car))
        {
            Selected_Car = 2;
        }
        if (GUI.Button(new Rect(360, 107, 50, 75), Masie_Car))
        {
            Selected_Car = 3;
        }
        if (GUI.Button(new Rect(279, 200, 50, 75), Nick_Car))
        {
            Selected_Car = 0;
        }
        if (GUI.Button(new Rect(360, 200, 50, 75), Oscar_Car))
        {
            Selected_Car = 1;
        }
        if (GUI.Button(new Rect(279, 300, 50, 75), Webster_Car))
        {
            Selected_Car = 4;
        }
        if (GUI.Button(new Rect(360, 300, 50, 75), "Start"))
        {
            Application.LoadLevel("GamePlay");
        }
        PlayerPrefs.SetInt("selectedCar", Selected_Car);
        Cal();
	}
    void Cal()
    {
        Top_Speed = Mathf.RoundToInt((Rig_Car[Selected_Car].m_maxSpeed / Max_Top_Speed) * Total_Width);
        Acceleration = Mathf.RoundToInt((Rig_Car[Selected_Car].m_acceleration / Max_Acceleration) * Total_Width);
        Handling = ((int)Total_Width);
        Break = ((int)Total_Width);
    }
}
