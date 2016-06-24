using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QSI.Services.Spec
{
    public interface IPasswordHash
    {
        bool ValidatePassword(string password, string correctHash);

        string HashPassword(string password);
    }
}
