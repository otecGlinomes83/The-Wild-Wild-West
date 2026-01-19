using System.Collections.Generic;

public interface IAttackStrategy
{
    public void Attack(List<IDamageable> damageables);
}