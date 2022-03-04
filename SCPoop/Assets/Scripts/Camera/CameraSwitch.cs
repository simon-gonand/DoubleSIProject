using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraSwitch : MonoBehaviour
{
    [Header("Requirements")]
    [SerializeField] private Animator camAnimator;
    [SerializeField] private CinemachineBrain cameraBrainP1;
    [SerializeField] private CinemachineBrain cameraBrainP2;

    [Header("Stats")]
    [SerializeField] private int camnbr;
    [SerializeField] private int startCamIndex = 1;

    private int currentCamIndex;

    public static CameraSwitch instance;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        Initialize();
    }

    private void Initialize()
    {
        currentCamIndex = startCamIndex;
        cameraBrainP2.enabled = false;

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

    [ContextMenu("SwitchPlayerCamera")]
    public void SwitchPlayerCamera()
    {
        cameraBrainP1.enabled = !cameraBrainP1.enabled;
        cameraBrainP2.enabled = !cameraBrainP2.enabled;

        if (cameraBrainP1.enabled)
        {
            cameraBrainP1.GetComponent<Camera>().targetDisplay = 0;
            cameraBrainP2.GetComponent<Camera>().targetDisplay = 1;
        }
        else
        {
            cameraBrainP1.GetComponent<Camera>().targetDisplay = 1;
            cameraBrainP2.GetComponent<Camera>().targetDisplay = 0;
        }

        if (currentCamIndex != 1)
        {
            currentCamIndex = 1;
            SwitchCam();
        }
    }
}
