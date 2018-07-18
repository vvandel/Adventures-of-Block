//http://stackoverflow.com/questions/13708395/how-can-i-draw-a-circle-in-unity3d/30441346#30441346

using UnityEngine;
using System.Collections;

public class CircleDraw : MonoBehaviour
{
    float theta_scale = 0.01f; //Set lower to add more points
    float radius = 3f;
    int size; //Total number of points in circle
    LineRenderer lineDrawer;
    float theta = 0f;

    [SerializeField]
    Color color;

    [SerializeField]
    float width = 0.02f;

    [SerializeField]
    Material material;

    public void SetRadius(float value)
    {
        radius = value;
    }

    public void SetEnabled(bool value)
    {
        this.enabled = value;
        lineDrawer.enabled = value;
    }

    void Start()
    {
        lineDrawer = GetComponent<LineRenderer>();
        lineDrawer.material = material;
        //lineDrawer.SetColors(color, color);
        //lineDrawer.SetWidth(width, width); //thickness of line
        lineDrawer.startColor = color;
        lineDrawer.endColor = color;
        lineDrawer.startWidth = width;
        lineDrawer.endWidth = width;
    }


    void Update()
    {
        theta = 0f;
        size = (int)((1f / theta_scale) + 1f);
        Vector3 pos;
        float gOx = gameObject.transform.position.x;
        float gOy = gameObject.transform.position.y;
        //lineDrawer.SetVertexCount(size);
        lineDrawer.numPositions = size;
        for (int i = 0; i < size; i++)
        {
            theta += (2.0f * Mathf.PI * theta_scale);
            float x = radius * Mathf.Cos(theta) + gOx;
            float y = radius * Mathf.Sin(theta) + gOy;
            pos = new Vector3(x, y, 0);
            lineDrawer.SetPosition(i, new Vector3(x, y, 0));
        }
    }
}