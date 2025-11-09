using System.Collections.Generic;
using UnityEngine;

public class BrickStateManager : MonoBehaviour
{
    public List<GhostBallActivator> allBricks = new List<GhostBallActivator>();


    private void Start()
    {
        CacheBricks();
    }
    public void CacheBricks()
    {
        {
            allBricks.Clear();

            GhostBallActivator[] found = FindObjectsByType<GhostBallActivator>(FindObjectsSortMode.None);

            allBricks.AddRange(found);
        }
    }

    public void ActivateGhostMode()
    {
        foreach (var brick in allBricks)
        {
            if (brick != null)
                brick.MakeBrickGhost();
        }
    }

    public void DeactivateGhostMode()
    {
        foreach (var brick in allBricks)
        {
            if (brick != null)
                brick.MakeBrickSolid();
        }
    }
}
