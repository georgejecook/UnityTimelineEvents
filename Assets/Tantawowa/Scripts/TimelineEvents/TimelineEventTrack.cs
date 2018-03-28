using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Tantawowa.TimelineEvents
{
    [TrackColor(0.4448276f, 0f, 1f)]
    [TrackClipType(typeof(TimelineEventClip))]
    [TrackBindingType(typeof(GameObject))]
    public class TimelineEventTrack : TrackAsset
    {
        public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
        {
            var director = go.GetComponent<PlayableDirector>();
            var trackTargetObject = director.GetGenericBinding(this) as GameObject;

            foreach (var clip in GetClips())
            {
                var playableAsset = clip.asset as TimelineEventClip;

                if (playableAsset)
                {
                    playableAsset.TrackTargetObject = trackTargetObject;
                    playableAsset.ClipStartTime = (float) clip.start;
                    playableAsset.ClipEndTime = (float) clip.end;
                }
            }

            var scriptPlayable = ScriptPlayable<TimelineEventMixerBehaviour>.Create(graph, inputCount);
            return scriptPlayable;
        }
    }
}