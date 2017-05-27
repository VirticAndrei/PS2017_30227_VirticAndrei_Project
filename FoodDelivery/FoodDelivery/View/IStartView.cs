using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FoodDelivery.Controller;

namespace FoodDelivery.View
{
    public interface IStartView
    {
        void SetController(StartController controller);
        void ValidateLog(string errorMsg);
        void ValidateSign(string errorMsg);
        void Logout();
        void Open();
        void CloseView();
    }
}
