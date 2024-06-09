using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    [SerializeField]
    private GameObject characterPrefab; // Reference to your prefab

    private CharacterView view;
    private CharacterModel model;
    private CharacterController controller;
    private GameObject CharacterInstance;

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

    public void Initialize()
    {
        if (characterPrefab != null)
        {   


            CharacterInstance = Instantiate(characterPrefab);
            var CharacterTransform = CharacterInstance.transform;

            view = CharacterInstance.GetComponent<CharacterView>();

            // view.ActivateGameObject();
            if (view != null)
            {   
                view.Initialize();
                view.characterTransform = CharacterTransform;
                model = new CharacterModel();
                // model.ReadData(); // This function will be implemented in the future.
                controller = new CharacterController();
                controller.SetData(model, view);
            }
            else
            {
                Debug.LogError("Prefab does not have a View component.");
            }

            // Find the CameraFollow script and set the target
            CameraFollow cameraFollow = FindObjectOfType<CameraFollow>();
            if (cameraFollow != null)
            {
                cameraFollow.target = CharacterTransform;
            }
            else
            {
                Debug.LogError("No CameraFollow script found in the scene.");
            }



        }
        else
        {
            Debug.LogError("Character prefab is not assigned. Assign the prefab in the Unity Editor.");
        }
    }

    public void Handle()
    {
        controller?.Handle();
    }

    public GameObject getCharacterInstance() { return CharacterInstance; }
}
