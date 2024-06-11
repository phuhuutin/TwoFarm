using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    [SerializeField]
    private GameObject _characterPrefab; // Reference to your prefab

    private CharacterView _view;
    private CharacterModel _model;
    private CharacterController _controller;
    private GameObject _characterInstance;

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
        if (_characterPrefab != null)
        {   


            _characterInstance = Instantiate(_characterPrefab);
            var CharacterTransform = _characterInstance.transform;

            _view = _characterInstance.GetComponent<CharacterView>();

            // view.ActivateGameObject();
            if (_view != null)
            {   
                _controller = new CharacterController();

                _view.Initialize(_controller);
                _view.CharacterTransform = CharacterTransform;
                _model = new CharacterModel();
                // model.ReadData(); // This function will be implemented in the future.
                _controller.SetData(_model, _view);
            }
            else
            {
                Debug.LogError("Prefab does not have a View component.");
            }

            // Find the CameraFollow script and set the target
            CameraFollow cameraFollow = FindObjectOfType<CameraFollow>();
            if (cameraFollow != null)
            {
                cameraFollow.Target = CharacterTransform;
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

    public GameObject getCharacterInstance() { return _characterInstance; }
}
