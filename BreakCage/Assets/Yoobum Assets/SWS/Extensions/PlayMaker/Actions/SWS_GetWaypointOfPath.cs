using System.Collections;
using UnityEngine;
using SWS;

//gets a waypoint of a path by using an index position
//and returns the corresponding waypoint gameobject
namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("Simple Waypoint System")]
    [Tooltip("Gets the desired waypoint of a bezier path.")]
    public class SWS_GetWaypointOfPath : FsmStateAction
    {
        [RequiredField]
        [ObjectType(typeof(PathManager))]
        [Tooltip("Path manager component")]
        public FsmObject pathObject;

        [UIHint(UIHint.FsmInt)]
        [Tooltip("Waypoint index")]
        public FsmInt wpIndex;

        [UIHint(UIHint.FsmGameObject)]
        [Tooltip("Waypoint gameobject")]
        public FsmGameObject waypoint;


        public override void Reset()
        {
            pathObject = null;
            wpIndex = null;
            waypoint = null;
        }


        public override void OnEnter()
        {
            Execute();

            Finish();
        }

        void Execute()
        {
            PathManager path = pathObject.Value as PathManager;

            if (wpIndex.Value > path.waypoints.Length - 1)
                wpIndex.Value = path.waypoints.Length - 1;

            if (path is BezierPathManager)
                waypoint.Value = (path as BezierPathManager).bPoints[wpIndex.Value].wp.gameObject;
            else
                waypoint.Value = path.waypoints[wpIndex.Value].gameObject;
        }
    }
}
