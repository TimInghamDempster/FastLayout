using FastLayoutFunctional;
using System.Windows.Input;

namespace FastLayout
{
    class EditorPaneViewModel : ViewModelBase
    {
        private TrackPlan _trackPlanState;

        public EditorPaneViewModel()
        {
            _trackPlanState = TrackPlanPanel.create;
            TrackPlanPanel.mouseDown(_trackPlanState);
        }

        internal void MouseDown()
        {
            _trackPlanState = TrackPlanPanel.mouseDown(_trackPlanState);
            OnPropertyChanged(nameof(Text));
            OnPropertyChanged(nameof(IsDrawing));
            OnPropertyChanged(nameof(IsSelecting));
        }
        internal void MouseUp()
        {
            var pos = Mouse.GetPosition(null);
            _trackPlanState = TrackPlanPanel.mouseUp(_trackPlanState, pos.X, pos.Y);
            OnPropertyChanged(nameof(Text));
            OnPropertyChanged(nameof(IsDrawing));
            OnPropertyChanged(nameof(IsSelecting));
            OnPropertyChanged(nameof(NewPathEnd));
            OnPropertyChanged(nameof(RectEndPoint));
            OnPropertyChanged(nameof(NewPathStart));
            OnPropertyChanged(nameof(RectStartPoint));
        }
        internal void MouseMove()
        {
            var pos = Mouse.GetPosition(null);
            _trackPlanState = 
                TrackPlanPanel.mouseMove(
                    _trackPlanState, 
                    Keyboard.IsKeyDown(Key.LeftCtrl),
                    pos.X,
                    pos.Y);
            OnPropertyChanged(nameof(Text));
            OnPropertyChanged(nameof(IsDrawing));
            OnPropertyChanged(nameof(IsSelecting));
            OnPropertyChanged(nameof(NewPathEnd));
            OnPropertyChanged(nameof(RectEndPoint));
            OnPropertyChanged(nameof(NewPathStart));
            OnPropertyChanged(nameof(RectStartPoint));
        }

        public string Text => TrackPlanPanel.getData(_trackPlanState).currentAction;

        public bool IsDrawing => _trackPlanState.IsDrawingPathState;
        public bool IsSelecting => _trackPlanState.IsMarrqueeState;

        public Point NewPathStart => TrackPlanPanel.getData(_trackPlanState).startPos;
        public Point NewPathEnd => TrackPlanPanel.getData(_trackPlanState).endPos;
        public Point RectEndPoint => TrackPlanPanel.getData(_trackPlanState).rectEndPos;
        public Point RectStartPoint => TrackPlanPanel.getData(_trackPlanState).rectStartPos;
    }
}
