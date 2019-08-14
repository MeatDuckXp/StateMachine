namespace StateMachine
{
    public interface IStateMachine<StateType, EventType>
    {
        /// <summary>
        /// State changed event
        /// </summary>
        event StateChangedHandler<StateType> StateChanged;

        /// <summary>
        /// Gets current state
        /// </summary>
        StateType CurrentState { get; }

        /// <summary>
        /// Gets initial state
        /// </summary>
        StateType InitialState { get; }

        /// <summary>
        /// Gets transitions count
        /// </summary>
        int TransitionCount { get; }

        /// <summary>
        /// Creates new instance of state change transition
        /// </summary>
        /// <param name="startState">Start state</param>
        /// <param name="transitionEvent">Event</param>
        /// <param name="guard">Transition guard</param>
        /// <param name="endState">End state</param>
        /// <param name="transitionAction">Transition Action</param>
        void AddTransition(StateType startState, EventType transitionEvent, StateTransitionGuard guard, StateType endState, StateTransitionAction transitionAction);

        /// <summary>
        /// Creates new instance of state change transition
        /// </summary>
        /// <param name="newTransition">StateTransition</param>
        void AddTransition(StateTransition<StateType, EventType> newTransition);

        /// <summary>
        /// Resets state machine to initial state
        /// </summary>
        void Reset();

        /// <summary>
        /// Resets state machine to the new state
        /// </summary>
        /// <param name="newState">New State</param>
        void Reset(StateType newState);

        /// <summary>
        /// Handles transition event 
        /// </summary>
        /// <param name="transitionEvent">Transition event</param>
        void HandleEvent(EventType gameEvent);
    }
}
