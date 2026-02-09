using System;
using UnityEngine;

public class AnimatorProxy : MonoBehaviour
{
    public event Action AttackPerformed;

    public void PerformAttack() =>
        AttackPerformed?.Invoke();
}