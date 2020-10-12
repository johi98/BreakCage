using System.Collections;
using UnityEngine;
using SWS;

//replaces a waypoint of a path by using an index position
//and sets the corresponding waypoint gameobject to the new gameobject,
//destroying the old waypoint
namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("Simple Waypoint System")]
    [Tooltip("Sets the desired waypoint of a path.")]
    public class SWS_SetWaypointOfPath : FsmStateAction
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

            if (wpIndex.Value >= path.waypoints.Length - 1)
                wpIndex.Value = path.waypoints.Length - 1;
            else if (wpIndex.Value <= 0)
                wpIndex.Value = 0;

            waypoint.Value.name = "Waypoint " + wpIndex.Value;
            waypoint.Value.transform.parent = path.transform;
            Transform oldWaypoint = null;

            if (path is BezierPathManager)
            {
                oldWaypoint = (path as BezierPathManager).bPoints[wpIndex.Value].wp;
                (path as BezierPathManager).bPoints[wpIndex.Value].wp = waypoint.Value.transform;
            }
            else
            {
                oldWaypoint = path.waypoints[wpIndex.Value];
                path.waypoints[wpIndex.Value] = waypoint.Value.transform;
            }
            Object.Destroy(oldWaypoint.gameObject);
        }
    }
}
