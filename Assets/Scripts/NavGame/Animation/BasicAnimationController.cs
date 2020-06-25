using UnityEngine;
using UnityEngine.AI;

namespace NavGame.Animation
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class BasicAnimationController : MonoBehaviour
    {
        public float smoothness = 0.1f;

        protected NavMeshAgent agent;
        protected Animator animator;

        protected virtual void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponentInChildren<Animator>();
        }

        protected virtual void Update()
        {
            float speedPercent = agent.velocity.magnitude / agent.speed;
            animator.SetFloat("speedPercent", speedPercent, smoothness, Time.deltaTime);
        }
    }
}
