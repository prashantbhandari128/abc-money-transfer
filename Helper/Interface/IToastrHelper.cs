﻿using ABCMoneyTransfer.Helper.Data.Toastr;
using Microsoft.AspNetCore.Mvc;

namespace ABCMoneyTransfer.Helper.Interface
{
    public interface IToastrHelper
    {
        void SendMessage(Controller controller, string title, string message, MessageType messageType);
        void AddMessage(string title, string message, MessageType messageType);
        void Send(Controller controller);
    }
}