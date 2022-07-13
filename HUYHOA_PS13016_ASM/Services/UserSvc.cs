using HUYHOA_PS13016_ASM.Interfaces;
using HUYHOA_PS13016_ASM.Models;
using System.Collections.Generic;
using System.Linq;

namespace HUYHOA_PS13016_ASM.Services
{
    public class UserSvc: IUser
    {
        protected DataContext _context;
        
        public UserSvc(DataContext context)
        {
            _context = context;
        }
        public Users CheckUser(Users user)
        {
            var check = _context.Users.FirstOrDefault(s => s.UserFullName == user.UserFullName);
            return check;
                
        }

        public List<Users> GetUserAll()
        {
            throw new System.NotImplementedException();
        }
    }
}
