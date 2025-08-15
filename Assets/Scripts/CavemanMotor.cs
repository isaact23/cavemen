using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CavemanMotor : MonoBehaviour
{
    // Reference to other game objects
    public GameObject player;
    public AudioManager audioManager;
    public DeathManager deathManager;
    
    // Time to stop in between patrols
    public float minWaitTime;
    public float maxWaitTime;

    // Distance between caveman and player to trigger certain events
    public float killRadius;
    public float chaseRadius;
    public float forgetRadius;
    
    // Caveman speeds
    public float walkSpeed;
    public float runSpeed;
    public float killTime = 1.2f; // Time to kill player
    public float lookSpeed = 5f;

    // List of patrol points
    public GameObject[] patrolPoints;
    
    private Status status = Status.Waiting;
    private int currentPatrolPoint = 0;
    private float timeLeftWaiting = 0f;
    private float lastPathCalculation;
    private float timeKilling = 0f;
    private NavMeshAgent navMeshAgent;
    private NavMeshPath pathToPlayer;
    private Animator animator;

    enum Status
    {
        Waiting,
        Walking,
        Chasing,
        Killing
    }

    // Start is called before the first frame update
    void Start()
    {
        lastPathCalculation = Time.time;
        navMeshAgent = GetComponent<NavMeshAgent>();
        pathToPlayer = new NavMeshPath();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (status == Status.Waiting) { // If waiting,
            Wait();
        }
        else if (status == Status.Walking) { // If walking,
            Walk();
        }
        else if (status == Status.Chasing) { // If chasing,
            Chase();
        }
        else if (status == Status.Killing) {
            Kill();
        }
    }

    // Return true if the caveman is currently chasing a player.
    public bool IsChasing()
    {
        return status == Status.Chasing;
    }

    // Select a time to wait and start waiting.
    private void StartWaiting()
    {
        status = Status.Waiting;
        animator.SetBool("Walking", false);
        animator.SetBool("Running", false);
        navMeshAgent.ResetPath();
        timeLeftWaiting = Random.Range(minWaitTime, maxWaitTime);
    }

    // Select the next patrol point and start walking.
    private void StartWalking()
    {
        status = Status.Walking;
        animator.SetBool("Walking", true);
        animator.SetBool("Running", false);
        navMeshAgent.speed = walkSpeed;
        navMeshAgent.SetDestination(patrolPoints[currentPatrolPoint].transform.position);
        currentPatrolPoint++;
        if (currentPatrolPoint >= patrolPoints.Length) {
            currentPatrolPoint = 0;
        }
    }
    
    // Start chasing the player.
    private void StartChasing()
    {
        status = Status.Chasing;
        animator.SetBool("Walking", false);
        animator.SetBool("Running", true);
        navMeshAgent.speed = runSpeed;
    }
    
    // Start killing the player.
    private void StartKilling()
    {
        if (status != Status.Killing) {
            status = Status.Killing;
            animator.SetBool("Killing", true);
            navMeshAgent.ResetPath();
            navMeshAgent.velocity = Vector3.zero;
            player.GetComponent<PlayerForceLook>().StartLooking(this.gameObject);
            audioManager.PlayDeathSound();
        }
    }
    
    // See if chasing the player is an option. If so, start chasing.
    private void AttemptChase()
    {
        if (player && DistanceToPlayer() <= chaseRadius) {
            pathToPlayer = GetPathToPlayer();
            if (pathToPlayer != null) {
                navMeshAgent.SetPath(pathToPlayer);
                StartChasing();
            }
        }
    }

    // Idle.
    private void Wait()
    {
        timeLeftWaiting -= Time.deltaTime; // Continue to wait.
        if (timeLeftWaiting <= 0f) { // If done waiting,
            StartWalking();
        }
        else {
            AttemptChase();
        }
    }
    
    // Walk.
    private void Walk()
    {
        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance &&
            !navMeshAgent.hasPath && navMeshAgent.velocity.sqrMagnitude <= 0.1f) { // If destination reached,
            StartWaiting();
        }
        else {
            AttemptChase();
        }
    }
    
    // Chase.
    private void Chase()
    {
        if (DistanceToPlayer() > forgetRadius) {
            StartWaiting();
        }
        else if (DistanceToPlayer() < killRadius) {
            StartKilling();
        }
        else {
            NavMeshPath path = GetPathToPlayer();
            if (path == null) {
                StartWaiting(); // If we cannot reach player, give up.
            }
            else {
                navMeshAgent.SetPath(path);
            }
        }
    }
    
    // Kill (continuously called during killing).
    private void Kill()
    {
        // Look in the direction of the player
        Vector3 direction = player.transform.position - transform.position + new Vector3(0, 0, 0);
        Vector3 lookVector = Vector3.RotateTowards(transform.forward, direction, lookSpeed * Time.deltaTime, 0.0f);
        transform.rotation = Quaternion.LookRotation(lookVector);
        
        timeKilling += Time.deltaTime;
        if (timeKilling >= killTime) {
            deathManager.KillPlayer();
        }
    }

    // Get path to player. If not possible, return null.
    // If called too frequently, return the previous result.
    private NavMeshPath GetPathToPlayer()
    {
        if (Time.time - lastPathCalculation >= 0.1f) {
            lastPathCalculation = Time.time;
            pathToPlayer = new NavMeshPath();
            navMeshAgent.CalculatePath(player.transform.position, pathToPlayer);
        }

        if (pathToPlayer != null && pathToPlayer.status == NavMeshPathStatus.PathComplete) {
            return pathToPlayer; // Return the path calculated last time.
        }

        return null;
    }

    // Calculate the distance from this caveman to the player.
    private float DistanceToPlayer()
    {
        return Mathf.Abs((transform.position - player.transform.position).magnitude);
    }
}