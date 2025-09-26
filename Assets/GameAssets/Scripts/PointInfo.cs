using DG.Tweening;
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
    [SerializeField] Vector3 minReducedLength;
    [SerializeField] Vector3 headPosTarget;
    [SerializeField] Vector3 headRotTarget;
    [SerializeField] bool chnageClothPos;
    [SerializeField] MeshRenderer pointMesh;

    [SerializeField] Vector3 secondRopePos;
    [SerializeField] Transform secondRope;
    [SerializeField] float speed;
    public void ReduceThreadLength()
    {
        Vector3 pos = startPointTransform.localPosition;

        startPointTransform.DOMove(minReducedLength, speed).SetEase(Ease.Linear).OnComplete(() =>
        {

        });
        secondRope.DOMove(secondRopePos, speed).SetEase(Ease.Linear).OnComplete(() =>
        {

        });
        //startPointTransform.localPosition = minReducedLength;
        //secondRope.localPosition = secondRopePos;

        if (chnageClothPos)
        {
            var clothHandler = ServiceLocator.GetService<IClothHandler>() as ClothHandler;
            clothHandler?.MovePart1(headPosTarget,headRotTarget);

            pointMesh.gameObject.SetActive(false);
        }
    }
   
}
    

    
