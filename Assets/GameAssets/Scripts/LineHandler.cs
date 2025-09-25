using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineHandler : MonoBehaviour, ILineHandler
{
    [SerializeField] LineRenderer lineRenderer;
    public LineRenderer Renderer => lineRenderer;

    [SerializeField] float speed = 0;
    [SerializeField] List<Vector3> points;
    [SerializeField] Vector3 startPoint;
    public IEnumerator DrawLine(Vector3 endPoint)
    {
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * speed;
            Vector3 pos = Vector3.Lerp(startPoint, endPoint, t);
            points.Add(pos);
            lineRenderer.positionCount = points.Count;
            lineRenderer.SetPositions(points.ToArray());
            yield return null;
        }
    }
    public void RemoveLine(Vector3 point)
    {
    }
    public void RegisterService()
    {
       ServiceLocator.RegisterService<ILineHandler>(this);
    }

   

    public void UnRegisterService()
    {
        ServiceLocator.UnRegisterService<ILineHandler>(this);
    }


}
