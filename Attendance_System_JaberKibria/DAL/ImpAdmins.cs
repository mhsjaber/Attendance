using Attendance_System_JaberKibria.Common;
using Attendance_System_JaberKibria.CustomModels;
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
        private int AdminId;
        private DateTime dateNow = DateTime.Now;

        public ImpAdmins()
        {
            AdminId = Convert.ToInt32(HttpContext.Current.Session["AdminId"]);
            _db = Global.dbContext;
            _connectionString = Global.ConnectionString;
        }

        public IEnumerable<AdminCustom> GetAllAdmins()
        {
            _query = @"SELECT a1.*, a2.Username CreatedAdmin, a3.Username UpdatedAdmin FROM Admins a1 LEFT OUTER JOIN Admins a2 ON a1.CreatedBy = a2.Id LEFT OUTER JOIN Admins a3 ON a1.UpatedBy = a3.Id WHERE a1.Status = 1";
            var allAdmins = _db.Database.SqlQuery<AdminCustom>(_query).ToList();
            return allAdmins;
        }

        public int AddOrUpdateAdmin(Admin admin)
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
                        //_query = string.Format(@"INSERT INTO Admins (Name,Username,Password,CreatedBy,CreateDate,Status)" +
                        //                       " VALUES('{0}', '{1}', '{2}', '{3}', '{4}','1')SELECT SCOPE_IDENTITY()"
                        //                       , admin.Name, admin.Username, admin.Password, AdminId, dateNow);

                        _query = string.Format(@"INSERT INTO Admins (Name,Username,Password,CreatedBy,CreateDate,Status)" +
                                               " VALUES(@Name,@Username,@Password,@CreatedBy,@CreateDate,1)");
                        command.Parameters.AddWithValue("@Name", admin.Name);
                        command.Parameters.AddWithValue("@Username", admin.Username);
                        command.Parameters.AddWithValue("@Password", admin.Password);
                        command.Parameters.AddWithValue("@CreateDate", dateNow);
                        command.Parameters.AddWithValue("@CreatedBy", AdminId);
                        command.CommandText = _query;
                        command.ExecuteNonQuery();

                    }

                    sqlTran.Commit();
                    return 1;

                }
                catch (SqlException ex)
                {
                    sqlTran.Rollback();
                    return ex.Number;
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
        public IEnumerable<Admin> Login(string Username)
        {
            _query = string.Format(@"SELECT * FROM Admins WHERE Username = '{0}' AND Status = 1", Username);
            var loggedAdmin = _db.Database.SqlQuery<Admin>(_query).ToList();
            return loggedAdmin;
        }
    }
}