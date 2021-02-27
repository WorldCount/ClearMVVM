﻿using System.Windows;
using ClearMVVM.Infrastructure.Commands.Base;

namespace ClearMVVM.Infrastructure.Commands
{
    class CloseWindowCommand : Command
    {
        private static Window GetWindow(object p) => p as Window ?? App.FocusedWindow ?? App.ActivedWindow;

        protected override bool CanExecute(object p) => GetWindow(p) != null;

        protected override void Execute(object p) => GetWindow(p)?.Close();
    }
}
