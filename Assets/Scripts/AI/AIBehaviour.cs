using Photon.Pun;

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
        public LayerMask whatIsGround;

        public bool onGround;

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
            agent.updateRotation = true;
            agent.updateUpAxis = true;
        }

        public void OnPhotonSerializeView(PhotonStream photonStream, PhotonMessageInfo info)
        {
            if (photonStream.IsWriting)
            {
                photonStream.SendNext(false);
                photonStream.SendNext(animator.GetBool("isWalking"));
                photonStream.SendNext(animator.GetBool("isSprinting"));
                photonStream.SendNext(animator.GetBool("isJumping"));
                photonStream.SendNext(animator.GetBool("isFalling"));
                photonStream.SendNext(animator.GetBool("isCrouching"));
            }
            else if (photonStream.IsReading)
            {
                bool lightObject = (bool)photonStream.ReceiveNext();
                animator.SetBool("isWalking", (bool)photonStream.ReceiveNext());
                animator.SetBool("isSprinting", (bool)photonStream.ReceiveNext());
                animator.SetBool("isJumping", (bool)photonStream.ReceiveNext());
                animator.SetBool("isFalling", (bool)photonStream.ReceiveNext());
                animator.SetBool("isCrouching", (bool)photonStream.ReceiveNext());
            }
        }

        void checkGround()
        {
            Vector3 origin = new Vector3(transform.position.x, transform.position.y - (transform.localScale.y * .5f),
    transform.position.z);
            Vector3 direction = transform.TransformDirection(Vector3.down);
            float distance = .75f;

            if (Physics.Raycast(origin, direction, out RaycastHit hit, distance))
            {
                Debug.DrawRay(origin, direction * distance, Color.red);
                onGround = true;
                animator.SetBool("isJumping", false);
                animator.SetBool("isFalling", false);
            }
            else
            {
                animator.SetBool("isFalling", true);
                onGround = false;
            }
        }

        private void Update()
        {
            checkGround();
            //Check for sight and attack range
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);

            if (!playerInSightRange) Patroling();
            if (playerInSightRange) ChasePlayer();
        }

        private void FixedUpdate()
        {
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
            if (!agent.pathPending)
            {
                if (!walkPointSet) SearchWalkPoint();

                if (walkPointSet)
                {
                    animator.SetBool("isWalking", true);
                    agent.SetDestination(walkPoint);
                    transform.LookAt(walkPoint);
                }
                else
                {
                    animator.SetBool("isWalking", false);
                }

                Vector3 distanceToWalkPoint = transform.position - walkPoint;

                //Walkpoint reached
                if (distanceToWalkPoint.magnitude < 10f)
                    walkPointSet = false;
            }
        }

        private void SearchWalkPoint()
        {
            //Calculate random point in range
            float randomZ = Random.Range(-walkPointRange, walkPointRange);
            float randomX = Random.Range(-walkPointRange, walkPointRange);

            walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

            if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
                walkPointSet = true;
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
                transform.LookAt(new Vector3(positonOfUser.x, positonOfUser.z));
            } else
            {
                animator.SetBool("isSprinting", false);
                animator.SetBool("isWalking", false);
            }
        }

        private void DestroyEnemy()
        {
            Destroy(gameObject);
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