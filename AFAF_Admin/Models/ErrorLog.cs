using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data;
using System.Data.OleDb;
using System.Web.Mvc;
using AFAF_Admin.Models;


namespace AFAF_Admin.Models
{
    public class ErrorLog
    {
        [Key, Required]
        public int errorID { get; set; }

        [Required]
        public DateTime timeStamp { get; set; }

        [Required]
        public String fileName { get; set; }

        [Required]
        public String functionName { get; set; }

        [Required]
        public String lineNumber { get; set; }

        [Required]
        public String errorText { get; set; }

        [Required]
        public String errorCode { get; set; }

        public String extraData { get; set; }

        /// <summary>
        /// Logs an exception into the ErrorLog table
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="additionalInformation"></param>
        /// <returns></returns>
        public static void logError(Exception ex, String additionalInformation)
        {
            ErrorLogEntities db = new ErrorLogEntities();
            ErrorLog errorLog = new ErrorLog();

            errorLog.timeStamp = DateTime.Now;
            errorLog.fileName = ex.StackTrace;
            errorLog.functionName = ex.TargetSite.ToString();
            errorLog.lineNumber = ex.StackTrace;
            errorLog.errorText = ex.Message;
            errorLog.errorCode = ex.HResult.ToString();
            errorLog.extraData = additionalInformation;

            db.ErrorLogs.Add(errorLog);
            db.SaveChanges();
        }
    }

    public class ErrorLogEntities : DbContext
    {
        public DbSet<ErrorLog> ErrorLogs { get; set; }
    }
}