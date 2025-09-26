using NUnit.Framework;
using Obi;
using Unity.VisualScripting;
using UnityEngine;

public class PointInfo : MonoBehaviour
{
    public ObiParticleAttachment startPoint;
    public ObiRopeCursor ropeCursor;
    public ObiRope rope;
    public ParticleSystem effect;
    public bool startAnimation;
    [SerializeField] Transform startPointTransform;
    [SerializeField] float minReducedLength;
    void ReduceThreadLength()
    {
        Vector3 pos = startPointTransform.localPosition;
        if (minReducedLength > 0)
        {
            if(Mathf.Abs(pos.y) < Mathf.Abs(minReducedLength))
                pos.y++;
        }
        else
        {
            if (Mathf.Abs( pos.y) < Mathf.Abs( minReducedLength))
                pos.y--;
        }

        startPointTransform.localPosition = new Vector3(pos.x, pos.y, pos.z);

    }
    private void Update()
    {
        if (startAnimation)
        {
            ReduceThreadLength();
        }
    }
}
    

    
