using System;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.Playables;

namespace Tantawowa.TimelineEvents
{
    [Serializable]
    public class TimelineEventBehaviour : PlayableBehaviour
    {
        /// <summary>
        /// Key for the current event handler - used to track changes 
        /// </summary>
        public string HandlerKey;

        /// <summary>
        /// Indicates that the method expects a single parameter
        /// </summary>
        public bool IsMethodWithParam;

        public bool InvokeEventsInEditMode;
        
        /// <summary>
        /// The object on which events are invoked
        /// </summary>
        public GameObject TargetObject;
        
        /// <summary>
        /// value of the argument to use - it's serialized to and from string
        /// </summary>
        public string ArgValue;

        private EventInvocationInfo invocationInfo;

        public override void OnBehaviourPlay(Playable playable, FrameData info)
        {
			// Only invoke if time has passed to avoid invoking
			// repeatedly after resume
			if ((info.frameId == 0) || (info.deltaTime > 0))
			{
				UpdateDelegates();
				if (invocationInfo != null)
				{
					invocationInfo.Invoke(IsMethodWithParam, ArgValue);
				}
			}
        }
        
        private void UpdateDelegates()
        {
            bool enableByMode = Application.isPlaying || InvokeEventsInEditMode;

            invocationInfo = GetInvocationInfo(enableByMode, HandlerKey, invocationInfo,
                IsMethodWithParam);
        }

        /// <summary>
        /// Given the method key and target, constructs event invocation info which can be used for later invoking
        /// the method on the target. Also updates the key to reduce the amount of instantiations.
        /// </summary>
        /// <param name="isEnabled"></param>
        /// <param name="methodKey"></param>
        /// <param name="currentInfo"></param>
        /// <param name="methodWitharg"></param>
        /// <returns></returns>
        private EventInvocationInfo GetInvocationInfo(bool isEnabled, string methodKey, EventInvocationInfo currentInfo,
            bool methodWitharg)
        {
            if (currentInfo != null && currentInfo.Key == methodKey &&
                !(string.IsNullOrEmpty(methodKey) || (methodKey.ToLower() == "none")))
            {
                return currentInfo;
            }

            Behaviour targetBehaviour = null;
            string methodName = null;
            GetBehaviourAndMethod(isEnabled, methodKey, ref targetBehaviour, ref methodName);

            if (targetBehaviour != null)
            {
                //get the method info
                var methodInfo = targetBehaviour
                    .GetType()
                    .GetMethods(BindingFlags.Public | BindingFlags.Instance)
					.FirstOrDefault(m => m.Name == methodName && m.ReturnType == typeof(void) &&
                                         m.GetParameters().Length == (methodWitharg ? 1 : 0));
                return new EventInvocationInfo(methodKey, targetBehaviour, methodInfo);
            }

            return null;
        }
        
        /// <summary>
        /// given the key and target, will return (by ref) the behaviour and method to use
        /// </summary>
        /// <param name="isEnabled"></param>
        /// <param name="key"></param>
        /// <param name="targetBehaviour"></param>
        /// <param name="methodName"></param>
        /// <exception cref="Exception"></exception>
        private void GetBehaviourAndMethod(bool isEnabled, string key, ref Behaviour targetBehaviour,
            ref string methodName)
        {
            if ((!isEnabled) || string.IsNullOrEmpty(key) || (key.ToLower() == "none"))
            {
                return;
            }

            //TODO do not do this if the method is the same
            if ((!string.IsNullOrEmpty(key)))
            {
                int splitIndex = key.LastIndexOf('.');
                string typeName = key.Substring(0, splitIndex);
                methodName =
                    key.Substring(splitIndex + 1, key.Length - (splitIndex + 1));

                if (string.IsNullOrEmpty(typeName) || string.IsNullOrEmpty(methodName))
                {
                    throw new Exception("Unable to parse callback method: " + key);
                }

                targetBehaviour = null;

                if (TargetObject == null)
                {
                    throw new Exception("No target set for key " + key);
                }

                foreach (var behaviour in TargetObject.GetComponents<Behaviour>())
                {
                    if (typeName == behaviour.GetType().ToString())
                    {
                        targetBehaviour = behaviour;
                        break;
                    }
                }

                if (targetBehaviour == null)
                {
                    throw new Exception("Unable to find target behaviour: key " + key + " typename " + typeName);
                }
            }
        }

    }
}