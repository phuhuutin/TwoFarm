using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonManager : MonoBehaviour
{
    public static SkeletonManager Instance { get; private set; }

    [SerializeField]
    private GameObject _skeletonPrefab; // Reference to your skeleton prefab
    [SerializeField]
    private bool isDebug; // Set this to false if you want to disable gizmo drawing


    private List<SkeletonController> _skeletonControllers = new List<SkeletonController>();

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

    // public void Initialize(List<Vector3> spawnPositions)
    // {
    //     if (_skeletonPrefab != null)
    //     {
    //         for (int i = 0; i < spawnPositions.Count; i++)
    //         {
    //             var gameObject = Instantiate(_skeletonPrefab, spawnPositions[i], Quaternion.identity);
    //             var skeletonTransform = gameObject.transform;

    //             var view = gameObject.GetComponent<SkeletonView>();

    //             if (view != null)
    //             {
    //                 var model = new SkeletonModel();
    //                 var controller = new SkeletonController();
    //                 view.Initialize(controller);
    //                 view.SkeletonTransform = skeletonTransform;


    //                 var characterTransform = PlayerManager.Instance.getCharacterInstance();

    //                 controller.SetData(model, view, characterTransform.transform);

    //                 _skeletonControllers.Add(controller);


    //             }
    //             else
    //             {
    //                 Debug.LogError("Prefab does not have a View component.");
    //             }
    //         }
    //     }
    //     else
    //     {
    //         Debug.LogError("Skeleton prefab is not assigned. Assign the prefab in the Unity Editor.");
    //     }
    // }

    public void Initialize(List<Vector3> spawnPositions)
    {
        if (_skeletonPrefab == null)
        {
            Debug.LogError("Skeleton prefab is not assigned. Assign the prefab in the Unity Editor.");
            return;
        }

        foreach (var spawnPosition in spawnPositions)
        {
            var gameObject = Instantiate(_skeletonPrefab, spawnPosition, Quaternion.identity);
            if (gameObject == null)
            {
                Debug.LogError("Failed to instantiate skeleton prefab.");
                continue;
            }

            var skeletonTransform = gameObject.transform;
            var view = gameObject.GetComponent<SkeletonView>();

            if (view == null)
            {
                Debug.LogError("Prefab does not have a SkeletonView component.");
                continue;
            }

            var model = new SkeletonModel();
            var controller = new SkeletonController();

            view.Initialize(controller);
            view.SkeletonTransform = skeletonTransform;

            var characterTransform = PlayerManager.Instance.getCharacterInstance();
            if (characterTransform == null)
            {
                Debug.LogError("Character Transform is null.");
                continue;
            }

            if (controller == null) Debug.Log("nu::::::::::::::::::::::");

            controller.SetData(model, view, characterTransform.transform);
            _skeletonControllers.Add(controller);

            Debug.Log("Skeleton initialized successfully.");
        }
    }

    public void Handle()
    {
        for (int i = _skeletonControllers.Count - 1; i >= 0; i--)
        {
            var controller = _skeletonControllers[i];
            if (!controller.GetModel().showOnScreen)
            {
                _skeletonControllers.RemoveAt(i);
                Debug.Log("remove from list");
            }
            else
            {

                controller.Handle();

            }

        }

        for (int i = 0; i < _skeletonControllers.Count; i++)
        {
            for (int j = i + 1; j < _skeletonControllers.Count; j++)
            {

                _skeletonControllers[i].ResolveCollision(_skeletonControllers[j]);


            }
        }




    }

    public List<SkeletonController> GetAllSkeletons()
    {
        return _skeletonControllers;
    }

    private void OnDrawGizmos()
    {
        if (isDebug)
        {
            foreach (var controller in _skeletonControllers)
            {
                controller.OnDebug();
            }
        }
    }


}