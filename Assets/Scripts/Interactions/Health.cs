using System;
using UnityEngine;

public class Health : MonoBehaviour, IChangeObservable
{
    [SerializeField] private int _maxHealth;

    private int _currentHealth;

    public event Action<int, int> ValueChanged;

    public int MaxHealth => _maxHealth;
    public int CurrentHealth => _currentHealth;

    private void Start()
    {
        _currentHealth = _maxHealth;
        ValueChanged?.Invoke(_currentHealth,_maxHealth);
    }

    public void Reset()
    {
        _currentHealth = _maxHealth;
        ValueChanged?.Invoke(_currentHealth,_maxHealth);
    }

    public void TakeDamage(int damage)
    {
        if (damage <= 0)
            return;

        _currentHealth = Mathf.Clamp(_currentHealth - damage, 0, _maxHealth);

        ValueChanged?.Invoke(_currentHealth, _currentHealth);
    }

    public void Heal(int healAmount)
    {
        if (healAmount <= 0)
            return;

        _currentHealth = Mathf.Clamp(_currentHealth + healAmount, 0, _maxHealth);
        ValueChanged?.Invoke(_currentHealth, _currentHealth);
    }
}
