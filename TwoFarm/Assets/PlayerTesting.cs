using UnityEngine;

public class PlayerTesting : MonoBehaviour
{
    public Animator characterAnimator;
    public Animator hairAnimator;
    public Animator toolAnimator;

    private bool isRunning = false;

    void Start()
    {
        // Initialize all animators
        if (characterAnimator == null)
            characterAnimator = GetComponent<Animator>();

        if (hairAnimator == null || toolAnimator == null)
        {
            foreach (Transform child in transform)
            {
                if (child.name == "CharactarHair")
                {
                    hairAnimator = child.GetComponent<Animator>();
                }
                else if (child.name == "CharactarTool")
                {
                    toolAnimator = child.GetComponent<Animator>();
                }
            }
        }
    }



    void Update()
    {
        // Check for Shift key press to toggle running state
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            isRunning = !isRunning;
            SwitchAnimationState();
        }
    }
    void SwitchAnimationState()
    {
        if (isRunning)
        {
            characterAnimator.Play("Run");
            hairAnimator.Play("Run_Hair");
            toolAnimator.Play("Run_Tool");
        }
        else
        {
            characterAnimator.Play("Idle");
            hairAnimator.Play("Idle_Hair");
            toolAnimator.Play("Idle_Tool");
        }
    }
}
