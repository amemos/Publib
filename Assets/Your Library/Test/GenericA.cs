using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericA<T> : MonoBehaviour where T : new()
{
    public static T StateMachine;
    private void Awake()
    {
        StateMachine = Initialize<T>();
    }
    public static T Initialize<T>() where T : new()
    {
        return new T();
    }
}

public class BBB
{
    public float xxx = 3.5f;
    public string zzz = "abc";

    public BBB()
    {
        xxx = 7.8f;
        zzz = "def";
    }
}