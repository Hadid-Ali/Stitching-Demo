using Obi;
using UnityEngine;

public class DummyRopeHandler : MonoBehaviour
{
    [SerializeField] ObiRope rope;
    [SerializeField] ObiRopeCursor ropeCursor;
    [SerializeField] float speed;
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            ropeCursor.ChangeLength(speed);
        }
    }
}
