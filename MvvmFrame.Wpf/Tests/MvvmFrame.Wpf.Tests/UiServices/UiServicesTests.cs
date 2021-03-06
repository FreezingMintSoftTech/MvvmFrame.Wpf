﻿using GetcuReone.MvvmFrame.Wpf;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvvmFrame.Wpf.TestAdapter.Helpers;
using MvvmFrame.Wpf.Tests.UiServices.Env;
using MvvmFrame.Wpf.TestsCommon;
using System;
using System.Windows.Controls;

namespace MvvmFrame.Wpf.Tests.UiServices
{
    [TestClass]
    public sealed class UiServicesTests
    {
        private Frame _frame;
        private UiServicesViewModel ViewModel { get; set; }

        public void Initialize()
        {
            _frame = new Frame();

            ViewModel = ViewModelBase.CreateViewModel<UiServicesViewModel>(_frame)
                .CheckCreateObject(2);
        }

        [TestMethod]
        [Description("[view-model][service] Checking the transfer of services when use GetViewModel")]
        [Timeout(Timeuots.Second.Two)]
        public void UiServicesTests_GetViewModelTestCase()
        {
            ThreadHelper.RunActinInSTAThread(() =>
            {
                Initialize();

                var result = ViewModel.GetViewModel<UiServicesViewModel>();

                Assert.AreEqual(ViewModel.UiServices, result.UiServices, "UiServices must be match");
            }, Timeuots.Second.Two);
        }

        [TestMethod]
        [Description("[view-model][service] check method AddTransient")]
        [Timeout(Timeuots.Second.Two)]
        public void UiServicesTests_ContainsTestCase()
        {
            ThreadHelper.RunActinInSTAThread(() =>
            {
                Initialize();

                ViewModel.UiServices.AddTransient<UiService, UiService>(_frame);

                Assert.IsTrue(ViewModel.UiServices.Contains<UiService>(), "services must contains");
                Assert.AreEqual(_frame, ViewModel.UiServices.GetUiService<UiService>().GetFrame(), "frames must match");
            }, Timeuots.Second.One);
        }

        [TestMethod]
        [Description("[view-model][service] check method AddTransient tow services")]
        [Timeout(Timeuots.Second.Two)]
        public void UiServicesTests_AddTransientTowServicesTestCase()
        {
            ThreadHelper.RunActinInSTAThread(() =>
            {
                Initialize();

                ViewModel.UiServices.AddTransient<UiService, UiService>(_frame);
                var result1 = ViewModel.UiServices.GetUiService<UiService>();
                var result2 = ViewModel.UiServices.GetUiService<UiService>();

                Assert.IsNotNull(result1, "result1 cannot be null");
                Assert.IsNotNull(result2, "result2 cannot be null");
                Assert.AreNotEqual(result1, result2, "services must different");
            }, Timeuots.Second.One);
        }

        [TestMethod]
        [Description("[negative][view-model][service] check method AddTransient. Add service already added previously")]
        [Timeout(Timeuots.Second.Two)]
        public void UiServicesTests_AddTransientNegativeTestCase()
        {
            ThreadHelper.RunActinInSTAThread(() =>
            {
                Initialize();

                ViewModel.UiServices.AddTransient<UiService, UiService>(_frame);

                TestHelper.ExpectedException<ArgumentException>(
                    () => ViewModel.UiServices.AddTransient<UiService, UiService>(_frame),
                    "service already added previously");
            }, Timeuots.Second.One);
        }

        [TestMethod]
        [Description("[negative][view-model][service] check method GetUiService. Return service does not exist")]
        [Timeout(Timeuots.Second.Two)]
        public void UiServicesTests_GetUiServiceNegativeTestCase()
        {
            ThreadHelper.RunActinInSTAThread(() =>
            {
                Initialize();

                TestHelper.ExpectedException<ArgumentException>(
                    () => ViewModel.UiServices.GetUiService<UiService>(),
                    "service does not exist");
            }, Timeuots.Second.One);
        }

        [TestMethod]
        [Description("[view-model][service] check method AddSingleton")]
        [Timeout(Timeuots.Second.Two)]
        public void UiServicesTests_AddSingletonTestCase()
        {
            ThreadHelper.RunActinInSTAThread(() =>
            {
                Initialize();

                ViewModel.UiServices.AddSingleton<UiService, UiService>(_frame);
                var result = ViewModel.UiServices.GetUiService<UiService>();

                Assert.IsNotNull(result, "result cannot be null");
                Assert.IsTrue(ViewModel.UiServices.Contains<UiService>(), "services must contains");
                Assert.AreEqual(_frame, result.GetFrame(), "frames must match");
            }, Timeuots.Second.One);
        }

        [TestMethod]
        [Description("[view-model][service] check method AddSingleton tow services")]
        [Timeout(Timeuots.Second.Two)]
        public void UiServicesTests_AddSingletonTowServicesTestCase()
        {
            ThreadHelper.RunActinInSTAThread(() =>
            {
                Initialize();

                ViewModel.UiServices.AddSingleton<UiService, UiService>(_frame);
                var result1 = ViewModel.UiServices.GetUiService<UiService>();
                var result2 = ViewModel.UiServices.GetUiService<UiService>();

                Assert.AreEqual(result1, result2, "services must match");
            }, Timeuots.Second.One);
        }

        [TestMethod]
        [Description("[negative][view-model][service] check method AddSingleton. Add service already added previously")]
        [Timeout(Timeuots.Second.Two)]
        public void UiServicesTests_AddSingletonNegativeTestCase()
        {
            ThreadHelper.RunActinInSTAThread(() =>
            {
                Initialize();

                ViewModel.UiServices.AddSingleton<UiService, UiService>(_frame);

                TestHelper.ExpectedException<ArgumentException>(
                    () => ViewModel.UiServices.AddSingleton<UiService, UiService>(_frame),
                    "service already added previously");
            }, Timeuots.Second.One);
        }

        [TestMethod]
        [Description("[negative][view-model][service] check method AddTransient. Add service already added previously with AddSingleton")]
        [Timeout(Timeuots.Second.Two)]
        public void UiServicesTests_AddSingletonAndAddTransientNegativeTestCase()
        {
            ThreadHelper.RunActinInSTAThread(() =>
            {
                Initialize();

                ViewModel.UiServices.AddSingleton<UiService, UiService>(_frame);

                TestHelper.ExpectedException<ArgumentException>(
                    () => ViewModel.UiServices.AddTransient<UiService, UiService>(_frame),
                    "service already added previously");
            }, Timeuots.Second.One);
        }

        [TestMethod]
        [Description("[negative][view-model][service] check method AddSingleton. Add service already added previously with AddTransient")]
        [Timeout(Timeuots.Second.Two)]
        public void UiServicesTests_AddTransientAndAddSingletonNegativeTestCase()
        {
            ThreadHelper.RunActinInSTAThread(() =>
            {
                Initialize();

                ViewModel.UiServices.AddTransient<UiService, UiService>(_frame);

                TestHelper.ExpectedException<ArgumentException>(
                    () => ViewModel.UiServices.AddSingleton<UiService, UiService>(_frame),
                    "service already added previously");
            }, Timeuots.Second.One);
        }
    }
}
