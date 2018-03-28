using System;
using UnityEngine;
using UnityEngine.Playables;

namespace Tantawowa.TimelineEvents
{
    public class TimelineEventMixerBehaviour : PlayableBehaviour
    {
        /**
        private float previousPlayheadTime;

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            base.ProcessFrame();
            int inputCount = playable.GetInputCount();

            float time = (float) playable.GetGraph().GetRootPlayable(0).GetTime();

            for (int i = 0; i < inputCount; i++)
            {
                ScriptPlayable<TimelineEventBehaviour> inputPlayable =
                    (ScriptPlayable<TimelineEventBehaviour>) playable.GetInput(i);
                TimelineEventBehaviour input = inputPlayable.GetBehaviour();

                //note - I've made these checks really forgiving for scrubbing back and forth, as this is the behaviour
                //that's comfortable for me
                //you might like to change what's considered an enter/exit to make it more/less strict/behave differently.
                //ie have OnEnter triggered whenever it enters the clip, not just the start time of the clip.
                //I personally found that to make no sense, as my intent is that clip start is the time of the event
                if (((previousPlayheadTime <= input.ClipStartTime) && time > input.ClipStartTime) ||
                    (previousPlayheadTime > input.ClipStartTime && time == input.ClipStartTime) // we loooped back to start
                )
                {
                    input.OnEnter();
                }
            }

            previousPlayheadTime = time;
        }
        **/
    }
}