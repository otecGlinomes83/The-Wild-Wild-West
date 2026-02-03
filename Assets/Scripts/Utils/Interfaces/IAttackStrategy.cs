using System;
using System.Collections.Generic;

public interface IAttackStrategy
{
    public void Attack(List<IDamageable> damageables);
}

public interface IPooled
{
    public void Reset();
}