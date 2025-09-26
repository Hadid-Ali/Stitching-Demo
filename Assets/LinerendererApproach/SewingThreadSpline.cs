using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;
using System.Linq;
using System;
using Unity.Splines.Examples;


public class SewingThreadSpline : MonoBehaviour
{
    [SerializeField] SplineContainer splineContainer;
    private Spline spline;
    [SerializeField] float splineWidth;
    [SerializeField] int resolution;

    [SerializeField] float minDistance = 0.05f;
    [Range(0f, 1f)][SerializeField] float tension = 0.25f;
    private Vector3 lastPos;

    [SerializeField]
    Mesh m_SampleDot;

    [SerializeField]
    Material m_SampleMat, m_ControlPointMat;
    [SerializeField] SplineRenderer m_SplineRenderer;
    void Awake()
    {
        spline = splineContainer.Spline;
    }
    private void OnEnable()
    {
        GameEvents.SewingThreadSplineEvents.onResetSpline.RegisterEvent(ResetSpline);
        GameEvents.SewingThreadSplineEvents.onAppSplineStitchPoints.RegisterEvent(AddStitchPoint);
        GameEvents.SewingThreadSplineEvents.onRebuiltSpline.RegisterEvent(RebuildSpline);
    }

    private void OnDisable()
    {
        GameEvents.SewingThreadSplineEvents.onAppSplineStitchPoints.UnregisterEvent(AddStitchPoint);
        GameEvents.SewingThreadSplineEvents.onResetSpline.UnregisterEvent(ResetSpline);
        GameEvents.SewingThreadSplineEvents.onRebuiltSpline.UnregisterEvent(RebuildSpline);
    }
    void AddStitchPoint(Vector3 worldPos)
    {
        Vector3 localPos = splineContainer.transform.InverseTransformPoint(worldPos);
        m_SplineRenderer.drawSample = false;
        if (spline.Count == 0 || Vector3.Distance(worldPos, lastPos) > minDistance)
        {
            localPos.z = -1;
            m_Stroke.Add(localPos);
            spline.Add(new BezierKnot(localPos));


            var all = new SplineRange(0, spline.Count);
            spline.SetTangentMode(all, TangentMode.AutoSmooth);
            spline.SetAutoSmoothTension(all, tension);

            lastPos = worldPos;

        }

        RebuildSpline();

    }

    public void GetSampledPoints(int samples = 20)
    {
        List<Vector3> points = new List<Vector3>();

        for (int i = 0; i <= samples; i++)
        {
            float t = i / (float)samples;
            Vector3 pos = spline.EvaluatePosition(t);
            points.Add(splineContainer.transform.TransformPoint(pos));
        }

    }

    void ResetSpline()
    {
        spline.Clear();
        m_Stroke.Clear();
        m_SplineRenderer.ResetLine();
    }
    [SerializeField] List<float3> m_Stroke = new List<float3>(1024);
    List<float3> m_Reduced = new List<float3>(512);
    // Point reduction epsilon determines how aggressive the point reduction algorithm is when removing redundant
    // points. Lower values result in more accurate spline representations of the original line, at the cost of
    // greater number knots.
    [Range(0f, 1f), SerializeField]
    float m_PointReductionEpsilon = .15f;

    // Tension affects how "curvy" splines are at knots. 0 is a sharp corner, 1 is maximum curvitude.
    [Range(0f, 1f), SerializeField]
    float m_SplineTension = 1 / 4f;
    void RebuildSpline()
    {
        // Before setting spline knots, reduce the number of sample points.
        SplineUtility.ReducePoints(m_Stroke, m_Reduced, m_PointReductionEpsilon);
        var spline = splineContainer.Spline;
        //Debug.LogError(" " + spline.Count);
        // Assign the reduced sample positions to the Spline knots collection. Here we are constructing new
        // BezierKnots from a single position, disregarding tangent and rotation. The tangent and rotation will be
        // calculated automatically in the next step wherein the tangent mode is set to "Auto Smooth."
        spline.Knots = m_Reduced.Select(x => new BezierKnot(x));

        var all = new SplineRange(0, spline.Count);

        // Sets the tangent mode for all knots in the spline to "Auto Smooth."
        spline.SetTangentMode(all, TangentMode.AutoSmooth);

        // Sets the tension parameter for all knots. Note that the "Tension" parameter is only applicable to
        // "Auto Smooth" mode knots.
        spline.SetAutoSmoothTension(all, m_SplineTension);

    }

}
