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
    //private 플레이어스크립트이름 playerSc;
    private float dist;
    private PlayerHealth playerHealth;

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
        playerHealth = pTransform.GetComponent<PlayerHealth>();
        curState = CurrentState.patrol;
        
    }



    void Update()
    {
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
        //cc.Move(dir.normalized * dsnvAgent.speed * Time.deltaTime);
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
        Vector3 dir = pTransform.position - dsTransform.position;
        dsnvAgent.speed = 1.4f;
        //cc.Move(dir.normalized * dsnvAgent.speed * Time.deltaTime);
        dsnvAgent.SetDestination(pTransform.position);
        dsanim.SetTrigger("Chase");
    }

    IEnumerator Attack()
    {
        // 플레이어를 향해 바라보도록 설정
        cc.Move(Vector3.zero);
        //dsTransform.LookAt(pTransform.position);

        if (dist <= attackDist)
        {
            dsanim.SetTrigger("Attack");
            DsDMG();
            yield return new WaitForSeconds(2.1f); // 공격 애니메이션 시간만큼 대기
            if (dist <= attackDist)
            {
                dsanim.SetTrigger("At");
                DsDMG();
                yield return new WaitForSeconds(2.1f); // 공격 애니메이션 시간만큼 대기
                if (dist <= attackDist)
                {
                    dsanim.SetTrigger("At2");
                    DsDMG();
                    yield return new WaitForSeconds(2.1f); // 공격 애니메이션 시간만큼 대기
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
        // dsAs[0].Play();
        AudioSource.PlayClipAtPoint(dsStep, dsnvAgent.transform.position);
        AudioSource.PlayClipAtPoint(dsKey, dsnvAgent.transform.position);
    }

    void DSAT()
    {
        AudioSource.PlayClipAtPoint(dsAt, dsnvAgent.transform.position);
    }

    //void LookAtP()
    //{
    //    // this.transform.LootAt(pTransform);
    //    Vector3 dsDir = dsnvAgent.desiredVelocity;
    //    dsDir.Set(dsDir.x, 0f, dsDir.z);
    //    Quaternion targetAngle = Quaternion.LookRotation(dsDir);
    //    dsnvAgent.transform.rotation = Quaternion.Slerp(dsnvAgent.transform.rotation, targetAngle, 120);
    //}

    void DsDMG()
    {
        // 플레이어에게 dsdmg 만큼 대미지를 입히는 함수를 실행
        playerHealth.TakeDamage((int)dsdmg);
    }
}
