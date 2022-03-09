using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshSpawner : MonoBehaviour
{
    public Card card;
    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;

    private float rotationSpeed = 5;

    public void SpawnMesh()
    {
        Debug.Log("saucisse");
        meshFilter.mesh = card.stats.scpMesh;
        meshRenderer.material = card.stats.scpMaterial;
        gameObject.SetActive(true);
    }

    public void UnspawnMesh()
    {
        gameObject.SetActive(false);
    }

    public void Update()
    {
        if(gameObject != null)
        {
            gameObject.transform.Rotate(new Vector3(0, 0, rotationSpeed) * Time.deltaTime);
        }

        if (CameraSwitch.instance.isCameraTopView)
        {
            meshRenderer.enabled = false;
        }
        if (CameraSwitch.instance.isCameraTopView == false)
        {
            meshRenderer.enabled = true;
        }
    }
}