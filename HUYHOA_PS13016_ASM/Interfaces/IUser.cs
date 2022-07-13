using HUYHOA_PS13016_ASM.Models;
using System.Collections.Generic;

namespace HUYHOA_PS13016_ASM.Interfaces
{
    public interface IUser
    {
        List<Users> GetUserAll();
        Users CheckUser(Users user);

    }
}
