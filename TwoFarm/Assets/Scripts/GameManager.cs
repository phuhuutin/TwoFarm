using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    PlayerManager Character;

    //  private ToolSelector toolSelector;


    void Start()
    {
        // // Find the Canvas object
        // GameObject canvasObject = GameObject.Find("ToolBarCanvas");

        // // Get the ToolSelector component attached to the Canvas
        // toolSelector = canvasObject.GetComponent<ToolSelector>();

        // if (toolSelector == null)
        // {
        //     Debug.LogError("ToolSelector component not found on Canvas!");
        // }



        PlayerManager.Instance.Initialize();
        Character = PlayerManager.Instance;
        List<Vector3> spawnPositions = new List<Vector3>
        {
            new Vector3(5, 0, 0),
            new Vector3(5, 6, 0)
        };

        SkeletonManager.Instance.Initialize(spawnPositions); // Initialize skeletons at specified positions
                                                             // skeletonManager =  SkeletonManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerManager.Instance.Handle();
        SkeletonManager.Instance.Handle();

        // if (Input.GetKeyDown(KeyCode.Alpha1)) { toolSelector.SelectTool(0); }
        // if (Input.GetKeyDown(KeyCode.Alpha2)) { toolSelector.SelectTool(1); }
        // if (Input.GetKeyDown(KeyCode.Alpha3)) { toolSelector.SelectTool(2); }
        // if (Input.GetKeyDown(KeyCode.Alpha4)) { toolSelector.SelectTool(3); }


        // AudioManager.handle();

        AttackDetection();
    }

    private void AttackDetection()
    {

        foreach (SkeletonController skeletonController in SkeletonManager.Instance.GetAllSkeletons())
        {
            // if (Character._controller.getModel().AttackBox.Intersects(skeletonController._model.BodyBox) && !Character._model.notInFightStatus())
            // {
            //     Character._controller.OnSkeletonCollision(skeletonController);
            // }

            Character._controller.AttackCollisionCheck(skeletonController);
            //Character._controller.ResolveCollision(skeletonController);
        }

    }
}
