using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    [Header("Requirements")]
    [SerializeField] private Animator camAnimator;

    [Header("Stats")]
    [SerializeField] private int camnbr;
    [SerializeField] private int startCamIndex;

    private int currentCamIndex;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        currentCamIndex = startCamIndex;

        SwitchCam();
    }

    private void Update()
    {
        GetInput();
    }

    private void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            OnUpArrow();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            OnDownArrow();
        }
    }

    private void OnUpArrow()
    {
        currentCamIndex += 1;
        currentCamIndex = Mathf.Clamp(currentCamIndex, 0, camnbr - 1);

        SwitchCam();
    }

    private void OnDownArrow()
    {
        currentCamIndex -= 1;
        currentCamIndex = Mathf.Clamp(currentCamIndex, 0, camnbr - 1);

        SwitchCam();
    }

    private void SwitchCam()
    {
        switch (currentCamIndex)
        {
            case 0:
                camAnimator.Play("HandCamera");
                break;
            case 1:
                camAnimator.Play("GlobalCamera");
                break;
            case 2:
                camAnimator.Play("BoardCamera");
                break;
        }
    }

}
