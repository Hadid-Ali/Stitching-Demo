using DG.Tweening;
using UnityEngine;

public class ClothHandler :MonoBehaviour, IClothHandler
{
    [SerializeField] Transform part1;
    [SerializeField] Transform part2;

    private void OnEnable()
    {
        RegisterService();
    }

    private void OnDisable()
    {
        UnRegisterService();
    }
    public void RegisterService()
    {
        ServiceLocator.RegisterService<IClothHandler>(this);
    }

    public void UnRegisterService()
    {
        ServiceLocator.UnRegisterService<IClothHandler>(this);
    }

    private Tween moveTween;
    [SerializeField] float speed;
    public void MovePart1(Vector3 targetPos, Vector3 rotation)
    {
        if (targetPos == null)
        {
            return;
        }

        if (moveTween != null && moveTween.IsActive())
            moveTween.Kill();

        Sequence seq = DOTween.Sequence();

        seq.Join(part1.transform.DOMove(targetPos, speed).SetEase(Ease.Linear));

        seq.Join(part1.transform.DORotate(rotation, speed).SetEase(Ease.Linear));

        moveTween = seq;
        //part1.transform.position = targetPos;
        //part1.transform.eulerAngles = rotation;
    }
}
