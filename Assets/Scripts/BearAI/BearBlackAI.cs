using UnityEngine;
using UnityEngine.AI;

namespace BearAI
{
    public class BearBlackAI : MonoBehaviour
    {
        public Transform fruitPoint;
        public Transform cookingPoint;
        private Transform _currentTarget;
        private NavMeshAgent _navMeshAgent;
        private Animator _animator;
        private bool isStanding = false;
        private bool isRunning = false;

        void Start()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();
            _currentTarget = fruitPoint;
            MoveToCurrentTarget();
        }
        void Update()
        {
            if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
            {
                SwitchTarget();
                TriggerAnimation();
                SwitchState();
                MoveToCurrentTarget();
            }
            else
            {
                isRunning = true;
                _animator.SetBool("isRunning", isRunning);
            }
        }

        private void MoveToCurrentTarget()
        {
            if (!isStanding) _navMeshAgent.SetDestination(_currentTarget.position);
        }
        private void SwitchTarget()
        {
            if (_currentTarget == fruitPoint) _currentTarget = cookingPoint;
            else _currentTarget = fruitPoint;
        }
        public void SwitchState()
        {
            isStanding = !isStanding;
            _animator.SetBool("isRunning", !isStanding);
        }
        private void TriggerAnimation()
        {
            if (_currentTarget == fruitPoint)
            {
                _animator.SetTrigger("Pick");
                isRunning = false;
            }
            if (_currentTarget == cookingPoint)
            {
                _animator.SetTrigger("Give");
                isRunning = false;
            }
        }
    }
}



