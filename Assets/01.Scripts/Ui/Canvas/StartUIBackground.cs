using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUIBackground : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] private Vector2 pos;
    [SerializeField] private float speed = 1;
    void Update()
    {
        pos = Camera.main.ScreenToViewportPoint(Input.mousePosition) * speed;

        Vector2 offset = pos;
        spriteRenderer.material.SetTextureOffset("_MainTex", offset);
    }
}
