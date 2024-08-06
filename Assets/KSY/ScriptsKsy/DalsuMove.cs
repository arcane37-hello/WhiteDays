using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public AudioClip dsAtss;

    public float currentSpeed;
    public float patrolSpeed = 0.8f;
    public float chaseSpeed = 1.8f;

    CharacterController cc;
<<<<<<< Updated upstream
    Vector3 patrolCenter;
    Vector3 patrolNext;
=======
    public GameObject hm;
>>>>>>> Stashed changes

    private List<AudioSource> dsAs = new List<AudioSource>();
    private Transform dsTransform;
    private Transform pTransform;
    private NavMeshAgent dsnvAgent;
    //private 플레이어스크립트이름 playerSc;
    private float dist;

    //추적 사정 거리
    public float chaseDist = 25.0f;
    //공격 사정 거리
    private float attackDist = 1.2f;


    void Start()
    {
        AddAudioSource(dsStep);
        AddAudioSource(dsKey);
        AddAudioSource(dsWs);
        AddAudioSource(dsAt);
        AddAudioSource(dsAtss);

        dsTransform = GetComponent<Transform>();
        pTransform = GameObject.FindGameObjectWithTag("Player").transform;
        cc = GetComponent<CharacterController>();
        dsnvAgent = GetComponent<NavMeshAgent>();
        dsnvAgent.updateRotation = false;
        dsanim = GetComponent<Animator>();
<<<<<<< Updated upstream
        curState = CurrentState.patrol;
        
=======
        hm = GameObject.FindWithTag("Player");
        playerHealth = hm.GetComponent<PlayerHealth>();
        curState = CurrentState.patrol;
        dsnvAgent.stoppingDistance = attackDist;


        bool canAt = true;

>>>>>>> Stashed changes
    }



    void Update()
    {
<<<<<<< Updated upstream
=======
        pTransform = hm.transform;
        // 플레이어를 향해 바라보도록 설정
        //Vector3 pSis = new Vector3(pTransform.position.x, pTransform.position.y, pTransform.position.z);
        //dsTransform.LookAt(pSis);
        if (dsnvAgent.velocity.sqrMagnitude > Mathf.Epsilon)
        {
            dsTransform.rotation = Quaternion.LookRotation(dsnvAgent.velocity.normalized);
        }
>>>>>>> Stashed changes
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
            Vector3 raydir = (pTransform.position - dsTransform.position).normalized;
            if (Physics.Raycast(dsTransform.position, raydir, out hit, chaseDist))
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
                Attack();
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
<<<<<<< Updated upstream
        Vector3 dir = pTransform.position - dsTransform.position;
        dsanim.SetTrigger("Patrol");
        dsnvAgent.speed = 0.8f;
        //cc.Move(dir.normalized * dsnvAgent.speed * Time.deltaTime);
        patrolNext = patrolCenter + new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
        dsnvAgent.SetDestination(patrolNext);
=======
        dsanim.SetTrigger("goPatrol");
        currentSpeed = patrolSpeed;
        dsnvAgent.speed = currentSpeed;
        dsnvAgent.SetDestination(pTransform.position);
>>>>>>> Stashed changes
    }

    void ChaseStart()
    {
        AudioSource.PlayClipAtPoint(dsWs, dsnvAgent.transform.position);
        Chase();
    }   
    
    void Chase()
    {
<<<<<<< Updated upstream
        Vector3 dir = pTransform.position - dsTransform.position;
        dsnvAgent.speed = 1.4f;
        //cc.Move(dir.normalized * dsnvAgent.speed * Time.deltaTime);
        dsnvAgent.SetDestination(pTransform.position);
        dsanim.SetTrigger("Chase");
=======
        currentSpeed = chaseSpeed;
        dsnvAgent.speed = currentSpeed;
        dsnvAgent.SetDestination(pTransform.position);
        dsanim.SetTrigger("goChase");
        
>>>>>>> Stashed changes
    }

    void Attack()
    {
        

        if (dist <= attackDist)
        {
<<<<<<< Updated upstream
            dsanim.SetTrigger("Attack");
            // DsDMG();
            yield return new WaitForSeconds(2.1f); // 공격 애니메이션 시간만큼 대기
            if (dist <= attackDist)
            {
                dsanim.SetTrigger("At");
                // DsDMG();
                yield return new WaitForSeconds(2.1f); // 공격 애니메이션 시간만큼 대기
                if (dist <= attackDist)
                {
                    dsanim.SetTrigger("At2");
                    // DsDMG();
                    yield return new WaitForSeconds(2.1f); // 공격 애니메이션 시간만큼 대기
=======
            dsnvAgent.isStopped = true;
            canAt = false;
            dsanim.SetTrigger("goAt");
            canAt = true;
            if (dist <= attackDist && canAt == true)
            {
                dsnvAgent.isStopped = true;
                canAt = false;
                dsanim.SetTrigger("goAt2");
                canAt = true;
                if (dist <= attackDist && canAt == true)
                {
                    dsnvAgent.isStopped = true;
                    canAt = false;
                    dsanim.SetTrigger("goAt3");
                    canAt = true;
>>>>>>> Stashed changes
                }
            }
            dsnvAgent.isStopped = false;
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

    IEnumerator DSFootStep()
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
    void DSATYES()
    {
        AudioSource.PlayClipAtPoint(dsAtss, pTransform.position);
        playerHealth.TakeDamage((int)dsdmg);
    }

    


    //void LookAtP()
    //{
    //    // this.transform.LootAt(pTransform);
    //    Vector3 dsDir = dsnvAgent.desiredVelocity;
    //    dsDir.Set(dsDir.x, 0f, dsDir.z);
    //    Quaternion targetAngle = Quaternion.LookRotation(dsDir);
    //    dsnvAgent.transform.rotation = Quaternion.Slerp(dsnvAgent.transform.rotation, targetAngle, 120);
    //}

<<<<<<< Updated upstream
    void DsDMG()
    {
        // 플레이어에게 dsdmg 만큼 대미지를 입히는 함수를 실행
    }
=======

 
>>>>>>> Stashed changes
}
