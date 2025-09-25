using Obi;
using System.Net.Mail;
using UnityEngine;

public class PointTrigger : MonoBehaviour
{
    [SerializeField] ObiRope rope;
    [SerializeField] ObiRopeCursor ropeCursor;
    [SerializeField]ObiParticleAttachment attachment;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("StitchPoint"))
        {
            //Debug.LogError("point " + other.gameObject.name);
            PointInfo info = other.GetComponent<PointInfo>();
            if (info)
            {
                this.transform.SetParent(other.transform);
                this.transform.localPosition = Vector3.zero;

                other.tag = "Untagged";
                if (attachment.particleGroup.name.Equals("start"))
                {
                    float currentLength = rope.CalculateLength();
                    float changedLength = currentLength + 12;
                    ropeCursor.ChangeLength(changedLength);
                    GameEvents.RaycastDetectorEvents.onResetNeedle.RaiseEvent();
                }


            }
        }
    }
}
