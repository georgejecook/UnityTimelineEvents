using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Tantawowa.TimelineEvents
{
    [ExecuteInEditMode]
    public class TimelineEventProxy : MonoBehaviour
    {
        //THIS SHOULD NOT BE DONE THIS WAY
        //YET, I DID.
        //TODO: FIX MESS.
        private static TimelineEventProxy instance;
        public static TimelineEventProxy Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<TimelineEventProxy>();
                    if(instance == null)
                    {
                        var go = new GameObject();
                        go.name = typeof(TimelineEventProxy).Name;
                        instance = go.AddComponent<TimelineEventProxy>();
                    }
                }
                return instance;
            }
        }

        //Why this is not a dictionary:
        //1-Serializables (even though there are serializable implementations out there...)
        //2-SerializedProperty can't dig through a dictionary (I think) it needs something indexed.
        //2.b they are private so no other class would mess with them, because they need to be in sync
        //2.c Hidden in inspector so one mess with it.

        [SerializeField]
        [HideInInspector]
        private List<UnityEvent> events = new List<UnityEvent>();
        [SerializeField]
        [HideInInspector]
        private List<TimelineEventClip> clips = new List<TimelineEventClip>();

        public UnityEvent EventForClip(TimelineEventClip clip)
        {
            if (clip == null)
                return null;

            if (!clips.Contains(clip))
            {
                clips.Add(clip);
                events.Add(new UnityEvent());
            }
            return events[clips.IndexOf(clip)];
        }

        public int IndexOfClip(TimelineEventClip clip)
        {
            if (clip == null)
                return -1;

            return clips.IndexOf(clip);
        }

    }
}
