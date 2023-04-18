using System;
using System.Collections;
using System.Collections.Generic;
using Shapes2D;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class MonsterMovement : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float attackRange;
    [SerializeField] [Range(0.1f, 10.0f)] private float walkSpeed;
    [SerializeField] [Range(0.1f, 10.0f)] private float runSpeed;

    [Header("Notify position")]
    [SerializeField] private Transform notifyPos;
    [SerializeField] private bool notify;
    
    [Space]

    [Header("Information")] 
    
    [SerializeField] private float realSpeed;
    [SerializeField] private Vector3 agentDestination;
    [SerializeField] private float distanceLeft;
    [SerializeField] private float distanceLevelToPlayer;
    [SerializeField] private Transform hrDestination;
    [SerializeField] private bool running;
    [SerializeField] private bool attacking;
    [SerializeField] private bool isNotified;

    private NavMeshAgent agent;
    private MonsterAnimationController ac;
    private GameObject player;
    private Vector3 lastFramePos;
    private Coroutine attack;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        ac = GetComponent<MonsterAnimationController>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (notify)
        {
            notify = false;
            NotifyLocation(notifyPos.position);
        }

        agentDestination = agent.destination;
        distanceLeft = Radar.CalculatePathDistance(transform.position, agentDestination);
        distanceLevelToPlayer = player.GetComponent<Radar>().GetDistanceLevel();
        
        /* Speed handling */
        
        realSpeed = CalculateRealSpeed();
        
        if (realSpeed > 0 && !running)
        { //Monster is walking
            //Set speed
            agent.speed = walkSpeed;
            
            StartCoroutine(ac.SetAnimatorState(MonsterAnimationController.MonsterAnimationStates.walking));
        }

        if (realSpeed > 0 && running)
        { //Monster is running
            //Set speed
            agent.speed = runSpeed;
            
            StartCoroutine(ac.SetAnimatorState(MonsterAnimationController.MonsterAnimationStates.running));
        }

        if (realSpeed == 0)
        { //Monster is standing still
            StartCoroutine(ac.SetAnimatorState(MonsterAnimationController.MonsterAnimationStates.idle));
        }
        
        
        /* Destination handling */

        //If the monster is NOT notified of a location
        if (!isNotified)
        {
            //Set to walking
            running = false;
            
            //Set destination to hr based destination
            StartCoroutine(SetDestination(hrDestination.transform.position, 0f));
        }

        //If the distance between monster and player is level 4, AND the heartrate is 1.25 or more times higher than normal:
        if (distanceLevelToPlayer == 4 && GameManager.GameInfo.GetHeartRate() >= GameManager.GameInfo.GetNHr() * 1.25)
        {
            GameManager.Instance.LoadScene("Lose");
        }

        //If the monster is notified of a location
        if (isNotified)
        {
            //Set to running
            running = true;

            //Set destination to notifying position
            StartCoroutine(SetDestination(notifyPos.position, 0f));

            //If the monster arrived at notifying position
            if (distanceLeft <= 1f)
            {
                
                
                //Set isNotified to false after a delay
                StartCoroutine(DeNotify(2f));
            }
        }
        
    }

    private IEnumerator DeNotify(float delay)
    {
        yield return new WaitForSeconds(delay);
        isNotified = false;
    }

    private IEnumerator SetDestination(Vector3 pos, float delay)
    {
        if (pos != agent.destination)
        {
            //Wait delay
            yield return new WaitForSeconds(delay);

            //Set the new destination
            agent.SetDestination(pos);
        }

        yield return null;
    }

    private IEnumerator Attack(float damage, float duration)
    {
        attacking = true;
        
        Debug.Log("Attack!");
        //player.GetComponent<PlayerInformation>().DoDamage(damage);

        yield return new WaitForSeconds(duration);

        attacking = false;
    }

    private bool PlayerInAttackRange(float attackRange)
    {
        //If the path-based distance between monster and player is within the attackRange
        if (Radar.CalculatePathDistance(gameObject.transform.position, player.transform.position) <= attackRange)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private float CalculateRealSpeed()
    {
        float speed = (transform.position - lastFramePos).magnitude / Time.deltaTime;
        lastFramePos = transform.position;

        return speed;
    }

    public void NotifyLocation(Vector3 location)
    {
        isNotified = true;
        SetDestination(location, 0f);
    }
}
