using Microsoft.EntityFrameworkCore;
using AceJobAgency.Model;
using AceJobAgency.ViewModels;

namespace AceJobAgency.Services
{
    public class MembershipService
    {
        private readonly AuthDbContext _context;

        public MembershipService(AuthDbContext context)
        {
            _context = context;
        }

        public List<AceJobUser> GetAll()
        {
            return _context.Memberships.OrderBy(x => x.NRIC).ToList();
        }

        public AceJobUser? GetAceJobUserByEmail(string email)
        {
            AceJobUser? AceJobUser = _context.Memberships.FirstOrDefault(x => x.Email.Equals(email));
            return AceJobUser;
        }

        public AceJobUser? GetAceJobUserByNRIC(string NRIC)
        {
            AceJobUser? AceJobUser = _context.Memberships.FirstOrDefault(x => x.NRIC.Equals(NRIC));
            return AceJobUser;
        }

        public void AddMember(AceJobUser member)
        {
            _context.Memberships.Add(member);
            _context.SaveChanges();
        }

        public void UpdateMember(AceJobUser Member)
        {
            _context.Memberships.Update(Member);
            _context.SaveChanges();
        }

        public void DeleteMember(AceJobUser Member)
        {
            _context.Memberships.Remove(Member);
            _context.SaveChanges();
        }
    }
}
