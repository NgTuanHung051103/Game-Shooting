using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetAnimationRun : MonoBehaviour
{
    private Animator _animator;
    void Start()
    {
        _animator = GetComponent<Animator>();    
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Bullet"))
        {   
            if( _animator && _animator.speed > 0)
            {
                _animator.speed = 0;
                Game2Controller.Instance.ShootOnTarget();
            }
            else if( !_animator)
            {
                Destroy(gameObject);
                Game2Controller.Instance.ShootOnTarget();
            }
        }
    }
}