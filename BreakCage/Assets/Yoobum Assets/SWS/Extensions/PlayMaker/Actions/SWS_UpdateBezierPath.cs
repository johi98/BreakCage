using System.Collections;
using UnityEngine;
using SWS;

//recalculates bezier path waypoints
namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("Simple Waypoint System")]
    [Tooltip("Recalculates waypoints of a bezier path in case it moved. Note that this is a performance-heavy action.")]
    public class SWS_UpdateBezierPath : FsmStateAction
    {
        [UIHint(UIHint.FsmString)]
        [Tooltip("Define path name OR path object")]
        public FsmString pathName;

        [ObjectType(typeof(BezierPathManager))]
        [Tooltip("Define path name OR path object")]
        public FsmObject pathObject;

        [UIHint(UIHint.FsmBool)]
        [Tooltip("Update per frame")]
        public bool everyFrame;


        public override void Reset()
        {
            pathName = null;
            pathObject = null;
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
            PathManager path = null;

            if (pathName.Value != "")
                WaypointManager.Paths.TryGetValue(pathName.Value, out path);
            else if (pathObject != null)
                path = pathObject.Value as BezierPathManager;

            if (path == null) return;

            (path as BezierPathManager).CalculatePath();
        }
    }
}
