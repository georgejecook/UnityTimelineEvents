using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Tantawowa.TimelineEvents
{
    [Serializable]
    public class TimelineEventClip : PlayableAsset, ITimelineClipAsset
    {
        public TimelineEventBehaviour template = new TimelineEventBehaviour();
        public GameObject TrackTargetObject { get; set; }
        
        public ClipCaps clipCaps
        {
            get { return ClipCaps.None; }
        }

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<TimelineEventBehaviour>.Create(graph, template);
            TimelineEventBehaviour clone = playable.GetBehaviour();
            clone.TargetObject = TrackTargetObject;

            clone.clip = this;

            TimelineEventProxy.Instance.EventForClip(this);

            return playable;
        }
    }
}