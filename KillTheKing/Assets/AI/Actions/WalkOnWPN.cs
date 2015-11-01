using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;
using RAIN.Representation;
using RAIN.Motion;
using RAIN.Navigation;
using RAIN.Navigation.Graph;
using RAIN.Navigation.Waypoints;

[RAINAction]
public class WalkOnWPN : RAINAction
{
	public Expression Destination = new Expression();
	public Expression WaypointNetwork = new Expression();	
	public Expression MoveTargetVariable = new Expression();

	private MoveLookTarget moveTarget = new MoveLookTarget();
	private int lastWaypoint = -1;
	private WaypointSet lastWaypointSet = null;

    public override void Start(RAIN.Core.AI ai)
    {

        base.Start(ai);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
		if (!MoveTargetVariable.IsValid)
			return ActionResult.FAILURE;


        return ActionResult.SUCCESS;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }


}