using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserLinePath : MonoBehaviour
{
    public LineRenderer laserLine;
    public LaserScript Laser;
    private float offset = -0.04f;
    const float yLine = -10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 laserPos = Laser.transform.position;
        transform.position = laserPos;
        Vector3 lineReference = new Vector3(-1 * laserPos.x + offset, yLine, 0);
        laserLine.SetPosition(1, lineReference);
    }

    public void moveLaserLine()
    {
       

        
    }
}
