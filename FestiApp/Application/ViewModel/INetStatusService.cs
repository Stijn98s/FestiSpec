using System;

namespace FestiApp.ViewModel
{
    public interface INetStatusService
    {
        bool IsActive { get; }
        bool NotIsActive { get; }
        void SubscribeWithAction(Action raiseCanExecuteChanged);
        void Start();
    }
}