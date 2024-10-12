using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BaseDocument : MonoBehaviour
{
    [SerializeField] protected UIDocument document;
    private void Reset()
    {
        document = GetComponent<UIDocument>();
    }
}
