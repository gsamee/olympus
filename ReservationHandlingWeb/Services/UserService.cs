using Microsoft.EntityFrameworkCore;
using ReservationHandlingWeb.Data;
using ReservationHandlingWeb.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationHandlingWeb.Services
{
    public class UserService
    {
        private readonly ReservationDBContext _context;
        public UserService(ReservationDBContext context)
        {
            _context = context;
        }

        public async Task UploadMemberCSV(string filepath)
        {
            try
            {
                MemberDetails member = new MemberDetails();
                var lines = System.IO.File.ReadAllLines(filepath);
                if (lines.Count() == 0) return;
                var columns = lines[0].Split(',');
                var table = new DataTable();
                var tableSeat = new DataTable();
                foreach (var c in columns)
                    table.Columns.Add(c);

                for (int i = 1; i < lines.Count(); i++)
                    table.Rows.Add(lines[i].Split(','));

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    member.ID = 0;
                    member.UserID = Convert.ToInt32(table.Rows[i][0].ToString());
                    member.Name = table.Rows[i][1].ToString();
                    member.NoOfMembers = Convert.ToInt32(table.Rows[i][2].ToString());
                    member.SeatNo = table.Rows[i][3].ToString();
                    member.Meal1 = table.Rows[i][4].ToString().ToUpper().Equals("TRUE");
                    member.Meal2 = table.Rows[i][5].ToString().ToUpper().Equals("TRUE");
                    member.Meal3 = table.Rows[i][6].ToString().ToUpper().Equals("TRUE");
                    member.IsVeg = table.Rows[i][7].ToString().ToUpper().Equals("TRUE");
                    member.Email = table.Rows[i][8].ToString();
                    member.IsInvitationSent = table.Rows[i][9].ToString().ToUpper().Equals("TRUE");
                    member.IsReminderSent = table.Rows[i][10].ToString().ToUpper().Equals("TRUE");

                    if (i == 0)
                    {
                        _context.DeleteMembers(Convert.ToInt32(member.UserID));
                        //await _context.SaveChangesAsync();
                    }

                    _context.Add(member);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
