using System.Collections;
using UnityEngine;
using SWS;

//sets the path of a walker object
namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("Simple Waypoint System")]
    [Tooltip("Sets the path of a walker object and starts movement. Define path name OR path object.")]
    public class SWS_SetPath : FsmStateAction
    {
        [RequiredField]
        [Tooltip("Walker object")]
        public FsmOwnerDefault walkerObject;

        [UIHint(UIHint.FsmString)]
        [Tooltip("Define path name OR path object")]
        public FsmString pathName;

        [ObjectType(typeof(PathManager))]
        [Tooltip("Define path name OR path object")]
        public FsmObject pathObject;


        public override void Reset()
        {
            walkerObject = null;
            pathObject = null;
            pathName = null;
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

            if (pathName.Value != "")
                go.SendMessage("SetPath", WaypointManager.Paths[pathName.Value]);
            else if (pathObject != null)
                go.SendMessage("SetPath", pathObject.Value as PathManager);
        }
    }
}
