﻿using System;
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
    class PurchaseOrderRepository : GenericRepository<PurchaseOrder>, IPurchaseOrderRepository
    {

        #region Constructor
        public PurchaseOrderRepository(IDatabase database) : base(database)
        {
        }

        #endregion

        #region Methods
        public void Delete(PurchaseOrder model)
        {
            // create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "DELETE FROM purchaseorder " +
                              "WHERE OrderID = ?",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            // create parameter definition
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_OrderID",
                Type = DbType.Int64,
                Value = model.OrderID
            });

            this.Database.ExecuteModQuery(definition);

            // Create query definition
            definition = new QueryDefinition()
            {
                CommandText = "UPDATE purchaseorder_log " +
                              "SET DeletedBy = ? " +
                              "WHERE OrderID = ? " +
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
                Value = model.OrderID
            });

            this.Database.ExecuteModQuery(definition);
        }

        public void DeleteByDest(long DestComp)
        {
            // create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "DELETE FROM purchaseorder " +
                              "WHERE DestinationID = ?",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            // create parameter definition
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_DestinationID",
                Type = DbType.Int64,
                Value = DestComp
            });

            this.Database.ExecuteModQuery(definition);
        }

        public void DeleteByManifest(long ManifestID)
        {
            // create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "DELETE FROM purchaseorder " +
                              "WHERE ManifestID = ?",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            // create parameter definition
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_ManifestID",
                Type = DbType.Int64,
                Value = ManifestID
            });

            this.Database.ExecuteModQuery(definition);
        }

        public void DeleteByNumber(long OrderNum)
        {
            // create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "DELETE FROM purchaseorder " +
                              "WHERE OrderNumber = ?",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            // create parameter definition
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_OderNumber",
                Type = DbType.Int64,
                Value = OrderNum
            });

            this.Database.ExecuteModQuery(definition);
        }

        public void DeleteBySource(long SourceComp)
        {
            // create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "DELETE FROM purchaseorder " +
                              "WHERE SourceID = ?",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            // create parameter definition
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_SourceID",
                Type = DbType.Int64,
                Value = SourceComp
            });

            this.Database.ExecuteModQuery(definition);
        }

        public IEnumerable<PurchaseOrder> GetAll()
        {
            // create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "SELECT OrderID, OrderNumber, SourceID, DestinationID, ManifestID, PaymentMade " +
                              "FROM purchaseorder " +
                              "ORDER BY OrderID",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            return this.Database.ExecuteListQuery<PurchaseOrder>(definition, Map);
        }

        public IEnumerable<PurchaseOrder> GetOrderByDestination(long DestCompany)
        {
            // create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "SELECT OrderID, OrderNumber, SourceID, DestinationID, ManifestID " +
                              "FROM purchaseorder " +
                              "WHERE DestinationID = ? " +
                              "ORDER BY OrderID",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            // create parameter definition
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_DestinationID",
                Type = DbType.Int64,
                Value = DestCompany
            });

            return this.Database.ExecuteListQuery<PurchaseOrder>(definition, Map);
        }

        public IEnumerable<PurchaseOrder> GetOrderByManifest(long ManifestID)
        {
            // create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "SELECT OrderID, OrderNumber, SourceID, DestinationID, ManifestID " +
                              "FROM purchaseorder " +
                              "WHERE ManifestID = ? " +
                              "ORDER BY OrderID",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            // create parameter definition
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_ManifestID",
                Type = DbType.Int64,
                Value = ManifestID
            });

            return this.Database.ExecuteListQuery<PurchaseOrder>(definition, Map);
        }

        public IEnumerable<PurchaseOrder> GetOrderByNumber(long OrderNum)
        {
            // create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "SELECT OrderID, OrderNumber, SourceID, DestinationID, ManifestID " +
                              "FROM purchaseorder " +
                              "WHERE OrderNumber = ? " +
                              "ORDER BY OrderID",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            // create parameter definition
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_OrderNumber",
                Type = DbType.Int64,
                Value = OrderNum
            });

            return this.Database.ExecuteListQuery<PurchaseOrder>(definition, Map);
        }

        public IEnumerable<PurchaseOrder> GetOrderBySource(long SourceCompany)
        {
            // create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "SELECT OrderID, OrderNumber, SourceID, DestinationID, ManifestID " +
                              "FROM purchaseorder " +
                              "WHERE SourceID = ? " +
                              "ORDER BY OrderID",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            // create parameter definition
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_SourceID",
                Type = DbType.Int64,
                Value = SourceCompany
            });

            return this.Database.ExecuteListQuery<PurchaseOrder>(definition, Map);
        }

        public PurchaseOrder GetSingle(object id)
        {
            // Create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "SELECT OrderID, OrderNumber, SourceID, DestinationID, ManifestID, PaymentMade " +
                              "FROM purchaseorder " +
                              "WHERE OrderID = ?",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            // create parameter definition
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_OrderID",
                Type = DbType.Int64,
                Value = id
            });

            return this.Database.ExecuteSingleQuery<PurchaseOrder>(definition, Map);
        }

        /*public PurchaseOrder GetSingle(long OrderID)
        {
            throw new NotImplementedException();
        }*/

        public void Insert(PurchaseOrder model)
        {

            long id;

            // Create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "INSERT INTO purchaseorder (OrderNumber, SourceID, DestinationID, ManifestID, PaymentMade, CreatedBy, LastModifiedBy) " +
                              "VALUES (?,?,?,?,?,?,?)",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_OrderNumber",
                Type = DbType.Int64,
                Value = model.OrderNumber
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_SourceID",
                Type = DbType.Int64,
                Value = model.SourceID
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_DestinationID",
                Type = DbType.Int64,
                Value = model.DestinationID
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_ManifestID",
                Type = DbType.Int64,
                Value = model.ManifestID
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_PaymentMade",
                Type = DbType.Boolean,
                Value = model.PaymentMade
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

            model.OrderID = id;
        }

        public void Update(PurchaseOrder model)
        {
            //create query definition
            QueryDefinition definition = new QueryDefinition()
            {
                CommandText = "UPDATE purchaseorder " +
                              "SET OrderNumber = ?, SourceID = ?, DestinationID = ?, ManifestID = ?, LastModifiedBy = ? " +
                              "WHERE OrderID = ?",
                cType = CommandType.Text,
                Database = "cs_499_tcms",
                Type = ConnectionType.MySQL
            };

            // create parameter definition
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_OrderNumber",
                Type = DbType.Int64,
                Value = model.OrderNumber
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_SourceID",
                Type = DbType.Int64,
                Value = model.SourceID
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_DestinationID",
                Type = DbType.Int64,
                Value = model.DestinationID
            });
            definition.Parameters.Add(new ParameterDefinition()
            {
                Direction = ParameterDirection.Input,
                Name = "P_ManifestID",
                Type = DbType.Int64,
                Value = model.ManifestID
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
                Name = "P_OrderID",
                Type = DbType.Int64,
                Value = model.OrderID
            });

            this.Database.ExecuteModQuery(definition);
        }

        protected override PurchaseOrder Map(IDataReader reader)
        {
            return new PurchaseOrder(reader.GetValueOrDefault<long>("OrderID"),
                reader.GetValueOrDefault<long>("OrderNumber"),
                reader.GetValueOrDefault<long>("SourceID"),
                reader.GetValueOrDefault<long>("DestinationID"),
                reader.GetValueOrDefault<long>("ManifestID"),
                reader.GetValueOrDefault<bool>("PaymentMade"));
        }


        #endregion
    }
}
