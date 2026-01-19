using System;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageableDetector
{
    public event Action<HitInfo> Hit;

    public List<IDamageable> Detect();
}