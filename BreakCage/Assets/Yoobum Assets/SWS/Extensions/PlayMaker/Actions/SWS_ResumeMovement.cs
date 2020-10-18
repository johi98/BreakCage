using System.Collections;
using UnityEngine;

//resumes movement of a walker object that uses a movement script
namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("Simple Waypoint System")]
    [Tooltip("Resumes movement of a previously paused walker object.")]
    public class SWS_ResumeMovement : FsmStateAction
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

            go.SendMessage("Resume");
        }
    }
}
