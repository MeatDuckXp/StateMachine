namespace StateMachine
{
    public class StateTransition<StateType, EventType>
    {
        public StateType CurrentState { get; set; }

        public StateType NewState { get; set; }

        public EventType Event { get; set; }

        public StateTransitionGuard Guard { get; set; }

        public StateTransitionAction Action { get; set; }

        public StateTransition(StateType currentState, EventType stateMachineEvent, StateTransitionGuard guard, StateType newState, StateTransitionAction action)
        {
            CurrentState = currentState;
            Event = stateMachineEvent;
            Guard = guard;
            NewState = newState;
            Action = action;
        }

    }
}
