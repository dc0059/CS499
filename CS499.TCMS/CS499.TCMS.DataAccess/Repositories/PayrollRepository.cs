using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CS499.TCMS.DataAccess;
using ToolKit.Data;
using CS499.TCMS.Model;
using System.Data;
using CS499.TCMS.DataAccess.IRepositories;

namespace CS499.TCMS.DataAccess.Repositories
{
    internal class PayrollRepository : GenericRepository<Payroll>, IPayrollRepository
    {
        #region Constructor
        public PayrollRepository(IDatabase database) : base(database)
        {
        }
        #endregion

        #region Methods
        public void Delete(Payroll model)
        {
            // create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "DELETE FROM payroll " +
                              "WHERE PayrollID = ?",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            // create parameter definition
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_PayrollID",
                Type = DbType.Int64,
                Value = model.PayrollID
            });

            this.Database.ExecuteModQuery(definition);

            // Create query definition
            definition = new QueryDefinition()
            {
                CommandText = "UPDATE payroll_log " +
                              "SET DeletedBy = ? " +
                              "WHERE PayrollID = ? " +
                              "AND ModifiedStatus = 'D'",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            // create parameter definition            
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_User",
                Type = DbType.String,
                Value = this.Database.UserName
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_ID",
                Type = DbType.Int64,
                Value = model.PayrollID
            });

            this.Database.ExecuteModQuery(definition);
        }

        public void Delete(long PayrollID)
        {
            // create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "DELETE FROM payroll " +
                              "WHERE PayrollID = ?",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            // create parameter definition
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_VehicleID",
                Type = DbType.Int64,
                Value = PayrollID
            });

            this.Database.ExecuteModQuery(definition);

            // Create query definition
            /*definition = new QueryDefinition()
            {
                CommandText = "UPDATE users_log " +
                              "SET DeletedBy = ? " +
                              "WHERE UserID = ? " +
                              "AND ModifiedStatus = 'D'",
                cType = CommandType.Text,
                Database = "database_name",
                Type = ConnectionType.MySQL
            };

            // create parameter definition            
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_User",
                Type = DbType.String,
                Value = this.Database.UserName
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_ID",
                Type = DbType.Int64,
                Value = model.EmployeeID
            });

            this.Database.ExecuteModQuery(definition);*/
        }

        public IEnumerable<Payroll> GetAll()
        {
            // create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "SELECT PaymentID, EmployeeID, PaymentDate, Payment " +
                              "FROM payroll " +
                              "ORDER BY PayrollID",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            return this.Database.ExecuteListQuery<Payroll>(definition, Map);
        }

        public Payroll GetSingle(object id)
        {
            // Create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "SELECT PaymentID, EmployeeID, PaymentDate, Payment " +
                              "FROM payroll " +
                              "WHERE PaymentID = ?",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            // create parameter definition
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_PaymentID",
                Type = DbType.Int64,
                Value = id
            });

            return this.Database.ExecuteSingleQuery<Payroll>(definition, Map);
        }

        /*public Payroll GetSingle(long PayrollID)
        {
            throw new NotImplementedException();
        }*/

        public IEnumerable<Payroll> GetStubsByDate(DateTime date)
        {
            // create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "SELECT PaymentID, EmployeeID, PaymentDate, Payment " +
                              "FROM payroll " +
                              "WHERE PaymentDate = ? " +
                              "ORDER BY PayrollID",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            // create parameter definition
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_PaymentDate",
                Type = DbType.Int64,
                Value = date
            });

            return this.Database.ExecuteListQuery<Payroll>(definition, Map);
        }

        public IEnumerable<Payroll> GetStubsByEmployee(long EmployeeID)
        {
            // create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "SELECT PaymentID, EmployeeID, PaymentDate, Payment " +
                              "FROM payroll " +
                              "WHERE EmployeeID = ? " +
                              "ORDER BY PayrollID",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };
            
            // create parameter definition
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_EmployeeID",
                Type = DbType.Int64,
                Value = EmployeeID
            });

            return this.Database.ExecuteListQuery<Payroll>(definition, Map);
        }

        public void Insert(Payroll model)
        {

            long id;

            // Create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "INSERT INTO payroll (EmployeeID, PaymentDate, Payment, CreatedBy, LastModifiedBy) " +
                              "VALUES (?,?,?,?,?)",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            // create parameter definition
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_EmployeeID",
                Type = DbType.Int64,
                Value = model.EmployeeID
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_PaymentDate",
                Type = DbType.DateTime,
                Value = model.PaymentDate
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_Payment",
                Type = DbType.Double,
                Value = model.Payment
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_CreatedBy",
                Type = DbType.String,
                Value = this.Database.UserName
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_LastModifiedBy",
                Type = DbType.String,
                Value = this.Database.UserName
            });

            this.Database.ExecuteModQuery(definition, out id);

            model.PayrollID = id;
        }

        public void Update(Payroll model)
        {
            //create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "UPDATE payroll " +
                              "SET EmployeeID = ?, PaymentDate = ?, Payment = ?, LastModifiedBy = ? " +
                              "WHERE PaymentID = ?",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            // create parameter definition
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_EmployeeID",
                Type = DbType.Int64,
                Value = model.EmployeeID
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_PaymentDate",
                Type = DbType.DateTime,
                Value = model.PaymentDate
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_Payment",
                Type = DbType.Double,
                Value = model.Payment
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_LastModifiedBy",
                Type = DbType.String,
                Value = this.Database.UserName
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_PayrollID",
                Type = DbType.Int64,
                Value = model.PayrollID
            });

            this.Database.ExecuteModQuery(definition);
        }

        protected override Payroll Map(IDataReader reader)
        {
            return new Payroll(reader.GetValueOrDefault<long>("PaymentId"),
                reader.GetValueOrDefault<long>("EmployeeID"),
                reader.GetValueOrDefault<DateTime>("PaymentDate"),
                reader.GetValueOrDefault<double>("Payment"),
                reader.GetValueOrDefault<double>("HoursWorked"));
        }

        #endregion
    }
}
