using System.Collections;
using UnityEngine;

//pauses movement of a walker object that uses a movement script
namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("Simple Waypoint System")]
    [Tooltip("Pauses movement of a walker object.")]
    public class SWS_PauseMovement : FsmStateAction
    {
        [RequiredField]
        [Tooltip("Walker object")]
        public FsmOwnerDefault walkerObject;

        [UIHint(UIHint.FsmInt)]
        [Tooltip("Duration")]
        public FsmFloat duration;


        public override void Reset()
        {
            walkerObject = null;
            duration = 0f;
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

            go.SendMessage("Pause", duration.Value);
        }
    }
}
