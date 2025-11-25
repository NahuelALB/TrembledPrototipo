using UnityEngine;
using System.Collections;

public class HealthTimer : MonoBehaviour
{
    public HealthBar healthbar;
    int tiempo = 60;

    void Start()
    {
        healthbar.SetMaxHealth(tiempo);
        StartCoroutine(Contar());
    }

    IEnumerator Contar()
    {
        while (tiempo > 0)
        {
            tiempo--;
            healthbar.setHealth(tiempo);
            yield return new WaitForSeconds(1f);
        }
    }
}
