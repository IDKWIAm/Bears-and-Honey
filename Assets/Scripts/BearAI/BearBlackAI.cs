using UnityEngine;
using UnityEngine.AI;

namespace BearAI
{
    public class BearBlackAI : MonoBehaviour
    {
        [SerializeField] private Transform fruitPoint;
        [SerializeField] private Transform cookingPoint;

        private Transform _currentTarget;
        private NavMeshAgent _navMeshAgent;
        private Animator _animator;

        private float _remainingDistance;

        private bool isStanding = false;
        private bool isRunning = false;
        private bool switched;

        void Start()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();
            _currentTarget = fruitPoint;
            MoveToCurrentTarget();
        }
        void Update()
        {
            CanculateRemainingDistance();
            if (_remainingDistance <= _navMeshAgent.stoppingDistance)
            {
                if (!switched)
                {
                    switched = true;
                    SwitchState();
                    TriggerAnimation();
                }
            }
            else
            {
                switched = false;
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

        private void CanculateRemainingDistance()
        {
            _remainingDistance = Vector3.Distance(transform.position, _currentTarget.position);
        }
    }
}



