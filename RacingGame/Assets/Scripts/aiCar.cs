using UnityEngine;
using System.Collections;

public enum Direction { Left, Right, Forward }

public class aiCar : RigidBodyCar {
    private Direction m_dir;

    void Start()
    {
        m_dir = Direction.Forward;
        m_goalDir = Vector3.forward;
        m_gasAmount = 1f;
    }

	// Update is called once per frame
	void Update () {
        switch (m_dir){
            case Direction.Left:
                if (transform.forward != m_goalDir){
                    m_steerAmount = -1;
                }
                else{
                    m_dir = Direction.Forward;
                }
                break;
            case Direction.Right:
                if (transform.forward != m_goalDir){
                    m_steerAmount = 1;
                }else{
                    m_dir = Direction.Forward;
                }
                break;
            default:
                if (m_goalDir == transform.forward){
                    m_dir = Direction.Left;
                }
                gas();
                break;
        }
        rightCheck();
	}
    void OnTriggerEnter(Collider other){
        PathTriggers pt = other.gameObject.GetComponent<PathTriggers>();
        if (pt != null){
            m_dir = pt.m_turnDir;
            m_goalDir = pt.m_goalDir;
        }
    }
}
