
using UnityEngine;
using UnityEngine.UI;

public class HealthBarBehavior : MonoBehaviour
{   

    public Slider Slider;
    public Color Low;
    public Color High;

    public Vector3 Offset;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetHealth(float health, float maxHealth){
        Slider.gameObject.SetActive(true);
        Slider.value = health/maxHealth;
    
        Debug.Log(health + "|" + maxHealth );
       // Slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(Low, High, Slider.normalizedValue);

    }

    // Update is called once per frame
    void Update()
    {

      //  Slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + Offset);
    }
}
