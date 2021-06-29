using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Debugger : MonoBehaviour // For debugging
{
    public bool Debug = false;

    public static Debugger Instance;

    public Canvas DebuggerCanvas = null;
    public TextMeshProUGUI SlopeDebug = null;
    public TextMeshProUGUI ClimbDebug = null;
    // public TextMeshProUGUI SlopeAngleDebug = null;

    private Vector3 groundCheckPosition = Vector3.zero;
    private float groundCheckRadius = 0;

    private float slopeRaycastAmount;

    public bool Climbing = false;

    public bool ResetPlayerPrefs;

    void Awake()
    {
        #region Singleton
        if (Instance == null)
        {
            Instance = this;
        } else
        {
            Destroy(gameObject);
        }
        #endregion
    }

    void Start()
    {
        DebuggerCanvas.gameObject.SetActive(Debug);

        if(ResetPlayerPrefs)
        {
            PlayerPrefs.DeleteAll();
        }
    }

    public void SetDebug()
    {
        Debug = !Debug;
        DebuggerCanvas.gameObject.SetActive(Debug);
    }

    public void UpdateClimbText()
    {
        if(Debug)
        {
            ClimbDebug.text = "Is Climbing: " + Climbing.ToString();
        }
    }

    public void UpdateSlopeDebugText(string _text)
    {
        if(Debug)
        {
            SlopeDebug.text = _text;
        }     
    }

    /*
    public void UpdateSlopeAngleText(float _angleAmount)
    {
        if(Debug)
        {
            SlopeAngleDebug.text = _angleAmount.ToString() + "°";
        }
    }
    */

    public void ShowGroundChecker(Vector3 _position, float _radius)
    {
        if(Debug)
        {
            groundCheckPosition = _position;
            groundCheckRadius = _radius;
        }
    }

    public void SetSlopeRaycast(float _raycastAmount)
    {
        slopeRaycastAmount = _raycastAmount;
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawSphere(groundCheckPosition, groundCheckRadius);

        if(PlayerManager.Instance.PlayerMovement.OnSlope())
        {
            Vector3 direction = transform.TransformDirection(Vector3.down) * slopeRaycastAmount;
            Gizmos.DrawRay(groundCheckPosition, direction);
        }
    }
}
