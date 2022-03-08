using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshSpawner : MonoBehaviour
{
    public Card card;
    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;

    public void SpawnMesh()
    {
        meshFilter.mesh = card.stats.scpMesh;
        meshRenderer.material = card.stats.scpMaterial;
        gameObject.SetActive(true);
    }

    public void UnspawnMesh()
    {
        gameObject.SetActive(false);
    }
}
