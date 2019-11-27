namespace StateMachineTests
{
    public enum MachineState
    {
        Start,
        Idle, 
        Run,
        Stop,
        Off
    }

    public enum MachineEvent
    {
        Honk,
        Start,
        Stop
    }
}
