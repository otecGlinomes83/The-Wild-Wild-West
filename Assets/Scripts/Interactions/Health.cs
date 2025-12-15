using UnityEngine;

public class Health : MonoBehaviour
{
    public void TakeDamage(int damage)
    {
        Debug.Log($"Damage taken! {damage}");
    }
}
