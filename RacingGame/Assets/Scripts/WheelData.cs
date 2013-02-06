using UnityEngine;
[System.Serializable]
public class WheelData
{
    public float m_asymptoteSlip;
    public float m_asymptoteValue;
    public float m_extremumSlip;
    public float m_extremumValue;
    public float m_stiffness;

    public WheelFrictionCurve setupWheelFriction()
    {
        WheelFrictionCurve wfc = new WheelFrictionCurve();
        wfc.asymptoteSlip = m_asymptoteSlip;
        wfc.asymptoteValue = m_asymptoteValue;
        wfc.extremumSlip = m_extremumSlip;
        wfc.extremumValue = m_extremumValue;
        wfc.stiffness = m_stiffness;
        return wfc;
    }
}
