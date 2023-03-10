using Abstract;
using Data.ValueObject;
using Enums;
using Managers;
using Signals;
using UnityEngine;
using UnityEngine.AI;

namespace States.EnemyStates
{
    public class EnemyMoveBaseState: EnemyBaseState
    {
        #region Self Variables
        

        #region Private Variables

        private EnemyManager _manager;
        private NavMeshAgent _agent;
        private EnemyData _data;
        private EnemyTypes _types;

        #endregion

        #endregion

        public EnemyMoveBaseState(ref EnemyManager manager, ref NavMeshAgent agent, ref EnemyData data,
            ref EnemyTypes enemyTypes)
        {
            _manager = manager;
            _agent = agent;
            _data = data;
            _types = enemyTypes;

        }
        public override void EnterState()
        {
            _agent.speed = _data.EnemyTypeDatas[_types].MoveSpeed;
            _manager.SetTriggerAnim(EnemyAnimTypes.Walk);
            _agent.SetDestination(_manager.BasePoints.transform.position);
        }

        public override void UpdateState()
        {
            if (_manager.Health())
            {
                _manager.SwitchState(EnemyStatesTypes.Death);
            }
            
        }

        public override void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("BasePoint"))
            {
                CoreGameSignals.Instance.onFailed?.Invoke();
            }
            
        }

        public override void OnTriggerExit(Collider other)
        {
        }
    }
}