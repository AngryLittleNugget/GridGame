using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static event System.Action HealthChange;
    private float _health = 4;
    public float maxHealth = 4;
    public float Health
    {
        get { return _health; }//
        set
        {
            _health = Mathf.Clamp(value, 0, 4);
            Debug.Log($"{_health}");

            if (_health <= 0)
            {
                Destroy(gameObject);
            }

        }
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log($"{Health}");
        }
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        HealthChange?.Invoke();
    }

    public void HealHealth(int heal)
    {
        Debug.Log("Using Health Item");
        Health += heal;
        HealthChange?.Invoke();
    }
}
