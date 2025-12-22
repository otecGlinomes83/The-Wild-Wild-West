using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class Inventory : MonoBehaviour
{

}

public class Axe
{
    [SerializeField] private int _damage;
    [SerializeField] private float _fireRate = 0.25f;

    private WaitForSecondsRealtime _fireRateCooldown;

    private bool _isAbleToAttack = true;

    public void Initialize()
    {
        _fireRateCooldown = new WaitForSecondsRealtime(_fireRate);
    }

    public void Attack()
    {
        if (_isAbleToAttack == false)
            return;


    }

    private IEnumerator WaitCooldown(WaitForSecondsRealtime cooldown, Action onComplete)
    {
        _isAbleToAttack = false;
        yield return cooldown;
        _isAbleToAttack = true;
        onComplete?.Invoke();

        yield break;
    }
}

[Serializable]
public class Detector
{
    [SerializeField] private LayerMask _detectingLayer;
    [SerializeField] private LayerMask _obstacleLayer;

    [SerializeField] private Transform _overlapArea;
    [SerializeField, Min(0f)] private float _overlapRadius = 1f;

    [SerializeField] private int _maxDetections = 20;

    private Collider[] _colliders;

    private int _resultDetectionsCount;

    public void Initialize()
    {
        _colliders = new Collider[_maxDetections];
    }

    private List<Health> TryDetectEnemies()
    {
        List<Health> result = new List<Health>();

        _resultDetectionsCount = Physics.OverlapSphereNonAlloc(_overlapArea.position, _overlapRadius, _colliders, _detectingLayer);

        for (int i = 0; i < _resultDetectionsCount; i++)
        {
            if (_colliders[i].TryGetComponent(out Health health) == false)
                continue;

            Vector3 colliderPosition = _colliders[i].transform.position;
            bool isObstacle = Physics.Linecast(_overlapArea.transform.position, colliderPosition, _obstacleLayer);

            if (isObstacle)
                continue;

            result.Add(health);
        }

        return result;
    }
}

public class GAY : MonoBehaviour
{
    [SerializeField] private TwoBoneIKConstraintData _contraints1;
    [SerializeField] private TwoBoneIKConstraint _contraints;
    [SerializeField] private Transform _target;
    [SerializeField] private Transform _hint;

    private void Awake()
    {
        _contraints1.target = _target;
        _contraints1.hint = _hint;  

        _contraints.data = _contraints1;
    }
}