using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthBar : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Slider slider;
    public Vector3 Offset;

    private Vector3 tempScale;
    private Transform skeletonTransform; // Reference to the skeleton's transform

    private bool facingRight = true; // Flag to track the flip state

    public void UpdateHealthBar(float currentValue, float maxValue){
        Color High = Color.green;
        Color Low = Color.red;
        slider.value = currentValue/maxValue;
    }
    void Start()
    {
                skeletonTransform = transform.parent; // Assuming the parent of the health bar is the skeleton

    }

    // Update is called once per frame
    void LateUpdate()
    {
        //transform.position = skeletonTransform.position + Offset;

        // Check if the parent skeleton has flipped
        bool currentFacingRight = skeletonTransform.localScale.x > 0; // Assuming flipping changes the local scale in X

        // If the flip state has changed, reverse the scale of the health bar
        if (currentFacingRight != facingRight)
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1; // Reverse the scale in X
            transform.localScale = scale;

            facingRight = currentFacingRight; // Update the flip state
        }
    }
}
