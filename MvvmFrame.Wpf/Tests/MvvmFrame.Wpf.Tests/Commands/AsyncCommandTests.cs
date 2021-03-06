﻿using GetcuReone.MvvmFrame.Wpf.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvvmFrame.Wpf.TestsCommon;
using System;
using System.Threading.Tasks;
using MvvmFrame.Wpf.TestAdapter.Helpers;

namespace MvvmFrame.Wpf.Tests.Commands
{
    [TestClass]
    public class AsyncCommandTests : UiTestBase<CommandViewModel>
    {
        [Timeout(Timeuots.Second.Ten)]
        [Description("[ui][async][command] run command")]
        [TestMethod]
        public void AsyncCommand_RunCommandTestCase()
        {
            object commandComlited = null;

            GivenInitViewModel()
                .And("Init command", viewModel =>
                {
                    commandComlited = false;
                    viewModel.AsyncCommand = new AsyncCommand(async _ =>
                    {
                        await Task.Delay(Timeuots.Millisecond.Hundred);
                        commandComlited = true;
                    });
                    return viewModel;
                })
                .AndAsync("Navigate", viewModel => NavigateAndWaitLoadPageAsync<CommandPage, CommandViewModel>(viewModel))
                .WhenAsync("Click button", async page =>
                {
                    page.btnAsyncCommand.OnClick();
                    await Task.Delay(Timeuots.Millisecond.Hundred);
                })
                .Then("Check run command", () => Assert.IsTrue(Convert.ToBoolean(commandComlited), "Command not runed"))
                .RunWindow(Timeuots.Second.Ten);
        }

        [Timeout(Timeuots.Second.Five)]
        [Description("[ui][async][command] run command")]
        [TestMethod]
        public void AsyncCommand_RunFinishOperationTestCase()
        {
            object finishComlited = false;

            GivenInitViewModel()
                .And("Init command", viewModel =>
                {
                    viewModel.AsyncCommand = new AsyncCommand(async e =>
                    {
                        await Task.Delay(Timeuots.Millisecond.Hundred);
                        e.AddFinalOperation(() => finishComlited = true);
                    });
                    return viewModel;
                })
                .AndAsync("Navigate", viewModel => NavigateAndWaitLoadPageAsync<CommandPage, CommandViewModel>(viewModel))
                .WhenAsync("Click button", async page =>
                {
                    page.btnAsyncCommand.OnClick();
                    await Task.Delay(Timeuots.Millisecond.Hundred);
                })
                .Then("Check run command", () => Assert.IsTrue(Convert.ToBoolean(finishComlited), "Finis operation not runed"))
                .RunWindow(Timeuots.Second.Five);
        }

        [Timeout(Timeuots.Second.Twenty)]
        [Description("[ui][async][command] run command")]
        [TestMethod]
        public void AsyncCommand_RunCompensationOperationTestCase()
        {
            object compensationComlited = false;
            object finishCommand = false;
            object finishOperationCoplited = false;

            GivenInitViewModel()
                .And("Init command", viewModel =>
                {
                    viewModel.AsyncCommand = new AsyncCommand(async e =>
                    {
                        await Task.Delay(Timeuots.Millisecond.Hundred);
                        e.AddFinalOperation(() => finishOperationCoplited = true);
                        e.AddCompensation(() =>
                        {
                            compensationComlited = true;
                            return default;
                        });
                        e.Cancel();

                        if (e.IsCancel)
                            return;

                        finishCommand = true;
                    });
                    return viewModel;
                })
                .AndAsync("Navigate", viewModel => NavigateAndWaitLoadPageAsync<CommandPage, CommandViewModel>(viewModel))
                .WhenAsync("Click button", async page =>
                {
                    page.btnAsyncCommand.OnClick();
                    await Task.Delay(Timeuots.Millisecond.Hundred);
                })
                .Then("Check run command", () =>
                {
                    Assert.IsTrue(Convert.ToBoolean(compensationComlited), $"compensation operation not runed. Value: {compensationComlited}");
                    Assert.IsFalse(Convert.ToBoolean(finishCommand), "finishCommand is false");
                    Assert.IsTrue(Convert.ToBoolean(finishOperationCoplited), "Finis operation not runed");
                })
                .RunWindow(Timeuots.Second.Twenty);
        }

        [Timeout(Timeuots.Second.Ten)]
        [Description("[ui][async][command] run command")]
        [TestMethod]
        public void AsyncCommand_P_RunCommandTestCase()
        {
            object commandComlited = false;

            GivenInitViewModel()
                .And("Init command", viewModel =>
                {
                    viewModel.AsyncCommandWithParam = new AsyncCommand<CommandViewModel>(async e =>
                    {
                        await Task.Delay(Timeuots.Millisecond.Hundred);
                        Assert.AreEqual(viewModel, e.CommandParam, "CommandParam must be view-model");
                        commandComlited = true;
                    });
                    return viewModel;
                })
                .AndAsync("Navigate", viewModel => NavigateAndWaitLoadPageAsync<CommandPage, CommandViewModel>(viewModel))
                .WhenAsync("Click button", async page =>
                {
                    page.btnAsyncCommandParam.OnClick();
                    await Task.Delay(Timeuots.Millisecond.Hundred);
                })
                .Then("Check run command", () => Assert.IsTrue(Convert.ToBoolean(commandComlited), "Command not runed"))
                .RunWindow(Timeuots.Second.Ten);
        }

        [Timeout(Timeuots.Second.Five)]
        [Description("[ui][async][command] run command")]
        [TestMethod]
        public void AsyncCommand_P_RunFinishOperationTestCase()
        {
            object finishComlited = false;

            GivenInitViewModel()
                .And("Init command", viewModel =>
                {
                    viewModel.AsyncCommandWithParam = new AsyncCommand<CommandViewModel>(async e =>
                    {
                        await Task.Delay(Timeuots.Millisecond.Hundred);
                        Assert.AreEqual(viewModel, e.CommandParam, "CommandParam must be view-model");
                        e.AddFinalOperation(() => finishComlited = true);
                    });
                    return viewModel;
                })
                .AndAsync("Navigate", viewModel => NavigateAndWaitLoadPageAsync<CommandPage, CommandViewModel>(viewModel))
                .WhenAsync("Click button", async page =>
                {
                    page.btnAsyncCommandParam.OnClick();
                    await Task.Delay(Timeuots.Second.One);
                })
                .Then("Check run command", () => Assert.IsTrue(Convert.ToBoolean(finishComlited), "Finis operation not runed"))
                .RunWindow(Timeuots.Second.Five);
        }

        [Timeout(Timeuots.Second.Five)]
        [Description("[ui][async][command] run command")]
        [TestMethod]
        public void AsyncCommand_P_RunCompensationOperationTestCase()
        {
            object compensationComlited = false;
            object finishCommand = false;
            object finishOperationCoplited = false;

            GivenInitViewModel()
                .And("Init command", viewModel =>
                {
                    viewModel.AsyncCommandWithParam = new AsyncCommand<CommandViewModel>(async e =>
                    {
                        Assert.AreEqual(viewModel, e.CommandParam, "CommandParam must be view-model");
                        e.AddFinalOperation(() => finishOperationCoplited = true);
                        e.AddCompensation(() =>
                        {
                            compensationComlited = true;
                            return default;
                        });
                        e.Cancel();

                        if (e.IsCancel)
                            return;

                        finishCommand = true;
                    });
                    return viewModel;
                })
                .AndAsync("Navigate", viewModel => NavigateAndWaitLoadPageAsync<CommandPage, CommandViewModel>(viewModel))
                .WhenAsync("Click button", async page =>
                {
                    page.btnAsyncCommandParam.OnClick();
                    await Task.Delay(Timeuots.Millisecond.Hundred);
                })
                .Then("Check run command", () =>
                {
                    Assert.IsTrue(Convert.ToBoolean(compensationComlited), "compensation operation not runed");
                    Assert.IsFalse(Convert.ToBoolean(finishCommand), "finishCommand is false");
                    Assert.IsTrue(Convert.ToBoolean(finishOperationCoplited), "Finis operation not runed");
                })
                .RunWindow(Timeuots.Second.Five);
        }
    }
}
