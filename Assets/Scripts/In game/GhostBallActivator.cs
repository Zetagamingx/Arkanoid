using UnityEngine;

public class GhostBallActivator : MonoBehaviour
{
    private BoxCollider brickCollider;
    private Bricks brickScript;


    public void Awake()
    {
        brickCollider = GetComponent<BoxCollider>();
        brickScript = GetComponent<Bricks>();
    }

    public void MakeBrickGhost()
    {
        brickCollider.isTrigger = true;
    }

    public void MakeBrickSolid()
    {
        brickCollider.isTrigger = false;
    }
}
