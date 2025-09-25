using System.Net.Sockets;
using UnityEngine;

public class RaycastDetector : MonoBehaviour
{
    [SerializeField] float distance;
    [SerializeField]LayerMask layerMask;
    [SerializeField] bool locked = false;

    private void OnEnable()
    {
        GameEvents.RaycastDetectorEvents.onResetNeedle.RegisterEvent(ResetNeedle);
        GameEvents.RaycastDetectorEvents.onUpdateNeedle.RegisterEvent(UpdateNeedle);
    }

    private void OnDisable()
    {
        GameEvents.RaycastDetectorEvents.onResetNeedle.UnregisterEvent(ResetNeedle);
        GameEvents.RaycastDetectorEvents.onUpdateNeedle.UnregisterEvent(UpdateNeedle);
    }
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastPoints(Input.mousePosition);
        }
    }
    RaycastHit hit;
    [SerializeField] GameObject needle = null;

    private void RaycastPoints(Vector3 touchPoint)
    {
        Ray ray = Camera.main.ScreenPointToRay(touchPoint);
        Debug.DrawRay(ray.origin, ray.direction * distance, Color.red);
        if (Physics.Raycast(ray.origin, ray.direction,out hit, distance, layerMask))
        {
            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("needle") && !locked)
                {
                    locked = true;
                    needle = hit.collider.gameObject;
                }

                if (needle)
                {
                    Vector3 pos = hit.point;
                    pos.z = -1;
                    needle.transform.position = pos;
                }
                
                if (hit.collider.CompareTag("StitchPoint"))
                {
                    Debug.LogError("Stitch point "+hit.collider.gameObject.name);
                }
            }
        }
      
    }

    void ResetNeedle()
    {
        needle.tag = "Untagged";
        needle = null;
        locked = false;
    }

    void UpdateNeedle(GameObject n_obj)
    {
        needle = n_obj;
    }
}
