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

    public LayerMask playerLayer;

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
    //private 플레이어스크립트이름 playerSc;
    private float dist;
    private PlayerHealth playerHealth;

    //추적 사정 거리
    public float chaseDist = 25.0f;
    //공격 사정 거리
    private float attackDist = 1.2f;
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
        if(playerHealth.ishiding == false)
        {
            if (dsnvAgent.velocity.sqrMagnitude > Mathf.Epsilon)
            {
                dsTransform.rotation = Quaternion.LookRotation(dsnvAgent.velocity.normalized);
            }
            StartCoroutine(CheckState());
            StartCoroutine(CheckStateForAction());
        }
        else
        {
            GoHome();
        }
    }

    IEnumerator CheckState()
    {
        dist = Vector3.Distance(dsTransform.position, pTransform.position);
        yield return new WaitForSeconds(0.25f);
        if (dist <= attackDist)
        {
            curState = CurrentState.attack;
        }
        else if (dist <= chaseDist)
        {
            RaycastHit hit;
            Vector3 raydir = (pTransform.position - dsTransform.position).normalized;
            if (Physics.Raycast(dsTransform.position, raydir, out hit, chaseDist, playerLayer))
                if (hit.collider.CompareTag("Player") && playerHealth.ishiding == false)
                {
                    if (curState == CurrentState.chase)
                    {
                        curState = CurrentState.chase;
                    }
                    else if(curState == CurrentState.chaseStart)
                    {
                        curState = CurrentState.chase;
                    }    
                    else
                    {
                        curState = CurrentState.chaseStart;
                    }
                }
                else if (hit.collider.CompareTag("Player") && playerHealth.ishiding == true)
                {
                    curState = CurrentState.goHome;
                }
        }
        else
        {
            curState = CurrentState.patrol;
        }
    }

    IEnumerator CheckStateForAction()
    {
        switch (curState)
        {
            case CurrentState.attack:
                Attack();
                break;
            case CurrentState.patrol:
                if(isChase == true)
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
                Chase();
                break;
            case CurrentState.goHome:
                GoHome();
                break;


        }
        yield return null;
    }

    void GoHome()
    {
        dsnvAgent.isStopped = true;
        AudioSource.PlayClipAtPoint(dsHmm, dsnvAgent.transform.position);
        dsanim.SetTrigger("goId2");
        dsanim.SetTrigger("goIdle1");
        dsnvAgent.isStopped = false;
        dsanim.SetTrigger("goPatrol");
        currentSpeed = patrolSpeed;
        dsnvAgent.speed = currentSpeed;
        dsnvAgent.SetDestination(home.transform.position);
        Destroy(gameObject, 10f);
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
    }

    void Chase()
    {
        dsanim.SetTrigger("goCh2");
        currentSpeed = chaseSpeed;
        dsnvAgent.speed = currentSpeed;
        dsnvAgent.SetDestination(pTransform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Door"))
        {
            // StartCoroutine(OpenDalsu());
        }
    }

    IEnumerator OpenDalsu()
    {
        // 멈춘다.
        yield return new WaitForSeconds(1);
        // 문을 연다.
        // 다시 이동한다.

    }

    void Attack()
    {


        if (dist <= attackDist && canAt == true)
        {
            currentSpeed = 0;
            dsnvAgent.speed = currentSpeed;
            dsnvAgent.isStopped = true;
            canAt = false;
            dsanim.SetTrigger("goAt22");
            canAt = true;
            if (dist <= attackDist && canAt == true)
            {
                currentSpeed = 0;
                dsnvAgent.speed = currentSpeed;
                dsnvAgent.isStopped = true;
                canAt = false;
                dsanim.SetTrigger("goAt2");
                canAt = true;
                if (dist <= attackDist && canAt == true)
                {
                    currentSpeed = 0;
                    dsnvAgent.speed = currentSpeed;
                    dsnvAgent.isStopped = true;
                    canAt = false;
                    dsanim.SetTrigger("goAt3");
                    canAt = true;
                }
            }
            currentSpeed = chaseSpeed;
            dsnvAgent.speed = currentSpeed;
            dsnvAgent.isStopped = false;
        }
        else if (dist <= chaseDist)
        {
            currentSpeed = chaseSpeed;
            dsnvAgent.speed = currentSpeed;
            dsnvAgent.isStopped = false;
            curState = CurrentState.chase;
            dsanim.SetTrigger("goChase");
        }


    }

    void DSFootStep()
    {
        AudioSource.PlayClipAtPoint(dsStep, dsnvAgent.transform.position);
        AudioSource.PlayClipAtPoint(dsKey, dsnvAgent.transform.position);
        
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
        
    }





}
