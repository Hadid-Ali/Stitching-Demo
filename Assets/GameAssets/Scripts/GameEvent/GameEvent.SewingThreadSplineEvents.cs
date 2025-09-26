using UnityEngine;

public static partial class GameEvents
{
    public static class SewingThreadSplineEvents
    {
        public static readonly GameEvent<Vector3> onAppSplineStitchPoints = new();
        public static readonly GameEvent onResetSpline = new();
        public static readonly GameEvent onRebuiltSpline = new();
    }
}

