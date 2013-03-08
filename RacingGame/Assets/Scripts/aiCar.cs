using UnityEngine;
using System.Collections;

public enum Direction { Left, Right, Forward }

public class aiCar : RigidBodyCar {
    public Direction m_dir;

    void Start()
    {
        m_dir = Direction.Forward;
        m_goalDir = Vector3.forward;
        m_gasAmount = 1f;
    }

	void Update () {
		if(Mathf.Abs(transform.rotation.eulerAngles.y-m_goalDir.y)>5f){
			if(transform.rotation.eulerAngles.y<m_goalDir.y){
				m_dir=Direction.Right;
			}else{
				m_dir=Direction.Left;
			}
		}else{
			m_dir=Direction.Forward;
		}
        switch (m_dir){
            case Direction.Left:
                m_steerAmount = -1;
				steer();
                break;
            case Direction.Right:
                m_steerAmount = 1;
				steer();
			break;
            default:
            	m_steerAmount=0;
				gas();
                break;
        }
        rightCheck();
	}	
}