using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonManager : MonoBehaviour
{
    public static SkeletonManager Instance { get; private set; }

    [SerializeField]
    private GameObject skeletonPrefab; // Reference to your skeleton prefab

    private List<SkeletonController> skeletonControllers = new List<SkeletonController>();

    private void Awake()
    {
        // Initialize singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Destroy the duplicate
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Make the instance persistent
        }
    }

    public void Initialize(List<Vector3> spawnPositions)
    {
        if (skeletonPrefab != null)
        {
            for (int i = 0; i < spawnPositions.Count; i++)
            {
                var gameObject = Instantiate(skeletonPrefab, spawnPositions[i], Quaternion.identity);
                var skeletonTransform = gameObject.transform;

                var view = gameObject.GetComponent<SkeletonView>();

                if (view != null)
                {
                    view.Initialize();
                    view.skeletonTransform = skeletonTransform;

                    var model = new SkeletonModel();
                    var controller = new SkeletonController();
                    var characterTransform = PlayerManager.Instance.getCharacterInstance();

                    controller.SetData(model, view, characterTransform.transform);

                    skeletonControllers.Add(controller);
                }
                else
                {
                    Debug.LogError("Prefab does not have a View component.");
                }
            }
        }
        else
        {
            Debug.LogError("Skeleton prefab is not assigned. Assign the prefab in the Unity Editor.");
        }
    }

    public void Handle()
    {
        foreach (var controller in skeletonControllers)
        {
            controller.Handle();
        }
    }
}