using System.Collections;
using UnityEngine;

//changes speed of a walker object
namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("Simple Waypoint System")]
    [Tooltip("Changes speed of a walker object. Consider not changing this per frame.")]
    public class SWS_ChangeSpeed : FsmStateAction
    {
        [RequiredField]
        [Tooltip("Walker object")]
        public FsmOwnerDefault walkerObject;

        [UIHint(UIHint.FsmFloat)]
        [Tooltip("Speed value")]
        public FsmFloat speed;

        [UIHint(UIHint.FsmBool)]
        [Tooltip("Update per frame")]
        public bool everyFrame;


        public override void Reset()
        {
            walkerObject = null;
            speed = null;
            everyFrame = false;
        }


        public override void OnEnter()
        {
            Execute();

            if(!everyFrame)
                Finish();
        }


        public override void OnUpdate()
        {
            Execute();
        }


        void Execute()
        {
            var go = Fsm.GetOwnerDefaultTarget(walkerObject);
            if (go == null) return;

            go.SendMessage("ChangeSpeed", speed.Value);
        }
    }
}
