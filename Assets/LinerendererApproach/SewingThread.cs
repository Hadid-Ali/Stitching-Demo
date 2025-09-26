using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class SewingThread : MonoBehaviour
{
    private LineRenderer line;
    private List<Vector3> controlPoints = new List<Vector3>();

    [Range(2, 20)] public int resolution = 10; // smoothness of spline

    void Awake()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = 0;
    }
    private void OnEnable()
    {
        GameEvents.ThreadManagerEvents.onLineReset.RegisterEvent(ResetLine);
        GameEvents.ThreadManagerEvents.onDrawingThread.RegisterEvent(UpdateThreadLine);
        GameEvents.ThreadManagerEvents.onAddingStitchPoints.RegisterEvent(AddStitchPoint);
    }

    private void OnDisable()
    {
        GameEvents.ThreadManagerEvents.onLineReset.UnregisterEvent(ResetLine);
        GameEvents.ThreadManagerEvents.onDrawingThread.UnregisterEvent(UpdateThreadLine);
        GameEvents.ThreadManagerEvents.onAddingStitchPoints.UnregisterEvent(AddStitchPoint);
    }
    // Call this when the needle passes through a hole
    void AddStitchPoint(Vector3 point)
    {
        controlPoints.Add(point);
        //DrawLiveThread(controlPoints);
    }

  
    // Catmull-Rom spline
    private List<Vector3> GenerateSpline(List<Vector3> points, int subdivisions)
    {
        List<Vector3> result = new List<Vector3>();

        for (int i = 0; i < points.Count - 1; i++)
        {
            Vector3 p0 = i == 0 ? points[i] : points[i - 1];
            Vector3 p1 = points[i];
            Vector3 p2 = points[i + 1];
            Vector3 p3 = (i + 2 < points.Count) ? points[i + 2] : points[i + 1];

            for (int j = 0; j <= subdivisions; j++)
            {
                float t = j / (float)subdivisions;
                Vector3 pos = CatmullRom(p0, p1, p2, p3, t);
                result.Add(pos);
            }
        }

        return result;
    }

    private Vector3 CatmullRom(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        // Catmull-Rom spline formula
        float t2 = t * t;
        float t3 = t2 * t;

        return 0.5f * ((2f * p1) +
                       (-p0 + p2) * t +
                       (2f * p0 - 5f * p1 + 4f * p2 - p3) * t2 +
                       (-p0 + 3f * p1 - 3f * p2 + p3) * t3);
    }
    List<Vector3> livePoints = new List<Vector3>();
    [SerializeField] int totalPointsToDrawLine;
    private void UpdateThreadLine(Vector3 needlePos)
    {
        // Get all fixed stitch points
        //List<Vector3> points = controlPoints;

        // Add the needle’s live position as the last one
        livePoints.Add(needlePos);
        if (livePoints.Count >= totalPointsToDrawLine)
        {
            if (controlPoints.Count == 0)
                livePoints = new List<Vector3>();
        }

        // Draw line from holes + needle
        DrawLiveThread(livePoints);
    }
    void ResetLine()
    {
        if(controlPoints.Count == 0)
        {
            livePoints = new List<Vector3>();
            line.positionCount = 0;
        }
    }
    void DrawLiveThread(List<Vector3> livePoints)
    {
        Debug.LogError("" + livePoints.Count);

        if (livePoints.Count < 2) return;

        List<Vector3> smoothed = GenerateSpline(livePoints, resolution);
        line.positionCount = smoothed.Count;
        line.SetPositions(smoothed.ToArray());
        Debug.LogError("" + smoothed.Count);
    }

}
