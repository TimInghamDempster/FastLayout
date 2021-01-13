using System;
using System.Collections.Generic;
using System.Text;

namespace FastLayout
{
    class MainWindowViewModel
    {
        public MainWindowViewModel(EditorPaneViewModel editorPaneViewModel, PropertyPanelViewModel propertyPanelViewModel)
        {
            EditorPaneViewModel = editorPaneViewModel;
            PropertyPanelViewModel = propertyPanelViewModel;
        }

        public EditorPaneViewModel EditorPaneViewModel { get; }

        public PropertyPanelViewModel PropertyPanelViewModel { get; }

        internal void MouseDown()
        {
            EditorPaneViewModel.MouseDown();
        }
        internal void MouseUp()
        {
            EditorPaneViewModel.MouseUp();
        }
        internal void MouseMove()
        {
            EditorPaneViewModel.MouseMove();
        }
    }
}
