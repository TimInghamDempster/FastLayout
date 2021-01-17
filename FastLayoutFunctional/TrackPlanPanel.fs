namespace FastLayoutFunctional

open System

type Point =
    {
        X: double;
        Y: double;
    }

type trackPlanData = 
    {
        currentAction: string;
        startPos: Point;
        endPos: Point;
        rectEndPos: Point; 
        rectStartPos: Point; 
    }

type WaitingTrackPlan =
    {
        data: trackPlanData;
    }

type BackgroundActionTrackPlan =
    {
        data: trackPlanData;
    }
type DrawingNewPath =
    {
        data: trackPlanData;
    }

type DraggingMarquee =
    {
        data: trackPlanData;
    }

type DraggingBackground =
    {
        data: trackPlanData;
    }

type TrackPlan=
    | WaitingState of WaitingTrackPlan
    | BackgroundActionState of BackgroundActionTrackPlan
    | DrawingPathState of DrawingNewPath
    | MarrqueeState of DraggingMarquee
    | DraggingBackground of DraggingBackground

type InitiateBackgroundAction = 
    WaitingTrackPlan -> BackgroundActionTrackPlan

type InitiatePathDraw =
    BackgroundActionTrackPlan -> DrawingNewPath

type EndPathDraw =
    DrawingNewPath -> WaitingTrackPlan

type StartSelectionDrag =
    BackgroundActionTrackPlan -> DraggingMarquee
    
type EndSelectionDrag =
    DraggingMarquee -> WaitingTrackPlan

type StartBackgroundDrag = 
    BackgroundActionTrackPlan -> DraggingBackground
    
type EndBackgroundDrag =
    DraggingBackground -> WaitingTrackPlan

module TrackPlanPanel =
    let create =
        WaitingState { data = {currentAction = "waiting"; startPos = {X =10.0; Y = 10.0}; endPos = {X =100.0; Y = 100.0}; rectStartPos = {X =10.0; Y = 10.0}; rectEndPos = {X =100.0; Y = 100.0}}}

    let getData trackPlan =
        let data =
            match trackPlan with
            | WaitingState state -> state.data
            | BackgroundActionState state -> state.data
            | DrawingPathState state -> state.data
            | MarrqueeState state -> state.data
            | DraggingBackground state -> state.data
        let topLeft = {X = Math.Min(data.startPos.X, data.endPos.X); Y = Math.Min(data.startPos.Y, data.endPos.Y)}
        let bottomRight = {X = Math.Max(data.startPos.X, data.endPos.X); Y = Math.Max(data.startPos.Y, data.endPos.Y)}
        let endPos = {X = bottomRight.X - topLeft.X; Y = bottomRight.Y - topLeft.Y}
        {data with rectEndPos = endPos; rectStartPos = topLeft}

    let initiateBackgroundAction : InitiateBackgroundAction =
        fun waitingTrackPlan ->
            {data = waitingTrackPlan.data}

    let initiatePath : InitiatePathDraw =
        fun state ->
            {data = {state.data with currentAction = "drawing"; }}

    let endPath : EndPathDraw =
        fun state ->
            {data = {state.data with currentAction = "waiting"}}

    let startSelectionDrag : StartSelectionDrag = 
        fun state ->
            {data = {state.data with currentAction = "dragging selection"}}

    let endSelectionDrag : EndSelectionDrag =
        fun state ->
            {data = {state.data with currentAction = "waiting"}}
            
    let startBackgroundDrag : StartBackgroundDrag = 
        fun state ->
            {data = {state.data with currentAction = "dragging background"}}

    let endBackgroundDrag : EndBackgroundDrag =
        fun state ->
            {data = {state.data with currentAction = "waiting"}}

    let updateEnd data x y =
        {data with endPos = {X = x; Y =y}}

    let updateStartAndEnd data x y =
        let endUpdated = updateEnd data x y
        {endUpdated with startPos = {X = x; Y = y}}

    let mouseDown trackPlan =
        match trackPlan with
        | WaitingState state -> BackgroundActionState (initiateBackgroundAction state)
        | DrawingPathState -> trackPlan
        | _ -> failwith "Cannot mouse down in current state"

    let mouseUp trackPlan x y =
        match trackPlan with
        | BackgroundActionState state -> DrawingPathState(initiatePath {state with data = updateStartAndEnd state.data x y})
        | DrawingPathState state -> WaitingState(endPath state)
        | MarrqueeState state -> WaitingState(endSelectionDrag state)
        | DraggingBackground state -> WaitingState(endBackgroundDrag state)
        | _ -> failwith "Cannot mouse up in current state"

    let mouseMove trackPlan ctrlDown x y =
        match trackPlan with
        | BackgroundActionState state -> 
            if ctrlDown
            then DraggingBackground(startBackgroundDrag state)
            else MarrqueeState(startSelectionDrag {state with data = updateStartAndEnd state.data x y})
        | DrawingPathState state -> DrawingPathState({state with data = updateEnd state.data x y})
        | MarrqueeState state -> MarrqueeState({state with data = updateEnd state.data x y})
        | _ -> trackPlan
    
