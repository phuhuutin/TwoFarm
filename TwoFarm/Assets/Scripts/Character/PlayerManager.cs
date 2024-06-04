using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    [SerializeField]
    private GameObject characterPrefab; // Reference to your prefab

    private CharacterView _view;
    private CharacterModel _model;
    private Controller _controller;

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


            var gameObject = Instantiate(characterPrefab);
            var CharacterTransform = gameObject.transform;

            var view = gameObject.GetComponent<CharacterView>();

            // view.ActivateGameObject();
            if (view != null)
            {   
                view.Initialize();
                view.characterTransform = CharacterTransform;
                var model = new CharacterModel();
                // model.ReadData(); // This function will be implemented in the future.
                var controller = new Controller();
                controller.SetData(model, view);
                _controller = controller;
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
        _controller?.Handle();
    }
}
