using UnityEngine;
using Obi;

public class RopePuller : MonoBehaviour
{
    public ObiRope rope;
    public ObiRopeCursor cursor;
    public float retractSpeed = 0.5f;
    public float minLength = 0.5f;

    private float currentLength;

    void Start()
    {
        if (rope != null)
        {
            currentLength = rope.restLength;
        }
    }

    void Update()
    {
        if (cursor != null)
        {
            // Simulate retracting the rope into the hole
            currentLength -= retractSpeed * Time.deltaTime;
            currentLength = Mathf.Max(minLength, currentLength);

            cursor.ChangeLength(currentLength);
        }
    }
}
