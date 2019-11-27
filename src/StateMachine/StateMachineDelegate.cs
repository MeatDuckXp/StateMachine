namespace StateMachine
{
    /// <summary>
    ///     State changes delegate
    /// </summary>
    /// <typeparam name="StateType">State type</typeparam>
    /// <param name="oldState">Old state.</param>
    /// <param name="newState">New State.</param>
    public delegate void StateChangedHandler<StateType>(StateType oldState, StateType newState);

    /// <summary>
    ///     State transition delegate
    /// </summary>
    /// <returns></returns>
    public delegate bool StateTransitionGuard();

    /// <summary>
    ///     State transition action delegate
    /// </summary>
    public delegate void StateTransitionAction();
}
