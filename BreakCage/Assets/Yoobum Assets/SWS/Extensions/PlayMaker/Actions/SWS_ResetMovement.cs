using System.Collections;
using UnityEngine;

//resets movement of a walker object that uses a movement script
namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("Simple Waypoint System")]
    [Tooltip("Resets movement of a walker object. Optionally repositions it to the start.")]
    public class SWS_ResetMovement : FsmStateAction
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

            go.SendMessage("ResetToStart");
        }
    }
}
