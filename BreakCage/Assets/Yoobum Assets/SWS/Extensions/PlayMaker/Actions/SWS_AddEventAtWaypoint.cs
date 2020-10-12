using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using SWS;

//implements a PlayMaker FSM method call via event insert
namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("Simple Waypoint System")]
    [Tooltip("Adds an event to a walker object for calling your own FSM state at waypoints.")]
    public class SWS_AddEventAtWaypoint : FsmStateAction
    {
        [RequiredField]
        [Tooltip("Walker object")]
        public FsmOwnerDefault walkerObject;

        [UIHint(UIHint.FsmInt)]
        [Tooltip("Waypoint index")]
        public FsmInt wpIndex;

        [RequiredField]
        [UIHint(UIHint.FsmGameObject)]
        [Tooltip("Receiver with the FSM event")]
        public PlayMakerFSM fsmReceiver;

        [RequiredField]
        [UIHint(UIHint.FsmString)]
        [Tooltip("Receiver FSM event to call")]
        public FsmString fsmEvent;


        public override void Reset()
        {
            walkerObject = null;
            wpIndex = null;
            fsmReceiver = null;
            fsmEvent = null;
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

            UnityEvent myEvent = events[wpIndex.Value];
            myEvent.AddListener(delegate { fsmReceiver.SendEvent(fsmEvent.Value); });
        }
    }
}
