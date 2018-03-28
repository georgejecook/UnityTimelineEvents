using System;
using Game.General.Navigation;
using UnityEngine;
using UnityEngine.AI;

namespace Tantawowa.Demo.DemoScripts
{
    public enum RobotState
    {
        Sleeping,
        GoToWork,
        GoHome,
    }

    public class Robot : MonoBehaviour
    {
        [SerializeField]
        private int points;

        [SerializeField]
        private RobotState currentState;

        public TextMesh Message;

        public NavMeshAgent Agent;
        public NavMeshFollower NavMeshFollower;

        public Transform Work;
        public Transform Home;

        public int Points
        {
            get { return points; }
            set
            {
                points = value;
                Message.text = points > 0 ? points.ToString() : "";
            }
        }

        public void AddScore(int points)
        {
            Points += points;
        }

        public void ResetScore()
        {
            Points = 0;
        }

        public void SetState(RobotState state)
        {
            currentState = state;
            switch (state)
            {
                case RobotState.Sleeping:
                    Agent.speed = 0f;
                    NavMeshFollower.Target = null;
                    break;
                case RobotState.GoToWork:
                    NavMeshFollower.Target = Work;
                    Agent.speed = 3;
                    break;
                case RobotState.GoHome:
                    NavMeshFollower.Target = Home;
                    Agent.speed = 6f;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("state", state, null);
            }
        }

        private void Update()
        {
            Vector3 v = Camera.main.transform.position - transform.position;
            v.x = v.z = 0.0f;
            Message.transform.LookAt(Camera.main.transform.position - v);
            Message.transform.Rotate(0, 180, 0);
        }
    }
}