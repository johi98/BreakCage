using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using SWS;

//manipulates the events list for removing events at a waypoint
namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("Simple Waypoint System")]
    [Tooltip("Removes all events of a walker object at a specific waypoint.")]
    public class SWS_RemoveEventsAtWaypoint : FsmStateAction
    {
        [RequiredField]
        [Tooltip("Walker object")]
        public FsmOwnerDefault walkerObject;

        [UIHint(UIHint.FsmInt)]
        [Tooltip("Waypoint index")]
        public FsmInt wpIndex;


        public override void Reset()
        {
            walkerObject = null;
            wpIndex = null;
        }


        public override void OnEnter()
        {
            Execute();

            Finish();
        }


        void Execute()
        {
            var go = Fsm.GetOwnerDefaultTarget(walkerObject);
            if (go == null) return;

            List<UnityEvent> events = null;

            splineMove spline = go.GetComponentInChildren<splineMove>();
            if (spline)
                events = spline.events;
            else
            {
                navMove nav = go.GetComponentInChildren<navMove>();
                if (nav)
                    events = nav.events;
            }

            if (events == null || events.Count == 0)
            {
                Debug.Log("RemoveEventsAtWaypoint action could not find events on " + go.name);
                return;
            }

            int size = events.Count;        
            if (wpIndex.Value >= size - 1)
                wpIndex.Value = size - 1;
            else if (wpIndex.Value <= 0)
                wpIndex.Value = 0;

            if (size >= wpIndex.Value)
            {
                UnityEvent myEvent = events[wpIndex.Value];
                myEvent.RemoveAllListeners();
            }
        }
    }
}
