
public class Global  {

    public static Int3 stageSize = new Int3(5, 10, 5);
}

public class Int3
{
    public int x, y, z;

    public Int3(int _x, int _y, int _z)
    {
        x = _x;
        y = _y;
        z = _z;
    }

    public Int3(UnityEngine.Vector3 vector3)
    {
        x = UnityEngine.Mathf.RoundToInt(vector3.x);
        y = UnityEngine.Mathf.RoundToInt(vector3.y);
        z = UnityEngine.Mathf.RoundToInt(vector3.z);
    }
}
