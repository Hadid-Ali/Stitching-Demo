using UnityEngine;

public interface IRaycastDetector : IGameService
{
    public bool isInTrigger { get; }
    public bool stopLine { get; }

    void IsInTrigger(bool val);
    void StopLinerenderer(bool val);
    bool IsStopped();
    bool GetTriggerVal();
}
