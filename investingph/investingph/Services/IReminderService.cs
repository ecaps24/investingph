using System;
using System.Collections.Generic;
using System.Text;

namespace investingph.Services
{
    public interface IReminderService
    {
        void Remind(string title, string message, DateTime dateTime, string type);
    }
}
