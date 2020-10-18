using System.Collections;
using UnityEngine;
using SWS;

//assigns a random path to the walker object
namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("Simple Waypoint System")]
    [Tooltip("Assigns a random path of the list to the walker object and starts movement.")]
    public class SWS_SetPathRandom : FsmStateAction
    {
        [RequiredField]
        [Tooltip("Walker object")]
        public FsmOwnerDefault walkerObject;

        [Tooltip("Path array")]
        public PathManager[] paths;


        public override void Reset()
        {
            walkerObject = null;
            paths = null;
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

            int index = Random.Range(0, paths.Length);
            PathManager pathObject = paths[index];
            if(pathObject is PathManager)
                go.SendMessage("SetPath", pathObject as PathManager);
            else
                go.SendMessage("SetPath", pathObject as BezierPathManager);
        }
    }
}
