﻿using System;
using System.Linq;
using System.Reflection;
using Tantawowa.Extensions;
using UnityEngine;
using UnityEngine.Events;

namespace Tantawowa.TimelineEvents
{
    public class EventInvocationInfo
    {
        public Behaviour TargetBehaviour;
        public MethodInfo MethodInfo;
        public static Type[] SupportedTypes = {typeof(string), typeof(float), typeof(int), typeof(bool)};
        public UnityEvent unityEvent;
        //used for tracking
        public string Key;

        public EventInvocationInfo(string key, Behaviour targetBehaviour, MethodInfo methodInfo, UnityEvent unityEvent)
        {
            Key = key;
            MethodInfo = methodInfo;
            TargetBehaviour = targetBehaviour;
            this.unityEvent = unityEvent;
        }

        private void Invoke(object value)
        {
            if (MethodInfo != null)
            {
                MethodInfo.Invoke(TargetBehaviour, new[] {value});
            }
        }

        private void InvokEnum(int value)
        {
            var type = MethodInfo.GetParameters()[0].ParameterType;
            var enumValue = Enum.ToObject(type, value);
            if (MethodInfo != null)
            {
                MethodInfo.Invoke(TargetBehaviour, new[] {enumValue});
            }
       }

        private void InvokeNoArgs()
        {
            if (MethodInfo != null)
            {
                MethodInfo.Invoke(TargetBehaviour, null);
            }
        }

        public void Invoke(bool isSingleArg, string value)
        {
            try
            {
                if (unityEvent != null)
                {
                    unityEvent.Invoke();
                }

                if (isSingleArg)
                {
                    var paramType = MethodInfo.GetParameters()[0].ParameterType;
                    if (paramType.IsEnum)
                    {
                        Invoke(value.ConvertToType<int>());
                    }
                    else if (SupportedTypes.Contains(paramType))
                    {
                        if (paramType == typeof(string))
                        {
                            Invoke(value);
                        }
                        else if (paramType == typeof(int))
                        {
                            Invoke(value.ConvertToType<int>());
                        }
                        else if (paramType == typeof(float))
                        {
                            Invoke(value.ConvertToType<float>());
                        }
                        else if (paramType == typeof(bool))
                        {
                            Invoke(value.ConvertToType<bool>());
                        }
                        else
                        {
                            Debug.Log(
                                "Could not parse argument value " + value + " for method " + MethodInfo.Name +
                                ". Ignoring");
                        }
                    }
                    else
                    {
                        Debug.Log("Could not parse argument value " + value + " for method " + MethodInfo.Name +
                                  ". Ignoring");
                    }
                }
                else
                {
                    InvokeNoArgs();
                }
            }
            catch (Exception e)
            {
                Debug.Log("Exception while executing timeline event " + MethodInfo.Name + " " + e);
            }
        }
    }
}