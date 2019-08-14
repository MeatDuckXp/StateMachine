using NUnit.Framework;
using StateMachine;

namespace StateMachineTests
{
    [TestFixture]
    public class StateMachineTest
    {
        [Test]
        public void Test_Create_Instance()
        {
            var stateMachine = new StateMachine<MachineState, MachineEvent>(MachineState.Off);
            Assert.NotNull(stateMachine);
        }

        [Test]
        public void Test_Initial_State_Set()
        {
            var stateMachine = new StateMachine<MachineState, MachineEvent>(MachineState.Off);
            Assert.AreEqual(MachineState.Off, stateMachine.InitialState);
        }

        [Test]
        public void Test_Current_State_Set()
        {
            var stateMachine = new StateMachine<MachineState, MachineEvent>(MachineState.Off);
            Assert.AreEqual(MachineState.Off, stateMachine.CurrentState);
        }

        [Test]
        public void Test_Register_State_Transition()
        {
            var stateMachine = new StateMachine<MachineState, MachineEvent>(MachineState.Off);
            stateMachine.AddTransition(MachineState.Off, MachineEvent.Start, () => { return true; }, MachineState.Idle, () => { });
            Assert.AreEqual(1, stateMachine.TransitionCount);
        }

        [Test]
        public void Test_Register_Multiple_State_Transition()
        {
            var stateMachine = new StateMachine<MachineState, MachineEvent>(MachineState.Off);

            stateMachine.AddTransition(MachineState.Off, MachineEvent.Start, () => { return true; }, MachineState.Idle, () => { });
            stateMachine.AddTransition(MachineState.Off, MachineEvent.Start, () => { return true; }, MachineState.Idle, () => { });
            stateMachine.AddTransition(MachineState.Off, MachineEvent.Start, () => { return true; }, MachineState.Idle, () => { });
            stateMachine.AddTransition(MachineState.Off, MachineEvent.Start, () => { return true; }, MachineState.Idle, () => { });
            stateMachine.AddTransition(MachineState.Off, MachineEvent.Start, () => { return true; }, MachineState.Idle, () => { });

            Assert.AreEqual(5, stateMachine.TransitionCount);
        }

        [Test]
        public void Test_Change_State_For_Existing_Transition()
        {
            var stateMachine = new StateMachine<MachineState, MachineEvent>(MachineState.Off);
            stateMachine.AddTransition(MachineState.Off, MachineEvent.Start, () => { return true; }, MachineState.Idle, () => { });
            stateMachine.AddTransition(MachineState.Off, MachineEvent.Honk, () => { return true; }, MachineState.Idle, () => { });

            stateMachine.HandleEvent(MachineEvent.Start);

            Assert.AreEqual(MachineState.Idle, stateMachine.CurrentState);
        }

        [Test]
        public void Test_Change_State_For_Non_Existing_Transition()
        {
            var stateMachine = new StateMachine<MachineState, MachineEvent>(MachineState.Off);
            stateMachine.AddTransition(MachineState.Off, MachineEvent.Start, () => { return true; }, MachineState.Idle, () => { });
            stateMachine.AddTransition(MachineState.Off, MachineEvent.Honk, () => { return true; }, MachineState.Idle, () => { });

            stateMachine.HandleEvent(MachineEvent.Stop);

            Assert.AreEqual(MachineState.Off, stateMachine.CurrentState);
        }

        [Test]
        public void Test_Guard_Methods_Gets_Exectued()
        {
            var guardStatementHasBeenExecuted = false;

            var guard = new StateTransitionGuard(() =>
            {
                guardStatementHasBeenExecuted = true;
                return true;
            });

            var stateMachine = new StateMachine<MachineState, MachineEvent>(MachineState.Off);
            stateMachine.AddTransition(MachineState.Off, MachineEvent.Start, guard, MachineState.Idle, () => { });
            stateMachine.AddTransition(MachineState.Off, MachineEvent.Honk, () => { return true; }, MachineState.Idle, () => { });

            stateMachine.HandleEvent(MachineEvent.Start);

            Assert.IsTrue(guardStatementHasBeenExecuted);
            Assert.AreEqual(MachineState.Idle, stateMachine.CurrentState);
        }

        [Test]
        public void Test_State_Transition_Action_Gets_Exectued()
        {
            var actionHasBeenExecuted = false;
            var transitionAciton = new StateTransitionAction(() =>
            {
                actionHasBeenExecuted = true;
            });

            var stateMachine = new StateMachine<MachineState, MachineEvent>(MachineState.Off);
            stateMachine.AddTransition(MachineState.Off, MachineEvent.Start, () => { return true; }, MachineState.Idle, transitionAciton);
            stateMachine.AddTransition(MachineState.Off, MachineEvent.Honk, () => { return true; }, MachineState.Idle, () => { });

            stateMachine.HandleEvent(MachineEvent.Start);

            Assert.IsTrue(actionHasBeenExecuted);
            Assert.AreEqual(MachineState.Idle, stateMachine.CurrentState);
        }
    }
}
