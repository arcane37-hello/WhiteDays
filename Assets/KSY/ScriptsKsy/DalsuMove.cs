using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class DalsuMove : MonoBehaviour
{
    public enum CurrentState { idle, patrol, chase, attack, chaseStart, goHome };
    CurrentState curState;

    public int dsdmg = 1;

    Animator dsanim;

    public AudioClip dsHuh;
    public AudioClip dsHmm;
    public AudioClip dsStep;
    public AudioClip dsKey;
    public AudioClip dsWs;
    public AudioClip dsAt;
    public AudioClip dsAtss;

    public string playerTag = "Player";

    public float currentSpeed;
    public float patrolSpeed = 0.8f;
    public float chaseSpeed = 1.8f;
    public GameObject hm;
    public GameObject home;

    private List<AudioSource> dsAs = new List<AudioSource>();
    private Transform dsTransform;
    private Transform pTransform;
    private NavMeshAgent dsnvAgent;
    private float dist;
    private PlayerHealth playerHealth;

    //추적 사정 거리
    public float chaseDist = 25.0f;
    //공격 사정 거리
    private float attackDist = 1.5f;
    private float atSSDist = 1.5f;

    bool canAt = true;
    public bool isChase = false;


    void Start()
    {

        dsTransform = GetComponent<Transform>();
        dsnvAgent = GetComponent<NavMeshAgent>();
        dsnvAgent.updateRotation = false;
        dsanim = GetComponent<Animator>();
        hm = GameObject.FindWithTag("Player");
        playerHealth = hm.GetComponent<PlayerHealth>();
        curState = CurrentState.patrol;
        dsnvAgent.stoppingDistance = attackDist;



    }



    void Update()
    {
        pTransform = hm.transform;
        if (playerHealth.ishiding == false)
        {
            if (dsnvAgent.velocity.sqrMagnitude > Mathf.Epsilon)
            {
                dsTransform.rotation = Quaternion.LookRotation(dsnvAgent.velocity.normalized);
            }
            CheckState();
            CheckStateForAction();
        }
        else
        {
            GoHome();
        }
    }

    void CheckState()
    {
        dist = Vector3.Distance(dsTransform.position, pTransform.position);

        if (dist <= attackDist)
        {
            curState = CurrentState.attack;
        }
        else if (dist <= chaseDist)
        {
            RaycastHit hit;
            Vector3 raydir = (pTransform.position - dsTransform.position).normalized;
            if (Physics.Raycast(dsTransform.position, raydir, out hit, chaseDist))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    if (curState == CurrentState.chase)
                    {
                        curState = CurrentState.chase;
                    }
                    else if (curState == CurrentState.chaseStart)
                    {
                        curState = CurrentState.chase;
                    }
                    else
                    {
                        curState = CurrentState.chaseStart;
                    }
                }
            }
        }
    }

    void CheckStateForAction()
    {
        switch (curState)
        {
            case CurrentState.attack:
                Attack();
                break;
            case CurrentState.patrol:
                if (isChase == true)
                {
                    isChase = false;
                }
                Patrol();
                break;
            case CurrentState.chaseStart:
                isChase = true;
                ChaseStart();
                break;
            case CurrentState.chase:
                isChase = true;
                Chase();
                break;
            case CurrentState.goHome:
                if (isChase == true)
                {
                    isChase = false;
                }
                GoHome();
                break;


        }
    }

    void GoHome()
    {
        AudioSource.PlayClipAtPoint(dsHmm, dsnvAgent.transform.position);
        dsanim.SetTrigger("goPatrol");
        currentSpeed = patrolSpeed;
        dsnvAgent.speed = currentSpeed;
        dsnvAgent.SetDestination(home.transform.position);
        Destroy(gameObject, 5f);
    }


    void Patrol()
    {
        dsanim.SetTrigger("goPatrol");
        currentSpeed = patrolSpeed;
        dsnvAgent.speed = currentSpeed;
        dsnvAgent.SetDestination(pTransform.position);
    }

    void ChaseStart()
    {
        AudioSource.PlayClipAtPoint(dsHuh, dsnvAgent.transform.position);
        AudioSource.PlayClipAtPoint(dsWs, dsnvAgent.transform.position);
        dsanim.SetTrigger("goChase");
        curState = CurrentState.chase;
    }

    void Chase()
    {
        dsanim.SetTrigger("goCh2");
        currentSpeed = chaseSpeed;
        dsnvAgent.speed = currentSpeed;
        dsnvAgent.SetDestination(pTransform.position);
    }


    void Attack()
    {
        currentSpeed = 0;
        dsnvAgent.isStopped = true;
        dsnvAgent.ResetPath();
        dsanim.SetTrigger("goAt");


    }

    void DSFootStep()
    {
        AudioSource.PlayClipAtPoint(dsStep, dsnvAgent.transform.position, 0.6f);
        AudioSource.PlayClipAtPoint(dsKey, dsnvAgent.transform.position, 1.3f);

    }

    IEnumerator DSWS()
    {
        while (curState == CurrentState.chase)
        {
            AudioSource.PlayClipAtPoint(dsWs, dsnvAgent.transform.position);
            yield return new WaitForSeconds(6);
        }
    }

    void DSAT()
    {
        AudioSource.PlayClipAtPoint(dsAt, dsnvAgent.transform.position);
    }
    void DSATYES()
    {
        dist = Vector3.Distance(dsTransform.position, pTransform.position);
        if (dist <= atSSDist)
        {
            AudioSource.PlayClipAtPoint(dsAtss, dsnvAgent.transform.position);
            playerHealth.TakeDamage(dsdmg);
        }
        else
        {
            dsanim.SetTrigger("goAt22");
            curState = CurrentState.chase;
        }

    }





}
