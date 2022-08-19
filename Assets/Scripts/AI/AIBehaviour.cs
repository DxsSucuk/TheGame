using Photon.Pun;

using System.Collections;

using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts
{
    public class AIBehaviour : MonoBehaviourPunCallbacks
    {

        public NavMeshAgent agent;

        public Animator animator;

        public Transform targetPlayer;

        public LayerMask whatIsPlayer;

        //Patroling
        public Vector3 walkPoint;
        bool walkPointSet;
        public float walkPointRange;

        //States
        public float sightRange;
        public bool playerInSightRange;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            StartCoroutine(UpdateAIPath());
        }

        public void OnPhotonSerializeView(PhotonStream photonStream, PhotonMessageInfo info)
        {
            if (photonStream.IsWriting)
            {
                photonStream.SendNext("AI");
                photonStream.SendNext(animator.GetBool("isWalking"));
                photonStream.SendNext(animator.GetBool("isSprinting"));
                photonStream.SendNext(animator.GetBool("isJumping"));
                photonStream.SendNext(animator.GetBool("isFalling"));
                photonStream.SendNext(animator.GetBool("isCrouching"));
            }
            else if (photonStream.IsReading)
            {
                string typ = (string)photonStream.ReceiveNext();

                if (typ.Equals("AI"))
                {
                    animator.SetBool("isWalking", (bool)photonStream.ReceiveNext());
                    animator.SetBool("isSprinting", (bool)photonStream.ReceiveNext());
                    animator.SetBool("isJumping", (bool)photonStream.ReceiveNext());
                    animator.SetBool("isFalling", (bool)photonStream.ReceiveNext());
                    animator.SetBool("isCrouching", (bool)photonStream.ReceiveNext());
                }
            }
        }

        private void Update()
        {
            //Check for sight and attack range
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);

            if (!playerInSightRange) Patroling();
            if (playerInSightRange) ChasePlayer();
        }

        Vector3 lastpos;
        IEnumerator UpdateAIPath()
        {
            while (gameObject.activeSelf)
            {
                yield return new WaitForSeconds(5f);
                if (walkPointSet && !playerInSightRange)
                {
                    if (lastpos == walkPoint)
                    {
                        SearchWalkPoint();
                    }
                    else
                    {
                        lastpos = walkPoint;
                    }
                }
                else if (!playerInSightRange)
                {
                    SearchWalkPoint();
                }
            }
        }

        GameObject GetClosestEnemy(FirstPersonController[] enemies)
        {
            GameObject bestTarget = null;
            float closestDistanceSqr = Mathf.Infinity;
            Vector3 currentPosition = transform.position;
            foreach (FirstPersonController potentialTarget in enemies)
            {
                Vector3 directionToTarget = potentialTarget.gameObject.transform.position - currentPosition;
                float dSqrToTarget = directionToTarget.sqrMagnitude;
                if (dSqrToTarget < closestDistanceSqr)
                {
                    closestDistanceSqr = dSqrToTarget;
                    bestTarget = potentialTarget.gameObject;
                }
            }

            return bestTarget;
        }

        private void Patroling()
        {
            if (walkPointSet)
            {
                animator.SetBool("isWalking", true);

                Vector3 distanceToWalkPoint = transform.position - walkPoint;

                //Walkpoint reached
                if (distanceToWalkPoint.magnitude < 10f)
                    walkPointSet = false;
            }
            else
            {
                animator.SetBool("isWalking", false);
                SearchWalkPoint();
            }
        }

        private void SearchWalkPoint()
        {
            //Calculate random point in range
            float randomZ = Random.Range(-walkPointRange, walkPointRange);
            float randomX = Random.Range(-walkPointRange, walkPointRange);

            walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

            walkPointSet = true;

            if (agent.destination != walkPoint)
                agent.SetDestination(walkPoint);
        }

        private void ChasePlayer()
        {
            walkPointSet = false;
            if (targetPlayer == null)
            {
                targetPlayer = GetClosestEnemy(GameObject.FindObjectsOfType<FirstPersonController>()).transform;
            }
            else
            {
                Vector3 directionToTarget = targetPlayer.position - transform.position;
                float dSqrToTarget = directionToTarget.sqrMagnitude;
                if (dSqrToTarget >= sightRange)
                {
                    targetPlayer = GetClosestEnemy(GameObject.FindObjectsOfType<FirstPersonController>()).transform;
                }
            }

            if (targetPlayer != null)
            {
                animator.SetBool("isWalking", false);
                animator.SetBool("isSprinting", true);
                Vector3 positonOfUser = targetPlayer.position;
                agent.SetDestination(positonOfUser);
            }
            else
            {
                animator.SetBool("isSprinting", false);
                animator.SetBool("isWalking", false);
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, sightRange);
            if (walkPoint != null)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(walkPoint, 10);
                Gizmos.DrawLine(transform.position, walkPoint);
            }
        }
    }
}