using System.Collections;
using UnityEngine;

public interface ILineHandler : IGameService
{
    IEnumerator DrawLine(Vector3 endPoint);
    void RemoveLine(Vector3 point);
}
