using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    void Awake()
    {
        World world = new World();
        GameObject gameObject = new GameObject();
        gameObject.name = "world";
        world = gameObject.AddComponent<World>();
    }
}
