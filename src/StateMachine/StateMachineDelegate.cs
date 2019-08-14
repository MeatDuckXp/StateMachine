namespace StateMachine
{
    public delegate void StateChangedHandler<StateType>(StateType oldState, StateType newState);

    public delegate bool StateTransitionGuard();

    public delegate void StateTransitionAction();
}
