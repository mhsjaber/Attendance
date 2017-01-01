using Attendance_System_JaberKibria.Common;
using Attendance_System_JaberKibria.CustomModels;
using Attendance_System_JaberKibria.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Attendance_System_JaberKibria.DAL
{
    public class ImpAttendance : IAttendance
    {
        private readonly AttendanceSystemEntities _db;
        private string _query;
        private string _connectionString;
        private int AdminId = 1;
        private DateTime dateNow = DateTime.Now;

        public ImpAttendance()
        {
            _db = Global.dbContext;
            _connectionString = Global.ConnectionString;
        }

        public string Createday(IEnumerable<EmployeeCustom> employees, DateTime Date)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlTransaction sqlTran = connection.BeginTransaction();

                SqlCommand command = connection.CreateCommand();
                command.Transaction = sqlTran;

                try
                {
                    foreach (var employee in employees)
                    {
                        _query = string.Format(@"INSERT INTO Attendance (EmployeeId,Date,CreatedBy,CreateDate,Status)" +
                                               " VALUES('@EmployeeId','@Date','@CreatedBy','@CreateDate',1)");
                        command.Parameters.AddWithValue("@EmployeeId", employee.Id);
                        command.Parameters.AddWithValue("@Date", Date);
                        command.Parameters.AddWithValue("@CreateDate", dateNow);
                        command.Parameters.AddWithValue("@CreatedBy", AdminId);
                        command.CommandText = _query;
                        command.ExecuteNonQuery();
                    }
                    sqlTran.Commit();
                    return "1";

                }
                catch (Exception ex)
                {
                    sqlTran.Rollback();
                    return ex.Message;
                }
            }
        }
    }
}