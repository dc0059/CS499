using System;
using System.Threading.Tasks;
using MahApps.Metro.Controls.Dialogs;

namespace CS499.TCMS.ViewUnitTest.DummyClasses
{
    public class DummyDialogCoordinator : IDialogCoordinator
    {
        Task<TDialog> IDialogCoordinator.GetCurrentDialogAsync<TDialog>(object context)
        {
            throw new NotImplementedException();
        }

        Task IDialogCoordinator.HideMetroDialogAsync(object context, BaseMetroDialog dialog, MetroDialogSettings settings)
        {
            throw new NotImplementedException();
        }

        Task<string> IDialogCoordinator.ShowInputAsync(object context, string title, string message, MetroDialogSettings settings)
        {
            throw new NotImplementedException();
        }

        Task<LoginDialogData> IDialogCoordinator.ShowLoginAsync(object context, string title, string message, LoginDialogSettings settings)
        {
            return Task.Run(() => 
            {
                return new LoginDialogData();
            });
        }

        Task<MessageDialogResult> IDialogCoordinator.ShowMessageAsync(object context, string title, string message, MessageDialogStyle style, MetroDialogSettings settings)
        {
            return Task.Run(() =>
            {
                return MessageDialogResult.Affirmative;
            });
        }

        Task IDialogCoordinator.ShowMetroDialogAsync(object context, BaseMetroDialog dialog, MetroDialogSettings settings)
        {
            throw new NotImplementedException();
        }

        string IDialogCoordinator.ShowModalInputExternal(object context, string title, string message, MetroDialogSettings settings)
        {
            throw new NotImplementedException();
        }

        LoginDialogData IDialogCoordinator.ShowModalLoginExternal(object context, string title, string message, LoginDialogSettings settings)
        {
            throw new NotImplementedException();
        }

        MessageDialogResult IDialogCoordinator.ShowModalMessageExternal(object context, string title, string message, MessageDialogStyle style, MetroDialogSettings settings)
        {
            throw new NotImplementedException();
        }

        Task<ProgressDialogController> IDialogCoordinator.ShowProgressAsync(object context, string title, string message, bool isCancelable, MetroDialogSettings settings)
        {
            throw new NotImplementedException();
        }
    }
}
