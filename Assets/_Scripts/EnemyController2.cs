using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController2 : MonoBehaviour
{
    #region Fields
    NavMeshAgent _navAgent;
    EnemyStates enemyState;
    Animator animator;
    Health enemyHealth;
    [SerializeField] Transform target;
    [SerializeField] GameObject _attackPoint, projectile;
    [SerializeField] float _runSpeed = 4f, _chaseDistance = 20f;
    [SerializeField] float timeBtwAttacks = 2f;
    float spreadRate;
    float _attackTimer;
    #endregion

    #region Unity Methods
    private void Awake()
    {
        AwakeRef();
    }
    private void Start()
    {
        enemyState = EnemyStates.CHASE;
    }
    private void Update()
    {
        _attackTimer += Time.deltaTime;
        if (enemyState == EnemyStates.CHASE)
        {
            Chase();
        }
        else if (enemyState == EnemyStates.ATTACK)
        {
            Attack();
        }
        if (enemyHealth.isDead)
        {
            _navAgent.isStopped = true;
        }
    }
    #endregion

    #region Private Methods
    private void Chase()
    {
        //enemymize yurume ozelligi ve hizi veriyoruz.
        _navAgent.isStopped = false;
        _navAgent.speed = _runSpeed;
        //player'imiza dogru kosacak
        _navAgent.SetDestination(target.position);//enemyi playera dogru yurutuyoruz.

        if (_navAgent.velocity.sqrMagnitude > 0)//navagent hareket ediyorsa
        //walk true
        {
            animator.SetBool("Run", true);
        }
        //playere yeterince yakinsak attack yapacagiz.
        if (Vector3.Distance(transform.position, target.position) <= _chaseDistance)
        {
            enemyState = EnemyStates.ATTACK;
        }
    }
    private void Attack()
    {
        //enemy'i durduruyoruz.
        _navAgent.isStopped = true;
        _navAgent.velocity = Vector3.zero;
        animator.SetBool("Run", false);

        transform.LookAt(target);
        if (_attackTimer > timeBtwAttacks)
        {
            _attackTimer = 0f;
            animator.SetTrigger("Shoot");
            spreadRate = UnityEngine.Random.Range(-.5f, .5f);
            Rigidbody rb = Instantiate(projectile, _attackPoint.transform.position + Vector3.one * spreadRate, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            Destroy(rb.gameObject, 2f);
        }
        //player kactigi zaman yapilacak islemler
        if (Vector3.Distance(transform.position, target.position) >= _chaseDistance)
        {
            enemyState = EnemyStates.CHASE;
        }
    }

    void AwakeRef()
    {
        _navAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        enemyHealth = GetComponent<Health>();
    }
}
#endregion
public enum EnemyStates
{
    CHASE, ATTACK, DEAD
}