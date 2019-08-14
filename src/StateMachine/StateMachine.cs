using System;
using System.Collections.Generic;

namespace StateMachine
{
    /// <summary>
    /// Simmple state machine implementation. Uses list as container to store the state transitions.
    /// </summary>
    /// <typeparam name="StateType">Type of known states</typeparam>
    /// <typeparam name="EventType">Type of known events</typeparam>
    public class StateMachine<StateType, EventType> : IStateMachine<StateType, EventType>
    {
        #region Fields
        private List<StateTransition<StateType, EventType>> _transitions;
        #endregion

        /// <summary>
        /// State changed event
        /// </summary>
        public event StateChangedHandler<StateType> StateChanged;

        /// <summary>
        /// Gets current state
        /// </summary>
        public StateType CurrentState { get; private set; }

        /// <summary>
        /// Gets initial state
        /// </summary>
        public StateType InitialState { get; private set; }

        /// <summary>
        /// Gets transitions count
        /// </summary>
        public int TransitionCount => _transitions.Count;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="initialState">Initial state value</param>
        public StateMachine(StateType initialState)
        {
            InitialState = initialState;
            CurrentState = initialState;
            _transitions = new List<StateTransition<StateType, EventType>>();
        }

        /// <summary>
        /// Creates new instance of state change transition
        /// </summary>
        /// <param name="startState">Start state</param>
        /// <param name="transitionEvent">Event</param>
        /// <param name="guard">Transition guard</param>
        /// <param name="endState">End state</param>
        /// <param name="transitionAction">Transition Action</param>
        public void AddTransition(StateType startState, EventType transitionEvent, StateTransitionGuard guard, StateType endState, StateTransitionAction transitionAction)
        {
            AddTransition(new StateTransition<StateType, EventType>(startState, transitionEvent, guard, endState, transitionAction));
        }

        /// <summary>
        /// Creates new instance of state change transition
        /// </summary>
        /// <param name="newTransition">StateTransition</param>
        public void AddTransition(StateTransition<StateType, EventType> newTransition)
        {
            if (newTransition == default(StateTransition<StateType, EventType>))
            {
                throw new ArgumentNullException(nameof(newTransition));
            }

            _transitions.Add(newTransition);
        }

        /// <summary>
        /// Resets state machine to initial state
        /// </summary>
        public void Reset()
        {
            ChangeState(InitialState);
        }

        /// <summary>
        /// Resets state machine to the new state
        /// </summary>
        /// <param name="newState">New State</param>
        public void Reset(StateType newState)
        {
            ChangeState(newState);
        }

        /// <summary>
        /// Handles transition event 
        /// </summary>
        /// <param name="transitionEvent">Transition event</param>
        public void HandleEvent(EventType transitionEvent)
        {
            for (int i = 0; i < _transitions.Count; i++)
            {
                var currentTransition = _transitions[i];

                if (CurrentState.Equals(currentTransition.CurrentState) && transitionEvent.Equals(currentTransition.Event))
                {
                    if (currentTransition.Guard == null || currentTransition.Guard())
                    {
                        ChangeState(currentTransition.NewState);
                        currentTransition.Action?.Invoke();
                        break;
                    }
                }
            }
        }

        #region Private Methods

        /// <summary>
        /// Changes the current state to the new state
        /// </summary>
        /// <param name="newState">New state</param>
        private void ChangeState(StateType newState)
        {
            if (CurrentState.Equals(newState))
            {
                return;
            }
            var oldState = CurrentState;
            CurrentState = newState;
            StateChanged?.Invoke(oldState, newState);
        }

        #endregion 
    }
}
