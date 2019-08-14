using NUnit.Framework;
using StateMachine; 

namespace StateMachineTests
{
    public enum CarState
    {
        Off,
        On,
        Start,
        Drive,
        Stop,
    }

    public enum CarStateEvent
    {
        TurnOn,
        TurnOff,
        Acceleerate,
        Break
    }

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
            //defining transition from off to on. The initial state needs to be off, the event that triggers the transition is TurnOn, the guard is 
            // () => _stateMachine.CurrentState == CarState.Off expression and the new state is On.
            _stateMachine.AddTransition(CarState.Off, CarStateEvent.TurnOn, () => _stateMachine.CurrentState == CarState.Off, CarState.On, () => { });
            _stateMachine.AddTransition(CarState.Off, CarStateEvent.Break, () => _stateMachine.CurrentState == CarState.Off, CarState.Off, () => { });
             
            //having the car started, we can accelerate. To do so, the guard check validates that the current state is in fact on.
            _stateMachine.AddTransition(CarState.On, CarStateEvent.Acceleerate, () => _stateMachine.CurrentState == CarState.On, CarState.Drive, () => { });
 
            //in order to fully stop, we are in driving state we break. The car stops at some poinet.
            _stateMachine.AddTransition(CarState.Drive, CarStateEvent.Break, () => _stateMachine.CurrentState == CarState.Drive, CarState.Stop, () => { });

            //to turn off the car, the car needs to be on, and that is what the guard is cheking. 
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

        public CarState CurrentState => _stateMachine.CurrentState;
    }

    [TestFixture]
    public class CarSimulationTest
    { 
        public CarSimulator MyCar { get; private set; }

        [SetUp]
        public void SetUp()
        {
            MyCar = new CarSimulator();
        }

        [Test]
        public void Test_Can_Start()
        {
            MyCar.TurnOn();
            Assert.AreEqual(CarState.On, MyCar.CurrentState);
        }

        [Test]
        public void Test_Can_Accelerate()
        {
            MyCar.TurnOn();
            MyCar.Accelerate();

            Assert.AreEqual(CarState.Drive, MyCar.CurrentState);
        }

        [Test]
        public void Test_Can_Not_Accelerate_Without_Started_Entine()
        { 
            MyCar.Accelerate(); 
            Assert.AreEqual(CarState.Off, MyCar.CurrentState);
        }

        [Test]
        public void Test_Can_Break_While_Car_Is_Off()
        {
            MyCar.Break();
            Assert.AreEqual(CarState.Off, MyCar.CurrentState);
        }

        [Test]
        public void Test_Drive_Car()
        {
            MyCar.Accelerate();
            Assert.AreEqual(CarState.Off, MyCar.CurrentState); 
            MyCar.TurnOn();
            Assert.AreEqual(CarState.On, MyCar.CurrentState);
            MyCar.Accelerate();
            Assert.AreEqual(CarState.Drive, MyCar.CurrentState);
            MyCar.Break();
            Assert.AreEqual(CarState.Stop, MyCar.CurrentState);
            MyCar.TurnOff();
            Assert.AreEqual(CarState.Off, MyCar.CurrentState); 
        }
    }
}
