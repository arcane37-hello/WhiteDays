using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DalsuMove : MonoBehaviour
{

    public enum CurrentState { idle, patrol, chase, attack, chaseStart };
    CurrentState curState;

    public float dsdmg = 1;

    Animator dsanim;

    public AudioClip dsStep;
    public AudioClip dsKey;
    public AudioClip dsWs;
    public AudioClip dsAt;

    CharacterController cc;
    Vector3 patrolCenter;
    Vector3 patrolNext;

    private List<AudioSource> dsAs = new List<AudioSource>();
    private Transform dsTransform;
    private Transform pTransform;
    private NavMeshAgent dsnvAgent;
    private float dist;

    //추적 사정 거리
    public float chaseDist = 20.0f;
    //공격 사정 거리
    public float attackDist = 3.0f;


    void Start()
    {
        AddAudioSource(dsStep);
        AddAudioSource(dsKey);
        AddAudioSource(dsWs);
        AddAudioSource(dsAt);

        patrolCenter = transform.position;
        patrolNext = patrolCenter;

        dsTransform = GetComponent<Transform>();
        pTransform = GameObject.FindGameObjectWithTag("Player").transform;
        cc = GetComponent<CharacterController>();
        dsnvAgent = GetComponent<NavMeshAgent>();
        dsanim = GetComponent<Animator>();
        curState = CurrentState.patrol;
        
    }



    void Update()
    {
        pTransform = hm.transform;
        if (dsnvAgent.velocity.sqrMagnitude > Mathf.Epsilon)
        {
            dsTransform.rotation = Quaternion.LookRotation(dsnvAgent.velocity.normalized);
        }
        StartCoroutine(CheckState());
        StartCoroutine(CheckStateForAction());
    }

    IEnumerator CheckState()
    {
        dist = Vector3.Distance(dsTransform.position, pTransform.position);
        yield return new WaitForSeconds(0.25f);
        if (dist <= attackDist)  //isHiding이 아니라면
        {
            curState = CurrentState.attack;
        }
        else if (dist <= chaseDist)  //isHiding이 아니라면
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, (pTransform.position - transform.position).normalized, out hit, chaseDist))
                if (hit.transform == pTransform)
                {
                    if (curState == CurrentState.chase)
                    {
                        curState = CurrentState.chase;
                    }
                    else if (curState == CurrentState.chaseStart)
                    {
                        curState = CurrentState.chase;
                    }
                    else if (curState == CurrentState.patrol)
                    {
                        curState = CurrentState.chaseStart;
                    }
                }

        }
        else
        {
            curState = CurrentState.patrol;
        }
    }

    IEnumerator CheckStateForAction()
    {
        switch(curState)
        {
            case CurrentState.attack:
                StartCoroutine(Attack());
                break;
            case CurrentState.patrol:
                Patrol();
                break;
            case CurrentState.chaseStart:
                ChaseStart();
                break;
            case CurrentState.chase:
                Chase();
                break;
            
        }
        yield return null;
    }

    void Patrol()
    {
        Vector3 dir = pTransform.position - dsTransform.position;
        dsanim.SetTrigger("Patrol");
        dsnvAgent.speed = 0.8f;
        patrolNext = patrolCenter + new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
        dsnvAgent.SetDestination(patrolNext);
    }

    void ChaseStart()
    {
        AudioSource.PlayClipAtPoint(dsWs, dsnvAgent.transform.position);
        Chase();
    }   
    
    void Chase()
    {
        currentSpeed = chaseSpeed;
        dsnvAgent.speed = currentSpeed;
        dsnvAgent.SetDestination(pTransform.position);
        dsanim.SetTrigger("goChase");
        
    }

    void Attack()
    {
        // 플레이어를 향해 바라보도록 설정
        if (dist <= attackDist && canAt == true)
        {
            dsanim.SetTrigger("Attack");
            if (dist <= attackDist)
            {
                dsanim.SetTrigger("At");
                if (dist <= attackDist)
                {
                    dsanim.SetTrigger("At2");
                }
            }
        }
        else if (dist <= chaseDist)
        {
            curState = CurrentState.chase;

        }


    }

    void AddAudioSource(AudioClip clip)
    {
        AudioSource newSource = gameObject.AddComponent<AudioSource>();
        newSource.clip = clip;
        dsAs.Add(newSource);
    }

    void DSFootStep()
    {
        AudioSource.PlayClipAtPoint(dsStep, dsnvAgent.transform.position);
        AudioSource.PlayClipAtPoint(dsKey, dsnvAgent.transform.position);
        while(curState == CurrentState.chase)
        {
            DSWS();
            yield return new WaitForSeconds(3);
        }
    }

    void DSWS()
    {
        AudioSource.PlayClipAtPoint(dsWs, dsnvAgent.transform.position);
    }

    void DSAT()
    {
        AudioSource.PlayClipAtPoint(dsAt, dsnvAgent.transform.position);
    }


=======
    void DSATYES()
    {
        AudioSource.PlayClipAtPoint(dsAtss, pTransform.position);
        playerHealth.TakeDamage((int)dsdmg);
    }

>>>>>>> Stashed changes
}
