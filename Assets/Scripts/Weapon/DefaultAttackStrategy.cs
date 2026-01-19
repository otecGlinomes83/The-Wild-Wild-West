using System;
using System.Collections.Generic;

public class DefaultAttackStrategy : IAttackStrategy
{
    private AttackData _data;
    private int _damage;

   public DefaultAttackStrategy(AttackData data)
    {
        _data = data;
        _damage = _data.Damage;
    }

    public void Attack(List<IDamageable> damageables)
    {
        foreach (IDamageable damageable in damageables)
        {
            damageable.ApplyDamage(_damage);
        }
    }
}
