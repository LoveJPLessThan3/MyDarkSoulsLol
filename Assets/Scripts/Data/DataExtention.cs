using UnityEngine;

public static class DataExtention
{
    // public static Vector3Data AsVectorData(this Vector3 vector) => new Vector3Data(vector.x, vector.y, vector.z);\
    public static Vector3Data AsVectorData(this Vector3 vector)
    {
        return new Vector3Data(vector.x, vector.y, vector.z);
    }

    public static Vector3 AsUnityVector(this Vector3Data vector3Data)
    {
        return new Vector3(vector3Data.X, vector3Data.Y, vector3Data.Z);
    }
    //Для того, чтобы перонаж появился чуть выше начальной позиции и не провалился в пол
    public static Vector3 AddY(this Vector3 vector, float y)
    {
        vector.y += y;
        return vector;
    }

    //Вывод T, принимаение <T> ()
    public static T ToDeserialized<T>(this string json) => 
        JsonUtility.FromJson<T>(json);
    public static string ToJson(this object obj) =>
        JsonUtility.ToJson(obj);
}
