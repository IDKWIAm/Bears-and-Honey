using UnityEngine;
using UnityEngine.AI;

namespace BearAI
{
    public class BearBlackAI : MonoBehaviour
    {
        [SerializeField] private bool isStatic;
        [SerializeField] private Transform fruitPoint;
        [SerializeField] private Transform cookingPoint;

        [SerializeField] private bool findCookingPoint;

        private Transform _currentTarget;
        private NavMeshAgent _navMeshAgent;
        private Animator _animator;

        private InventoryManager _inventoryManager;

        private float _remainingDistance;

        private bool isStanding = false;
        private bool isRunning = true;
        private bool switched;

        private int dish;

        void Start()
        {
            _animator = GetComponent<Animator>();
            _inventoryManager = GameObject.FindObjectOfType<InventoryManager>();

            if (isStatic) return;

            _navMeshAgent = GetComponent<NavMeshAgent>();
            if (findCookingPoint)
                cookingPoint = FindClosestCookingPoint();
            _currentTarget = fruitPoint;
            MoveToCurrentTarget();
            _animator.SetBool("isRunning", isRunning);
        }

        void Update()
        {
            if (isStatic) return;

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
            else switched = false;            
        }

        private Transform FindClosestCookingPoint()
        {
            GameObject[] cookingPoints = GameObject.FindGameObjectsWithTag("Cooking Point");
            float minDis = Mathf.Infinity;
            GameObject closestCookingPoint = null;
            foreach (GameObject cookingPoint in cookingPoints)
            {
                float dis = Vector3.Distance(transform.position, cookingPoint.transform.position);
                if (dis < minDis)
                {
                    minDis = dis;
                    closestCookingPoint = cookingPoint;
                }
            }
            if (closestCookingPoint == null) return null;
            return closestCookingPoint.transform;
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

        public void TriggerMakeDish(int chance = 100)
        {
            if (Random.Range(1, 100) <= chance)
            {
                _inventoryManager?.MakeDish(dish);
            }
        }

        public void SwitchState()
        {
            isStanding = !isStanding;
            isRunning = !isRunning;
            _animator.SetBool("isRunning", isRunning);
        }

        private void TriggerAnimation()
        {
            if (_currentTarget == fruitPoint)
                _animator.SetTrigger("Pick");

            if (_currentTarget == cookingPoint)
                _animator.SetTrigger("Give");
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



