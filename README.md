# StateMachine - Simple finite state machine in c#
 
Example: Car simualtion

**First, we need to define the possible car states that we are interested to simulate. This can differ from case to case.**
  
  ```
  public enum CarState { Off, On, Start, Drive, Stop }
  
  ```
  
**Second, we need to define events that will trigger certain state transitions. For this case, we have chosen the following ones.**
  
  ```
  public enum CarStateEvent { TurnOn, TurnOff, Acceleerate, Break }
  ```
  
**The CarSimulator class is the acutal simulator that can be used to wrap up the state machine and privide a way to control the class states in a simple way.**

```
  public class CarSimulator
  {
    private StateMachine<CarState, CarStateEvent> _stateMachine;

    public CarSimulator()
    {
        _stateMachine = new StateMachine<CarState, CarStateEvent>(CarState.Off); 
        LoadTransitions();
    }

    private void LoadTransitions()
    { 
        _stateMachine.AddTransition(CarState.Off, CarStateEvent.TurnOn, () => _stateMachine.CurrentState == CarState.Off, CarState.On, () => { });
        _stateMachine.AddTransition(CarState.Off, CarStateEvent.Break, () => _stateMachine.CurrentState == CarState.Off, CarState.Off, () => { });
        _stateMachine.AddTransition(CarState.On, CarStateEvent.Acceleerate, () => _stateMachine.CurrentState == CarState.On, CarState.Drive, () => { }); 
        _stateMachine.AddTransition(CarState.Drive, CarStateEvent.Break, () => _stateMachine.CurrentState == CarState.Drive, CarState.Stop, () => { });
        _stateMachine.AddTransition(CarState.On, CarStateEvent.TurnOff, () => _stateMachine.CurrentState == CarState.On, CarState.Off, () => { });
        _stateMachine.AddTransition(CarState.Stop, CarStateEvent.TurnOff, () => true, CarState.Off, () => { });
     }

        public void TurnOn()
        {
            _stateMachine.HandleEvent(CarStateEvent.TurnOn);
        }

        public void TurnOff()
        {
            _stateMachine.HandleEvent(CarStateEvent.TurnOff);
        }

        public void Accelerate()
        {
            _stateMachine.HandleEvent(CarStateEvent.Acceleerate);
        }

        public void Break()
        {
            _stateMachine.HandleEvent(CarStateEvent.Break);
        }

        public void Stop()
        {
            _stateMachine.HandleEvent(CarStateEvent.Break);
        }
    }

```
