using UnityEngine;

public static partial class GameEvents
{
   public static class RaycastDetectorEvents
    {
        public static readonly GameEvent onResetNeedle = new();
        public static readonly GameEvent<GameObject> onUpdateNeedle = new();
    }
}
