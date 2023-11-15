using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class CircleDrawer : MonoBehaviour
{
    public float radius = 5f;
    public int segments = 30;

    void Start()
    {
        DrawCircle();
    }

    void DrawCircle()
    {
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = segments + 1;
        lineRenderer.useWorldSpace = false;

        float deltaTheta = (2f * Mathf.PI) / segments;
        float theta = 0f;

        for (int i = 0; i < segments + 1; i++)
        {
            float x = radius * Mathf.Cos(theta);
            float z = radius * Mathf.Sin(theta);

            Vector3 pos = new Vector3(x, 0f, z);
            lineRenderer.SetPosition(i, pos);

            theta += deltaTheta;
        }
    }
}
