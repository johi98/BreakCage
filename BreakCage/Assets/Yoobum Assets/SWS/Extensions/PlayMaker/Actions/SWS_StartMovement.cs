using System.Collections;
using UnityEngine;

//starts movement of a walker object that uses a movement script
namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("Simple Waypoint System")]
    [Tooltip("Starts movement of a walker object.")]
    public class SWS_StartMovement : FsmStateAction
    {
        [RequiredField]
        [Tooltip("Walker object")]
        public FsmOwnerDefault walkerObject;


        public override void Reset()
        {
            walkerObject = null;
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

            go.SendMessage("StartMove");
        }
    }
}
