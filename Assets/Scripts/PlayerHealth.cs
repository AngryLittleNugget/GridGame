using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealth : MonoBehaviour 
{
    private int _health = 4;
    public int Health
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
    }
}
