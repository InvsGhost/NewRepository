using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {
    public GameObject Obj1;
    public GameObject Obj2;
    public GameObject Obj3;
    public Vector3 BaseRotation;
    public float height;
    static float len1 = 1;
    static float len2 = 1;
    static float board = 10;
    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        //Vector3 pos1 = Obj1.transform.position;	
        //Vector3 pos2 = Obj2.transform.position;
        //Vector3 Rpos1 = Coordinate.TransByRotation(pos1, BaseRotation, 1);
        //Vector3 Rpos2 = Coordinate.TransByRotation(pos2, BaseRotation, 1);
        //print(Rpos1);
        //print(Rpos2);
        //Vector3 Dst = new Vector3((Rpos1.x * len2 + Rpos2.x * len1) / (len1 + len2), (Rpos1.y * len2 + Rpos2.y * len1) / (len1 + len2), Mathf.Min(Rpos1.z, Rpos2.z)-height);
        //print(Dst);
        //this.transform.position = Coordinate.TransByRotation(Dst, BaseRotation, -1);
        //this.transform.eulerAngles = BaseRotation;
        //float size = Mathf.Max(Mathf.Max(Mathf.Abs((Rpos1 - Dst).x), Mathf.Abs((Rpos2 - Dst).x)), Mathf.Max(Mathf.Abs((Rpos1 - Dst).y), Mathf.Abs((Rpos2 - Dst).y))) * 1.2f;
        //this.GetComponent<Camera>().orthographicSize = size;
        Vector3 pos1 = Coordinate.GetXY(Obj1.transform.position);
        Vector3 pos2 = Coordinate.GetXY(Obj2.transform.position);
        Vector3 V21 = new Vector3(pos2.x - pos1.x, 0, pos2.z - pos1.z);
        print(V21);
        float Angle0;
        if (V21.x >= 0) { Angle0 = Mathf.Asin(V21.z / Mathf.Pow(V21.x * V21.x + V21.z * V21.z, 0.5f)) * 180 / Mathf.PI; }
        else { Angle0 = 180 - Mathf.Asin(V21.z / Mathf.Pow(V21.x * V21.x + V21.z * V21.z, 0.5f)) * 180 / Mathf.PI; }
        print(Angle0);
        Vector3 NewRotation = new Vector3(BaseRotation.x, -(-BaseRotation.y + Angle0) + 90, 0);
        print(NewRotation);

        Vector3 Rpos1 = Coordinate.TransByRotation(pos1, NewRotation, 1);
        Vector3 Rpos2 = Coordinate.TransByRotation(pos2, NewRotation, 1);
        print(Rpos1);
        print(Rpos2);
        Vector3 Dst = new Vector3((Rpos1.x * len2 + Rpos2.x * len1) / (len1 + len2), (Rpos1.y * len2 + Rpos2.y * len1) / (len1 + len2), Mathf.Min(Rpos1.z, Rpos2.z) - height);
        print(Dst);
        this.transform.position = Coordinate.TransByRotation(Dst, NewRotation, -1);
        print(NewRotation);

        this.transform.eulerAngles = NewRotation;
        print(NewRotation);

        float size = Mathf.Max(Mathf.Max(Mathf.Abs((Rpos1 - Dst).x), Mathf.Abs((Rpos2 - Dst).x)), Mathf.Max(Mathf.Abs((Rpos1 - Dst).y), Mathf.Abs((Rpos2 - Dst).y))) * 1.2f;
        this.GetComponent<Camera>().orthographicSize = size;
    }
}

public class Coordinate
{
    public static Vector3 GetXY(Vector3 Pos)
    {
        return new Vector3(Pos.x, 0, Pos.z);
    }
    public static Vector3 TransByRotation(Vector3 Pos, Vector3 Rotation, float M)
    {
        float Mode = M / Mathf.Abs(M);
        float Rx = Rotation.x * Mode ;
        float Ry = Rotation.y * Mode;
        float Rz = Rotation.z * Mode ;
        Vector3 NewPos = Pos;
        //NewPos = new Vector3(NewPos.x, NewPos.y * Mathf.Cos(Rx) - NewPos.z * Mathf.Sin(Rx), NewPos.y * Mathf.Sin(Rx) + NewPos.z * Mathf.Cos(Rx));
        //NewPos = new Vector3(NewPos.x * Mathf.Cos(Ry) + NewPos.z * Mathf.Sin(Ry), NewPos.y, -NewPos.x * Mathf.Sin(Ry) + NewPos.z * Mathf.Cos(Ry));
        //NewPos = new Vector3(NewPos.x * Mathf.Cos(Rz) - NewPos.y * Mathf.Sin(Rz), NewPos.x * Mathf.Sin(Rz) + NewPos.y * Mathf.Cos(Rz), NewPos.z);
        Quaternion TmpQ = new Quaternion();
        TmpQ.eulerAngles = new Vector3(Rx, Ry, Rz);
        NewPos = TmpQ * NewPos;
        return NewPos;
    }
}