using System;
using UnityEngine;

public class AnimatorProxy : MonoBehaviour
{
    public event Action AttackPerformed;
    public event Action AttackStarted;
    public event Action AttackFinished;

    public void PerformAttack() =>
        AttackPerformed?.Invoke();

    public void StartAttack() =>
        AttackStarted?.Invoke();

    public void FinishAttack() =>
        AttackFinished?.Invoke();
}