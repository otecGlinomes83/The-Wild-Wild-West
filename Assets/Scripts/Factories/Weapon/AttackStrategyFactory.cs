public static class AttackStrategyFactory
{
    public static IAttackStrategy Create(AttackData attackData)
    {
        return new DefaultAttackStrategy(attackData);
    }
}
