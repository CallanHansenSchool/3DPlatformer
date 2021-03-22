using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraZoom : MonoBehaviour
{
    public float minZoom = 30f;
    public float maxZoom = 45f;
    public float sensitivity = 10f;

    public float zoomInValue;

    [SerializeField] private CinemachineFreeLook mainCamera;

    public bool CanZoom = true;
    public static CameraZoom Instance;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        } else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        zoomInValue = mainCamera.m_Lens.FieldOfView;
    }

    void Update()
    {
        if (CanZoom)
        {
            zoomInValue -= Input.GetAxis("Mouse ScrollWheel") * sensitivity;
            zoomInValue = Mathf.Clamp(zoomInValue, minZoom, maxZoom);
            mainCamera.m_Lens.FieldOfView = zoomInValue;
        }
    }
}
