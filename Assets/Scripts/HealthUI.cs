using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    private Canvas canvas;
    private Transform pHealthBarTransform;
    public Image pHealthGreen;
    private PlayerHealth pHealth;

    void OnEnable()
    {
        PlayerHealth.OnDamage += HealthBarUpdate;
    }

    void OnDisable()
    {
        PlayerHealth.OnDamage -= HealthBarUpdate;
    }
    void Start()
    {
        pHealthBarTransform = gameObject.transform;
        pHealthGreen = pHealthBarTransform.Find("PHealthGreen").GetComponent<Image>();  //GameObject.FindWithTag("PlayerHealth").GetComponentInChildren<Image>();
        canvas = FindAnyObjectByType<Canvas>();
        pHealth = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (pHealth == null)
        {
            pHealth = FindAnyObjectByType<PlayerHealth>();
            if (pHealth == null)
            {
                return;
            }
            Debug.Log("pHealthNull");
        }
    }

    private void HealthBarUpdate()
    {
        pHealthGreen.fillAmount = pHealth.Health / pHealth.maxHealth;
    }
}
