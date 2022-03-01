using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDragMovements : MonoBehaviour
{
    [SerializeField]
    private Transform self;

    private MousePositionDetection mousePosition;

    private Vector3 offset;
    private float zCoord;

    private bool _isDragged = false;
    public bool isDragged { set { _isDragged = value; } }

    // Start is called before the first frame update
    void Start()
    {
        mousePosition = Camera.main.GetComponent<MousePositionDetection>();
    }

    public void InitDrag()
    {
        zCoord = Camera.main.WorldToScreenPoint(self.position).z;

        offset = self.position - GetMouseWorldPosition();
    }

    public void EndDrag()
    {
    }

    public Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = zCoord;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
    // Update is called once per frame
    void Update()
    {
        if (!_isDragged) return;

        self.position = GetMouseWorldPosition() + offset;
    }
}
