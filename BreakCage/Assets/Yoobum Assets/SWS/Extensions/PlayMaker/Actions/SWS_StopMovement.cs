using System.Collections;
using UnityEngine;

//stops movement of a walker object that uses a movement script
namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("Simple Waypoint System")]
    [Tooltip("Stops movement of a walker object.")]
    public class SWS_StopMovement : FsmStateAction
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

            go.SendMessage("Stop");
        }
    }
}
