using System;
using UnityEngine;
using UnityEngine.AI;

namespace Game.General.Navigation
{
    public class NavMeshFollower : MonoBehaviour
    {
        public Action<NavMeshFollower> OnArrive;
        public NavMeshAgent Agent;

        [SerializeField]
        private Transform target;

        public Transform Target
        {
            get { return target; }
            set
            {

                target = value;
                if (Agent != null && target != null && gameObject.activeInHierarchy)
                {
                    Agent.enabled = true;
                    Agent.destination = target.position;
                    Agent.updateRotation = true;
                    Agent.enabled = enabled;
                }
                else
                {
                    Agent.enabled = false;
                }
            }
        }

        void Update()
        {
            if (Agent.enabled && Agent.remainingDistance <= Agent.stoppingDistance)
            {
                Agent.enabled = false;
                if (OnArrive != null)
                {
                    OnArrive.Invoke(this);
                }
            }
        }
    }
}