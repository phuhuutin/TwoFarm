using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       PlayerManager.Instance.Initialize();

        List<Vector3> spawnPositions = new List<Vector3>
        {
            new Vector3(5, 0, 0),
            new Vector3(5, 6, 0)
        };

        SkeletonManager.Instance.Initialize(spawnPositions); // Initialize skeletons at specified positions
    }
    
    // Update is called once per frame
    void Update()
    {
        PlayerManager.Instance.Handle();
        SkeletonManager.Instance.Handle();

        // AudioManager.handle();
    }
}
