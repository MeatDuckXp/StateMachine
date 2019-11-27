namespace StateMachine
{
    /// <summary>
    ///     This class represents a state transition definition.
    /// </summary>
    /// <typeparam name="StateType">Type of known states.</typeparam>
    /// <typeparam name="EventType">Type of known events.</typeparam>
    public class StateTransition<StateType, EventType>
    {
        /// <summary>
        ///     Gets Current State
        /// </summary>
        public StateType CurrentState { get; set; }

        /// <summary>
        ///     Gets New state
        /// </summary>
        public StateType NewState { get; set; }

        /// <summary>
        ///     Gets Event that triggers the state change
        /// </summary>
        public EventType Event { get; set; }

        /// <summary>
        ///     Gets Guard action
        /// </summary>
        public StateTransitionGuard Guard { get; set; }

        /// <summary>
        ///     Gets State transition action
        /// </summary>
        public StateTransitionAction Action { get; set; }

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="currentState">Current state.</param>
        /// <param name="stateMachineEvent">State machine event.</param>
        /// <param name="guard">Transition guard.</param>
        /// <param name="newState">New state.</param>
        /// <param name="action">Action.</param>
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
