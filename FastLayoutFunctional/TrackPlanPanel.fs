namespace FastLayoutFunctional
open System.Windows.Input

type WaitingTrackPlan =
    {
        temp: string;
    }

type BackgroundActionTrackPlan =
    {
        temp: string;
    }

type DrawingNewPath =
    {
        temp: string;
    }

type DraggingMarquee =
    {
        temp: string;
    }

type DraggingBackground =
    {
        temp: string;
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
        WaitingState { WaitingTrackPlan.temp = "waiting" }

    let getText (trackPlan : TrackPlan) =
        match trackPlan with
        | WaitingState state -> state.temp
        | BackgroundActionState state -> state.temp
        | DrawingPathState state -> state.temp
        | MarrqueeState state -> state.temp
        | DraggingBackground state -> state.temp

    let initiateBackgroundAction : InitiateBackgroundAction =
        fun waitingTrackPlan ->
            {temp = waitingTrackPlan.temp}

    let initiatePath : InitiatePathDraw =
        fun _ ->
            {temp = "drawing"}

    let endPath : EndPathDraw =
        fun _ ->
            {temp = "waiting"}

    let startSelectionDrag : StartSelectionDrag = 
        fun _ ->
            {temp = "dragging selection"}

    let endSelectionDrag : EndSelectionDrag =
        fun _ ->
            {temp = "waiting"}
            
    let startBackgroundDrag : StartBackgroundDrag = 
        fun _ ->
            {temp = "dragging background"}

    let endBackgroundDrag : EndBackgroundDrag =
        fun _ ->
            {temp = "waiting"}

    let mouseDown trackPlan =
        match trackPlan with
        | WaitingState state -> BackgroundActionState (initiateBackgroundAction state)
        | DrawingPathState -> trackPlan
        | _ -> failwith "Cannot mouse down in current state"

    let mouseUp trackPlan =
        match trackPlan with
        | BackgroundActionState state -> DrawingPathState(initiatePath state)
        | DrawingPathState state -> WaitingState(endPath state)
        | MarrqueeState state -> WaitingState(endSelectionDrag state)
        | DraggingBackground state -> WaitingState(endBackgroundDrag state)
        | _ -> failwith "Cannot mouse up in current state"

    let mouseMove trackPlan ctrlDown =
        match trackPlan with
        | BackgroundActionState state -> 
            if ctrlDown
            then DraggingBackground(startBackgroundDrag state)
            else MarrqueeState(startSelectionDrag state)
        | _ -> trackPlan
    
