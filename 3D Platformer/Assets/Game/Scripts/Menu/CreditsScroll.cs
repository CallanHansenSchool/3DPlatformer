using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsScroll : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 200.0f;

    void Start()
    {
        Time.timeScale = 1;
    }

    void Update()
    {
        transform.Translate(Vector2.up * scrollSpeed * Time.deltaTime);
    }
}
