using UnityEngine;
using UnityEngine.AI;

namespace BearAI
{
    public class BearBlackAI : MonoBehaviour
    {
        [SerializeField] private Transform fruitPoint;
        [SerializeField] private Transform cookingPoint;

        [SerializeField] private bool findCookingPoint;

        private Transform _currentTarget;
        private NavMeshAgent _navMeshAgent;
        private Animator _animator;

        private InventoryManager _inventoryManager;

        private float _remainingDistance;

        private bool isStanding = false;
        private bool isRunning = false;
        private bool switched;

        private int dish;

        void Start()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();
            if (findCookingPoint)
                cookingPoint = GameObject.FindGameObjectWithTag("Cooking Point").transform;
            _inventoryManager = GameObject.FindObjectOfType<InventoryManager>();
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
            SwitchState();

            if (_currentTarget == fruitPoint)
            {
                _currentTarget = cookingPoint;
            }
            else
            {
                _inventoryManager?.MakeDish(dish);
                _currentTarget = fruitPoint;
            }

            MoveToCurrentTarget();
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

        public void SetDish(int dishNum)
        {
            dish = dishNum;
        }
    }
}



