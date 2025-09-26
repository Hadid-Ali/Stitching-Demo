using UnityEngine;

public interface IClothHandler : IGameService
{
    public void MovePart1(Vector3 targetPos, Vector3 rotation);
}
