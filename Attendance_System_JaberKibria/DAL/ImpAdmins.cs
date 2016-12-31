using Attendance_System_JaberKibria.Common;
using Attendance_System_JaberKibria.DAL;
using Attendance_System_JaberKibria.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Attendance_System_JaberKibria.DAL
{
    public class ImpAdmins : IAdmins
    {
        private readonly AttendanceSystemEntities _db;
        private string _query;
        private string _connectionString;
        private int AdminId = 1;
        private DateTime dateNow = DateTime.Now;

        public ImpAdmins()
        {
            _db = Global.dbContext;
            _connectionString = Global.ConnectionString;
        }

        public IEnumerable<Admin> GetAllAdmins()
        {
            _query = @"SELECT * FROM Admins WHERE Status = 1";
            var allAdmins = _db.Database.SqlQuery<Admin>(_query).ToList();
            return allAdmins;
        }

        public bool AddOrUpdateAdmin(Admin admin)
        {

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                
                SqlTransaction sqlTran = connection.BeginTransaction();
                
                SqlCommand command = connection.CreateCommand();
                command.Transaction = sqlTran;

                try
                {
                    if (admin.Id != 0)
                    {
                        var
                         _query = string.Format(@"");

                        command.CommandText = _query;
                        command.ExecuteNonQuery();

                    }
                    else
                    {
                        _query = string.Format(@"INSERT INTO Admins (Name,Username,Password,CreatedBy,CreateDate,Status)" +
                                               " VALUES(@Name,@Username,@Password,@CreatedBy,@CreateDate,1)");
                        command.Parameters.AddWithValue("@Name", admin.Name);
                        command.Parameters.AddWithValue("@Username", admin.Username);
                        command.Parameters.AddWithValue("@Password", admin.Password);
                        command.Parameters.AddWithValue("@CreateDate", dateNow);
                        command.Parameters.AddWithValue("@CreatedBy", AdminId);
                        command.CommandText = _query;

                    }

                    sqlTran.Commit();
                    return true;

                }
                catch (Exception ex)
                {
                    sqlTran.Rollback();
                    return false;
                }
            }
        }

        public bool DeleteAdmin(int ID)
        {
            try
            {
                _query = string.Format(@"UPDATE Admins SET Status='{0}' WHERE Id='{1}'",
                                               0, ID);
                _db.Database.ExecuteSqlCommand(_query);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }
    }
}