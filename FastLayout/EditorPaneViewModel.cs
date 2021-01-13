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
        }
        internal void MouseUp()
        {
            _trackPlanState = TrackPlanPanel.mouseUp(_trackPlanState);
            OnPropertyChanged(nameof(Text));
        }
        internal void MouseMove()
        {
            _trackPlanState = TrackPlanPanel.mouseMove(_trackPlanState, Keyboard.IsKeyDown(Key.LeftCtrl));
            OnPropertyChanged(nameof(Text));
        }

        public string Text => TrackPlanPanel.getText(_trackPlanState);
    }
}
