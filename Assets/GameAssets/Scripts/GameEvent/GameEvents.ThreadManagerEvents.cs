using UnityEngine;

public static partial class GameEvents
{
    public static class ThreadManagerEvents
    {
        public static readonly GameEvent onLineReset = new();
        public static readonly GameEvent<Vector3> onDrawingThread = new();
        public static readonly GameEvent<Vector3> onAddingStitchPoints = new();
    }
}

