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
            AdminId = Convert.ToInt32(HttpContext.Current.Session["AdminId"]);
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
                                               " VALUES(" + employee.Id + ",'" + Date + "'," + AdminId + ",'" + dateNow + "',1)");

                        command.CommandText = _query;
                        command.ExecuteNonQuery();
                    }
                    sqlTran.Commit();
                    return "1";

                }
                catch (SqlException ex)
                {
                    sqlTran.Rollback();
                    return ex.Number.ToString();
                }
            }
        }

        public IEnumerable<AttendanceCustom> DaywiseData(DateTime Date)
        {
            _query = @"SELECT a1.*, a2.Username CreatedAdmin, a3.Username UpdatedAdmin, a4.Name EmployeeName, a4.Username EmployeeUsername " +
                " FROM Attendance a1 LEFT OUTER JOIN Admins a2 ON a1.CreatedBy = a2.Id LEFT OUTER JOIN Admins a3 " +
                " ON a1.UpdatedBy = a3.Id INNER JOIN Employees a4 ON a4.Id = a1.EmployeeId WHERE a1.Date = '" + Date + "'";
            var attendance = _db.Database.SqlQuery<AttendanceCustom>(_query).ToList();
            return attendance;
        }

        public IEnumerable<AttendanceCustom> PersonwiseData(int Id, int Month, int Year)
        {
            _query = @"SELECT a1.*, a2.Username CreatedAdmin, a3.Username UpdatedAdmin, a4.Name EmployeeName, a4.Username EmployeeUsername " +
                " FROM Attendance a1 LEFT OUTER JOIN Admins a2 ON a1.CreatedBy = a2.Id LEFT OUTER JOIN Admins a3 ON a1.UpdatedBy = a3.Id " +
                " INNER JOIN Employees a4 ON a4.Id = a1.EmployeeId WHERE a1.EmployeeId = " + Id + " AND MONTH(a1.Date) = " +
                Month + " AND Year(a1.Date) = " + Year + "";
            var attendance = _db.Database.SqlQuery<AttendanceCustom>(_query).ToList();
            return attendance;
        }

        public IEnumerable<AttendanceCustom> AttendanceDetails(int Id, DateTime Date)
        {
            _query = @"SELECT * FROM Attendance WHERE EmployeeId = " + Id + " AND Date = '" + Date + "'";
            var attendance = _db.Database.SqlQuery<AttendanceCustom>(_query).ToList();
            return attendance;
        }

        public int UpdateAttendance(AttendanceCustom attendance)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlTransaction sqlTran = connection.BeginTransaction();

                SqlCommand command = connection.CreateCommand();
                command.Transaction = sqlTran;

                try
                {
                    if (attendance.Id == 0)
                    {
                        _query = string.Format(@"INSERT INTO Attendance (Intime, OutTime, EmployeeId, Date, CreatedBy,CreateDate, Status) VALUES('" +
                            attendance.InTime + "','" + attendance.OutTime + "', '" + attendance.EmployeeId + "', '" + attendance.Date + "', " +
                            AdminId + ", '" + dateNow + "', 1)");
                    }
                    else
                    {
                        _query = string.Format(@"UPDATE Attendance SET  Intime = '" + attendance.InTime + "', OutTime = '" + attendance.OutTime + "', " +
                            "UpdatedBy = " + AdminId + ", UpdateDate = '" + dateNow + "'  WHERE Id = " + attendance.Id);
                    }
                    command.CommandText = _query;
                    command.ExecuteNonQuery();
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

        public int Checkin(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlTransaction sqlTran = connection.BeginTransaction();

                SqlCommand command = connection.CreateCommand();
                command.Transaction = sqlTran;

                try
                {
                    _query = string.Format(@"UPDATE Attendance SET  InTime = '" + dateNow + "' WHERE EmployeeId = " + id + " AND Date = '" +
                        DateTime.Today + "' AND InTime  IS NULL");

                    command.CommandText = _query;
                    command.ExecuteNonQuery();
                    sqlTran.Commit();
                    return 1;
                }
                catch (Exception ex)
                {
                    sqlTran.Rollback();
                    return 0;
                }
            }
        }

        public int Checkout(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlTransaction sqlTran = connection.BeginTransaction();

                SqlCommand command = connection.CreateCommand();
                command.Transaction = sqlTran;

                try
                {
                    _query = string.Format(@"UPDATE Attendance SET  OutTime = '" + dateNow + "' WHERE EmployeeId = " + id + " AND Date = '" +
                        DateTime.Today + "' AND OutTime IS NULL");

                    command.CommandText = _query;
                    command.ExecuteNonQuery();
                    sqlTran.Commit();
                    return 1;
                }
                catch (Exception ex)
                {
                    sqlTran.Rollback();
                    return 0;
                }

            }
        }
    }
}