using UnityEngine;

using Framework;

namespace Tool
{
    public sealed class ToolStateChanger : Singleton<ToolStateChanger>
    {
        public ToolStates CurrentState { get; private set; }

        [SerializeField] private ToolStates startState;

        private void Start()
        {
            CurrentState = startState;
        }

        public void SetCurrentState(ToolStates targetState)
        {
            if (targetState == CurrentState)
                return;
            
            CurrentState = targetState;
        }

        // int wrapper funtion because Unity can't do enums in unityevents
        public void SetCurrentState(int targetState) => SetCurrentState((ToolStates) targetState);
    }
}