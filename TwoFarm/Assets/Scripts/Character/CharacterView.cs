using Assets.Scripts.Interfaces;
using UnityEngine;
namespace Assets.Scripts.Character
{
    public class CharacterView : MonoBehaviour, IView
    {
        public Animator CharacterAnimator;
        public Animator HairAnimator;
        public Animator ToolAnimator;
        public Transform CharacterTransform { get; set; } // Reference to the character's transform for flipping
        [SerializeField]
        public Vector2 BodyBoxSize;
        [SerializeField]
        public Vector2 AttackBoxSize;
        [SerializeField]
        public Vector2 AttackBoxOffset;

        public ToolSelector ToolSelector;



        public void Initialize()
        {

            if (CharacterAnimator == null)
                CharacterAnimator = this.gameObject.GetComponent<Animator>();
            // Initialize all animators
            if (HairAnimator == null || ToolAnimator == null)
            {
                foreach (Transform child in transform)
                {
                    if (child.name == "CharacterHair")
                    {
                        HairAnimator = child.GetComponent<Animator>();
                    }
                    else if (child.name == "CharacterTool")
                    {
                        ToolAnimator = child.GetComponent<Animator>();
                    }
                }
            }


            GameObject canvasObject = GameObject.Find("ToolBarCanvas");

            // Get the ToolSelector component attached to the Canvas
            ToolSelector = canvasObject.GetComponent<ToolSelector>();

            if (ToolSelector == null)
            {
                Debug.LogError("ToolSelector component not found on Canvas!");
            }



        }

        public void FlipTransform(bool facingRight)
        {
            Vector3 scale = CharacterTransform.localScale;
            scale.x = facingRight ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
            CharacterTransform.localScale = scale;
        }
        public void SetPosition(Vector2 modelMoveSpeed)
        {
            transform.Translate(modelMoveSpeed);
        }
        public void ActivateGameObject()
        {
            // Activate the GameObject
            gameObject.SetActive(true);
        }

        public void PlayAnimation(AnimationType animation)
        {
            CharacterAnimator.Play(animation + "");
            HairAnimator.Play(animation + "_Hair");
            ToolAnimator.Play(animation + "_Tool");
        }


        public Transform GetTransform()
        {
            return CharacterTransform;
        }

        public MonoBehaviour GetMonoBehaviour()
        {
            return this;
        }



        public Vector2 getBodyBoxSize()
        {
            return BodyBoxSize;
        }
    }
}
